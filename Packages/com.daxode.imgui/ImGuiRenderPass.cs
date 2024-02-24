﻿using System;
using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using Object = UnityEngine.Object;

namespace com.daxode.imgui
{
    public class ImGuiRenderPass : ScriptableRenderPass, IDisposable
    {
        Mesh m_Mesh;
        Material m_Material;
        NativeReference<Mesh.MeshData> m_MeshData;
        NativeArray<VertexAttributeDescriptor> m_VertexAttributes;
        
        GraphicsBuffer m_DrawCmdBuffer;
        NativeList<GraphicsBuffer.IndirectDrawIndexedArgs> m_DrawCommands;
        NativeList<(UnityObjRef<Texture2D> texture, Rect scissor)> m_AdditionalData;
        
        static readonly int k_MainTex = Shader.PropertyToID("_MainTex");
        MaterialPropertyBlock m_PropertyBlock;

        public unsafe ImGuiRenderPass()
        {
            m_Material = CoreUtils.CreateEngineMaterial(Shader.Find("Hidden/ImGuiDrawShader"));
            renderPassEvent = RenderPassEvent.AfterRendering;
            m_DrawCommands = new NativeList<GraphicsBuffer.IndirectDrawIndexedArgs>(100, Allocator.Persistent);
            m_AdditionalData = new NativeList<(UnityObjRef<Texture2D> texture, Rect scissor)>(100, Allocator.Persistent);
            m_Mesh = new Mesh();
            m_MeshData = new NativeReference<Mesh.MeshData>(Allocator.Persistent);
            m_DrawCmdBuffer = new GraphicsBuffer(GraphicsBuffer.Target.IndirectArguments, 1000, GraphicsBuffer.IndirectDrawIndexedArgs.size);
            m_VertexAttributes = new NativeArray<VertexAttributeDescriptor>(3, Allocator.Persistent)
            {
                [0] = new (VertexAttribute.Position, VertexAttributeFormat.Float32, 2),
                [1] = new (VertexAttribute.Color, VertexAttributeFormat.UNorm8, 4),
                [2] = new (VertexAttribute.TexCoord0, VertexAttributeFormat.Float32, 2)
            };
            m_PropertyBlock = new MaterialPropertyBlock();
            
            m_DrawCmdBuffer.name = "ImGuiDrawCmdBuffer";
            
            // Setup Dear ImGui context
            ImGui.CheckVersion();
            ImGui.CreateContext();
            var io = ImGui.GetIO();
            io->ConfigFlags |= ImGuiConfigFlags.NavEnableKeyboard;     // Enable Keyboard Controls
            io->ConfigFlags |= ImGuiConfigFlags.NavEnableGamepad;      // Enable Gamepad Controls
            io->ConfigFlags |= ImGuiConfigFlags.DockingEnable;         // Enable Docking
            
            // Setup Dear ImGui style
            ImGui.StyleColorsDark();

            // Setup Platform/Renderer backends
            InputAndWindowHooks.Init();
            RenderHooks.Init();
            io->Fonts->AddFontFromFileTTF(@"C:\Windows\Fonts\comic.ttf", 72.0f);
        }

        public override unsafe void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
        {
            if (math.any(ImGui.GetIO()->DisplaySize <= 0))
                return;
            
            // Rendering
            ImGui.Render();
            
            var dataArray = Mesh.AllocateWritableMeshData(1);
            m_MeshData.Value = dataArray[0];
            m_DrawCommands.Clear();
            m_AdditionalData.Clear();
            FillDrawCommandsAndAdditionalData(ImGui.GetDrawData(), m_MeshData, m_VertexAttributes,
                (NativeList<GraphicsBuffer.IndirectDrawIndexedArgs>*)UnsafeUtility.AddressOf(ref m_DrawCommands), 
                (NativeList<(UnityObjRef<Texture2D> texture, Rect scissor)>*)UnsafeUtility.AddressOf(ref m_AdditionalData));
            Mesh.ApplyAndDisposeWritableMeshData(dataArray, m_Mesh, MeshUpdateFlags.DontRecalculateBounds | MeshUpdateFlags.DontResetBoneBounds);
            m_Mesh.bounds = new Bounds(Vector3.zero, Vector3.one * 1000);
            m_DrawCmdBuffer.SetCounterValue((uint)m_DrawCommands.Length);
            m_DrawCmdBuffer.SetData(m_DrawCommands.AsArray());
        }

        public override void Execute(ScriptableRenderContext context,
            ref RenderingData renderingData)
        {
            //Get a CommandBuffer from pool.
            var cmd = CommandBufferPool.Get("ImGuiRenderPass");
            for (int i = 0; i < m_DrawCommands.Length; i++)
            {
                m_PropertyBlock.SetTexture(k_MainTex, m_AdditionalData[i].texture);
                cmd.EnableScissorRect(m_AdditionalData[i].scissor);
                cmd.DrawMeshInstancedIndirect(
                    m_Mesh, 0, 
                    m_Material, -1, 
                    m_DrawCmdBuffer, i * GraphicsBuffer.IndirectDrawIndexedArgs.size, 
                    m_PropertyBlock);
            }
            cmd.DisableScissorRect();
            
            //Execute the command buffer and release it back to the pool.
            context.ExecuteCommandBuffer(cmd);
            CommandBufferPool.Release(cmd);
        }


