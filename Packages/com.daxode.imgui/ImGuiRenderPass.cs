using System;
using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.LowLevel;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using Object = UnityEngine.Object;

namespace com.daxode.imgui
{
    public class ImGuiRenderPass : ScriptableRenderPass, IDisposable
    {
        Mesh mesh;
        Material material;
        NativeList<GraphicsBuffer.IndirectDrawIndexedArgs> draw_cmds;
        NativeReference<(UnityObjRef<Texture2D> texture, Rect scissor)> additionalData;
        NativeReference<Mesh.MeshData> data;
        GraphicsBuffer drawCmdBuffer;
        
        public unsafe ImGuiRenderPass(Material material)
        {
            this.material = material;
            renderPassEvent = RenderPassEvent.AfterRendering;
            draw_cmds = new NativeList<GraphicsBuffer.IndirectDrawIndexedArgs>(100, Allocator.Persistent);
            additionalData = new NativeReference<(UnityObjRef<Texture2D> texture, Rect scissor)>(Allocator.Persistent);
            mesh = new Mesh();
            data = new NativeReference<Mesh.MeshData>(Allocator.Persistent);
            drawCmdBuffer = new GraphicsBuffer(GraphicsBuffer.Target.IndirectArguments, 1000, GraphicsBuffer.IndirectDrawIndexedArgs.size);
            drawCmdBuffer.name = "ImGuiDrawCmdBuffer";
            
            // Setup Dear ImGui context
            ImGui.CheckVersion();
            ImGui.CreateContext();
            var io = ImGui.GetIO();
            io->ConfigFlags |= ImGuiConfigFlags.NavEnableKeyboard;     // Enable Keyboard Controls
            io->ConfigFlags |= ImGuiConfigFlags.NavEnableGamepad;      // Enable Gamepad Controls

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
            data.Value = dataArray[0];
            draw_cmds.Clear();
            ImGui_ImplVulkan_RenderDrawData(ImGui.GetDrawData(), data, (NativeList<GraphicsBuffer.IndirectDrawIndexedArgs>*)UnsafeUtility.AddressOf(ref draw_cmds), additionalData);
            Mesh.ApplyAndDisposeWritableMeshData(dataArray, mesh, MeshUpdateFlags.DontRecalculateBounds | MeshUpdateFlags.DontResetBoneBounds);
            mesh.bounds = new Bounds(Vector3.zero, Vector3.one * 1000);
            drawCmdBuffer.SetCounterValue((uint)draw_cmds.Length);
            drawCmdBuffer.SetData(draw_cmds.AsArray());
            material.mainTexture = additionalData.Value.texture;
        }

        public override unsafe void Execute(ScriptableRenderContext context,
            ref RenderingData renderingData)
        {
            
            //Get a CommandBuffer from pool.
            CommandBuffer cmd = CommandBufferPool.Get("ImGuiRenderPass");
            for (int i = 0; i < draw_cmds.Length; i++)
            {
                // cmd.EnableScissorRect(additionalData.Value.scissor);
                cmd.DrawMeshInstancedIndirect(mesh, 0, material, -1, drawCmdBuffer, i * GraphicsBuffer.IndirectDrawIndexedArgs.size);
            }
            // cmd.DisableScissorRect();
            
            //Execute the command buffer and release it back to the pool.
            context.ExecuteCommandBuffer(cmd);
            CommandBufferPool.Release(cmd);
        }


        [BurstCompile]
        unsafe static void ImGui_ImplVulkan_RenderDrawData(ImDrawData* draw_data, NativeReference<Mesh.MeshData> data,
            NativeList<GraphicsBuffer.IndirectDrawIndexedArgs>* draw_cmds,
            NativeReference<(UnityObjRef<Texture2D> texture, Rect scissor)> additionalData)
        {
            // Avoid rendering when minimized, scale coordinates for retina displays (screen coordinates != framebuffer coordinates)
            var fb_width = (int)(draw_data->DisplaySize.x * draw_data->FramebufferScale.x);
            var fb_height = (int)(draw_data->DisplaySize.y * draw_data->FramebufferScale.y);
            if (fb_width <= 0 || fb_height <= 0)
                return;

            // Allocate array to store enough vertex/index buffers
            var meshData = data.Value;
            if (draw_data->TotalVtxCount > 0)
            {
                // Create or resize the vertex/index buffers
                meshData.SetIndexBufferParams(draw_data->TotalIdxCount, IndexFormat.UInt16);
                meshData.SetVertexBufferParams(draw_data->TotalVtxCount,
                    new VertexAttributeDescriptor(VertexAttribute.Position, VertexAttributeFormat.Float32, 2),
                    new VertexAttributeDescriptor(VertexAttribute.Color, VertexAttributeFormat.UNorm8, 4),
                    new VertexAttributeDescriptor(VertexAttribute.TexCoord0, VertexAttributeFormat.Float32, 2)
                );

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
            float2 clip_off = draw_data->DisplayPos; // (0,0) unless using multi-viewports
            float2 clip_scale = draw_data->FramebufferScale; // (1,1) unless using retina display which are often (2,2)

            // Render command lists
            // (Because we merged all buffers into a single one, we maintain our own offset into them)
            int global_vtx_offset = 0;
            int global_idx_offset = 0;
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
                        var clip_min = new float2((pcmd.ClipRect.x - clip_off.x) * clip_scale.x,
                            (pcmd.ClipRect.y - clip_off.y) * clip_scale.y);
                        var clip_max = new float2((pcmd.ClipRect.z - clip_off.x) * clip_scale.x,
                            (pcmd.ClipRect.w - clip_off.y) * clip_scale.y);

                        // Clamp to viewport as vkCmdSetScissor() won't accept values that are off bounds
                        clip_min = math.max(clip_min, new float2(0, 0));
                        clip_max = math.min(clip_max, new float2(fb_width, fb_height));
                        if (clip_max.x <= clip_min.x || clip_max.y <= clip_min.y)
                            continue;

                        // Bind DescriptorSet with font or user texture
                        // VkDescriptorSet desc_set[1] = { (VkDescriptorSet)pcmd->TextureId };
                        // if (sizeof(ImTextureID) < sizeof(ImU64))
                        // {
                        //     // We don't support texture switches if ImTextureID hasn't been redefined to be 64-bit. Do a flaky check that other textures haven't been used.
                        //     IM_ASSERT(pcmd->TextureId == (ImTextureID)bd->FontDescriptorSet);
                        //     desc_set[0] = bd->FontDescriptorSet;
                        // }
                        // vkCmdBindDescriptorSets(command_buffer, VK_PIPELINE_BIND_POINT_GRAPHICS, bd->PipelineLayout, 0, 1, desc_set, 0, nullptr);
                        var value = additionalData.Value;
                        var textureId = pcmd.GetTexID();
                        if (cmd_i == 0)
                            value.scissor = new Rect(clip_min, clip_max - clip_min);
                        value.texture = UnsafeUtility.As<ImTextureID, UnityObjRef<Texture2D>>(ref textureId);
                        additionalData.Value = value;

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
                Object.Destroy(material);
                Object.Destroy(mesh);
            }
            else
            {
                Object.DestroyImmediate(material);
                Object.DestroyImmediate(mesh);
            }
            
            draw_cmds.Dispose();
            additionalData.Dispose();
            data.Dispose();
        }
    }
}