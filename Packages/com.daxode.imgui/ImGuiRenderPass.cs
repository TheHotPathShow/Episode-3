using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace com.daxode.imgui
{
    public class ImGuiRenderPass : ScriptableRenderPass
    {
        Mesh mesh;
        Material material;
        NativeList<GraphicsBuffer.IndirectDrawIndexedArgs> draw_cmds;
        NativeReference<(UnityObjRef<Texture2D> texture, Rect scissor)> additionalData;
        NativeReference<Mesh.MeshData> data;
        RTHandle m_CameraColorTarget;
        GraphicsBuffer drawCmdBuffer;
        
        public ImGuiRenderPass(Material material)
        {
            this.material = material;
            renderPassEvent = RenderPassEvent.AfterRendering;
            draw_cmds = new NativeList<GraphicsBuffer.IndirectDrawIndexedArgs>(100, Allocator.Persistent);
            additionalData = new NativeReference<(UnityObjRef<Texture2D> texture, Rect scissor)>(Allocator.Persistent);
            mesh = new Mesh();
            data = new NativeReference<Mesh.MeshData>(Allocator.Persistent);
            drawCmdBuffer = new GraphicsBuffer(GraphicsBuffer.Target.IndirectArguments, 1000, GraphicsBuffer.IndirectDrawIndexedArgs.size);
            drawCmdBuffer.name = "ImGuiDrawCmdBuffer";
        }

        public override void Configure(CommandBuffer cmd,
            RenderTextureDescriptor cameraTextureDescriptor)
        {
            // // Set the texture size to be the same as the camera target size.
            // textureDescriptor.width = cameraTextureDescriptor.width;
            // textureDescriptor.height = cameraTextureDescriptor.height;
            //
            // // Check if the descriptor has changed, and reallocate the RTHandle if necessary
            // RenderingUtils.ReAllocateIfNeeded(ref textureHandle, textureDescriptor);
        }

        public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
        {
            // Set the textureHandle as the color target of the camera.
            // ConfigureInput(ScriptableRenderPassInput.Color);
            // ConfigureTarget(m_CameraColorTarget);
            // ConfigureClear(ClearFlag.None, Color.black);
        }

        public unsafe override void Execute(ScriptableRenderContext context,
            ref RenderingData renderingData)
        {
            if (ImGui.GetCurrentContext() == null)
                return;
            
            // Rendering
            ImGui.Render();
            // int display_w, display_h;
            // glfwGetFramebufferSize(window, &display_w, &display_h);
            // glViewport(0, 0, display_w, display_h);
            // glClearColor(clear_color.x * clear_color.w, clear_color.y * clear_color.w, clear_color.z * clear_color.w, clear_color.w);
            // glClear(GL_COLOR_BUFFER_BIT);

            
            var dataArray = Mesh.AllocateWritableMeshData(1);
            data.Value = dataArray[0];
            draw_cmds.Clear();
            var draw_data = ImGui.GetDrawData();
            ImGui_ImplVulkan_RenderDrawData(draw_data, data, (NativeList<GraphicsBuffer.IndirectDrawIndexedArgs>*)UnsafeUtility.AddressOf(ref draw_cmds), additionalData);
            Mesh.ApplyAndDisposeWritableMeshData(dataArray, mesh, meshUpdateFlags);
            mesh.bounds = new Bounds(Vector3.zero, Vector3.one * 1000);
            drawCmdBuffer.SetCounterValue((uint)draw_cmds.Length);
            drawCmdBuffer.SetData(draw_cmds.AsArray());
            
            // foreach (var drawCmd in draw_cmds) 
            //     Debug.Log($"drawCmd: {drawCmd.indexCountPerInstance} {drawCmd.instanceCount} {drawCmd.startIndex} {drawCmd.baseVertexIndex} {drawCmd.startInstance}");
            
            material.mainTexture = additionalData.Value.texture;
            
            //Get a CommandBuffer from pool.
            CommandBuffer cmd = CommandBufferPool.Get("ImGuiRenderPass");
            // cmd.SetViewProjectionMatrices(renderingData.cameraData.camera.worldToCameraMatrix, renderingData.cameraData.camera.projectionMatrix);
            Debug.Log($"Display Pos: {draw_data->DisplayPos} Display Size: {draw_data->DisplaySize} Scissor: {additionalData.Value.scissor}");
            cmd.SetViewport(new Rect(draw_data->DisplayPos, draw_data->DisplaySize));
            cmd.EnableScissorRect(additionalData.Value.scissor);
            cmd.DrawMeshInstancedIndirect(mesh, 0, material, -1, drawCmdBuffer);
            cmd.DisableScissorRect();
            
            // renderingData.cameraData.camera.AddCommandBuffer(CameraEvent.AfterEverything, cmd);
            // glfwMakeContextCurrent(window);
            // glfwSwapBuffers(window);


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
            int fb_width = (int)(draw_data->DisplaySize.x * draw_data->FramebufferScale.x);
            int fb_height = (int)(draw_data->DisplaySize.y * draw_data->FramebufferScale.y);
            if (fb_width <= 0 || fb_height <= 0)
                return;

            // Allocate array to store enough vertex/index buffers
            var meshData = data.Value;

            if (draw_data->TotalVtxCount > 0)
            {
                // Create or resize the vertex/index buffers
                var vertex_size = draw_data->TotalVtxCount;
                var index_size = draw_data->TotalIdxCount;

                meshData.SetIndexBufferParams(index_size, IndexFormat.UInt16);
                meshData.SetVertexBufferParams(vertex_size,
                    new VertexAttributeDescriptor(VertexAttribute.Position, VertexAttributeFormat.Float32, 2),
                    new VertexAttributeDescriptor(VertexAttribute.Color, VertexAttributeFormat.UNorm8, 4),
                    new VertexAttributeDescriptor(VertexAttribute.TexCoord0, VertexAttributeFormat.Float32, 2)
                );

                // Upload vertex/index data into a single contiguous GPU buffer
                var indexData = meshData.GetIndexData<ImDrawIdx>();
                var vertexData = meshData.GetVertexData<ImDrawVert>();
                ImDrawVert* vtx_dst = (ImDrawVert*)vertexData.GetUnsafePtr();
                ImDrawIdx* idx_dst = (ImDrawIdx*)indexData.GetUnsafePtr();
                for (int n = 0; n < draw_data->CmdListsCount; n++)
                {
                    ref var cmd_list = ref draw_data->CmdLists[n];
                    // for (int i = 0; i < cmd_list.Value->VtxBuffer.Size; i++) 
                    //     Debug.Log(cmd_list.Value->VtxBuffer[i].pos.xy);
                    
                    UnsafeUtility.MemCpy(vtx_dst, cmd_list.Value->VtxBuffer.Data,
                        cmd_list.Value->VtxBuffer.Size * sizeof(ImDrawVert));
                    UnsafeUtility.MemCpy(idx_dst, cmd_list.Value->IdxBuffer.Data,
                        cmd_list.Value->IdxBuffer.Size * sizeof(ImDrawIdx));
                    vtx_dst += cmd_list.Value->VtxBuffer.Size;
                    idx_dst += cmd_list.Value->IdxBuffer.Size;
                }
            }

            // Setup desired Vulkan state
            ImGui_ImplVulkan_SetupRenderState(draw_data, fb_width, fb_height);

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
                Debug.Log($"CmdListsCount: {cmd_list.Value->CmdBuffer.Size}");
                for (int cmd_i = 0; cmd_i < cmd_list.Value->CmdBuffer.Size; cmd_i++)
                {
                    ref var pcmd = ref cmd_list.Value->CmdBuffer[cmd_i];
                    if (pcmd.UserCallback.Value != null)
                    {
                        // User callback, registered via ImDrawList::AddCallback()
                        // (ImDrawCallback_ResetRenderState is a special callback value used by the user to request the renderer to reset render state.)
                        if (pcmd.UserCallback.Value == ImDrawCallback.ResetRenderState)
                            ImGui_ImplVulkan_SetupRenderState(draw_data, fb_width, fb_height);
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
            meshData.SetSubMesh(0, new SubMeshDescriptor(0, draw_data->TotalIdxCount, MeshTopology.Triangles), meshUpdateFlags);
        }

        const MeshUpdateFlags meshUpdateFlags = MeshUpdateFlags.DontRecalculateBounds | MeshUpdateFlags.DontResetBoneBounds; 
        
        unsafe static void ImGui_ImplVulkan_SetupRenderState(ImDrawData* drawData, int fbWidth, int fbHeight)
            {
                // Setup render state: alpha-blending enabled, no face culling, no depth testing, scissor enabled, vertex/texcoord/color pointers, polygon fill.
                // glEnable(GL_BLEND);
                // glBlendFunc(GL_SRC_ALPHA, GL_ONE_MINUS_SRC_ALPHA);
                // //glBlendFuncSeparate(GL_SRC_ALPHA, GL_ONE_MINUS_SRC_ALPHA, GL_ONE, GL_ONE_MINUS_SRC_ALPHA); // In order to composite our output buffer we need to preserve alpha
                // glDisable(GL_CULL_FACE);
                // glDisable(GL_DEPTH_TEST);
                // glDisable(GL_STENCIL_TEST);
                // glDisable(GL_LIGHTING);
                // glDisable(GL_COLOR_MATERIAL);
                // glEnable(GL_SCISSOR_TEST);
                // glEnableClientState(GL_VERTEX_ARRAY);
                // glEnableClientState(GL_TEXTURE_COORD_ARRAY);
                // glEnableClientState(GL_COLOR_ARRAY);
                // glDisableClientState(GL_NORMAL_ARRAY);
                // glEnable(GL_TEXTURE_2D);
                // glPolygonMode(GL_FRONT_AND_BACK, GL_FILL);
                // glShadeModel(GL_SMOOTH);
                // glTexEnvi(GL_TEXTURE_ENV, GL_TEXTURE_ENV_MODE, GL_MODULATE);

                // If you are using this code with non-legacy OpenGL header/contexts (which you should not, prefer using imgui_impl_opengl3.cpp!!),
                // you may need to backup/reset/restore other state, e.g. for current shader using the commented lines below.
                // (DO NOT MODIFY THIS FILE! Add the code in your calling function)
                //   GLint last_program;
                //   glGetIntegerv(GL_CURRENT_PROGRAM, &last_program);
                //   glUseProgram(0);
                //   ImGui_ImplOpenGL2_RenderDrawData(...);
                //   glUseProgram(last_program)
                // There are potentially many more states you could need to clear/setup that we can't access from default headers.
                // e.g. glBindBuffer(GL_ARRAY_BUFFER, 0), glDisable(GL_TEXTURE_CUBE_MAP).

                // Setup viewport, orthographic projection matrix
                // Our visible imgui space lies from draw_data->DisplayPos (top left) to draw_data->DisplayPos+data_data->DisplaySize (bottom right). DisplayPos is (0,0) for single viewport apps.
                // glViewport(0, 0, (GLsizei)fb_width, (GLsizei)fb_height);
                // glMatrixMode(GL_PROJECTION);
                // glPushMatrix();
                // glLoadIdentity();
                // glOrtho(draw_data->DisplayPos.x, draw_data->DisplayPos.x + draw_data->DisplaySize.x, draw_data->DisplayPos.y + draw_data->DisplaySize.y, draw_data->DisplayPos.y, -1.0f, +1.0f);
                // glMatrixMode(GL_MODELVIEW);
                // glPushMatrix();
                // glLoadIdentity();
            }

            public void Dispose()
            {
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

            public void SetTarget(RTHandle rendererCameraColorTargetHandle) 
                => m_CameraColorTarget = rendererCameraColorTargetHandle;
    }
}