        [BurstCompile]
        unsafe static void FillDrawCommandsAndAdditionalData(
            ImDrawData* draw_data, NativeReference<Mesh.MeshData> data,
            NativeArray<VertexAttributeDescriptor> vertexAttributes,
            NativeList<GraphicsBuffer.IndirectDrawIndexedArgs>* draw_cmds,
            NativeList<(UnityObjRef<Texture2D> texture, Rect scissor)>* additionalData)
        {
            // Avoid rendering when minimized, scale coordinates for retina displays (screen coordinates != framebuffer coordinates)
            var frameSize = (int2)(draw_data->DisplaySize * draw_data->FramebufferScale.x);
            if (math.any(frameSize <= 0))
                return;

            // Allocate array to store enough vertex/index buffers
            var meshData = data.Value;
            if (draw_data->TotalVtxCount > 0)
            {
                // Create or resize the vertex/index buffers
                meshData.SetIndexBufferParams(draw_data->TotalIdxCount, IndexFormat.UInt16);
                meshData.SetVertexBufferParams(draw_data->TotalVtxCount, vertexAttributes);

                // Upload vertex/index data into a single contiguous GPU buffer
                var indexData = meshData.GetIndexData<ImDrawIdx>();
                var vertexData = meshData.GetVertexData<ImDrawVert>();
                var vertexDestination = (ImDrawVert*)vertexData.GetUnsafePtr();
                var indexDestination = (ImDrawIdx*)indexData.GetUnsafePtr();
                for (int n = 0; n < draw_data->CmdListsCount; n++)
                {
                    ref var cmd_list = ref draw_data->CmdLists[n];
                    UnsafeUtility.MemCpy(vertexDestination, cmd_list.Value->VtxBuffer.Data,
                        cmd_list.Value->VtxBuffer.Size * sizeof(ImDrawVert));
                    UnsafeUtility.MemCpy(indexDestination, cmd_list.Value->IdxBuffer.Data,
                        cmd_list.Value->IdxBuffer.Size * sizeof(ImDrawIdx));
                    vertexDestination += cmd_list.Value->VtxBuffer.Size;
                    indexDestination += cmd_list.Value->IdxBuffer.Size;
                }
            }
            
            // Will project scissor/clipping rectangles into framebuffer space
            var clip_off = draw_data->DisplayPos; // (0,0) unless using multi-viewports
            var clip_scale = draw_data->FramebufferScale; // (1,1) unless using retina display which are often (2,2)

            // Render command lists
            // (Because we merged all buffers into a single one, we maintain our own offset into them)
            var global_vtx_offset = 0;
            var global_idx_offset = 0;
            for (int n = 0; n < draw_data->CmdListsCount; n++)
            {
                ref var cmd_list = ref draw_data->CmdLists[n];
                for (int cmd_i = 0; cmd_i < cmd_list.Value->CmdBuffer.Size; cmd_i++)
                {
                    ref var pcmd = ref cmd_list.Value->CmdBuffer[cmd_i];
                    if (pcmd.UserCallback.Value != null)
                    {
                        // User callback, registered via ImDrawList::AddCallback()
                        // (ImDrawCallback_ResetRenderState is a special callback value used by the user to request the renderer to reset render state.)
                        if (pcmd.UserCallback.Value == ImDrawCallback.ResetRenderState)
                        {} // ImGui_ImplVulkan_SetupRenderState(draw_data, fb_width, fb_height);
                        else
                            pcmd.UserCallback.Value(cmd_list.Value, (ImDrawCmd*)UnsafeUtility.AddressOf(ref pcmd));
                    }
                    else
                    {
                        // Project scissor/clipping rectangles into framebuffer space
                        var clipMin = (pcmd.ClipRect.xw - clip_off);
                        clipMin.y = frameSize.y - clipMin.y;
                        clipMin *= clip_scale;
                        var clipMax = (pcmd.ClipRect.zy - clip_off);
                        clipMax.y = frameSize.y - clipMax.y;
                        clipMax *= clip_scale;
                        if (clipMax.x <= clipMin.x || clipMax.y <= clipMin.y)
                            continue;
                        
                        var textureId = pcmd.GetTexID();
                        additionalData->Add((
                            UnsafeUtility.As<ImTextureID, UnityObjRef<Texture2D>>(ref textureId), 
                            new Rect(clipMin, clipMax - clipMin)));

                        // Draw
                        draw_cmds->Add(new GraphicsBuffer.IndirectDrawIndexedArgs()
                        {
                            indexCountPerInstance = pcmd.ElemCount,
                            instanceCount = 10,
                            startIndex = (uint)(pcmd.IdxOffset + global_idx_offset),
                            baseVertexIndex = (uint)(pcmd.VtxOffset + global_vtx_offset),
                            startInstance = 0
                        });
                    }
                }

                global_idx_offset += cmd_list.Value->IdxBuffer.Size;
                global_vtx_offset += cmd_list.Value->VtxBuffer.Size;
            }
            
            meshData.subMeshCount = 1;
            meshData.SetSubMesh(0, 
                new SubMeshDescriptor(0, draw_data->TotalIdxCount), 
                MeshUpdateFlags.DontRecalculateBounds | MeshUpdateFlags.DontResetBoneBounds);
        }

        public void Dispose()
        {
            // Cleanup
            RenderHooks.Shutdown();
            InputAndWindowHooks.Shutdown();
            // ImGui.DestroyContext();
            
            if (EditorApplication.isPlaying)
            {
                Object.Destroy(m_Material);
                Object.Destroy(m_Mesh);
            }
            else
            {
                Object.DestroyImmediate(m_Material);
                Object.DestroyImmediate(m_Mesh);
            }
            
            m_DrawCommands.Dispose();
            m_AdditionalData.Dispose();
            m_MeshData.Dispose();
        }
    }
}