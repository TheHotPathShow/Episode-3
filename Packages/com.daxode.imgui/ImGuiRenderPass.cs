using System.Collections.Generic;
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
        Material material;
        
        public ImGuiRenderPass(Material material)
        {
            this.material = material;
            renderPassEvent = RenderPassEvent.AfterRenderingPostProcessing;
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
            // ConfigureTarget(renderingData.cameraData.renderer.cameraColorTargetHandle);
            // ConfigureClear(ClearFlag.None, Color.black);
        }

        public unsafe override void Execute(ScriptableRenderContext context,
            ref RenderingData renderingData)
        {
            // Rendering
            ImGui.Render();
            // int display_w, display_h;
            // glfwGetFramebufferSize(window, &display_w, &display_h);
            // glViewport(0, 0, display_w, display_h);
            // glClearColor(clear_color.x * clear_color.w, clear_color.y * clear_color.w, clear_color.z * clear_color.w, clear_color.w);
            // glClear(GL_COLOR_BUFFER_BIT);
            
            CommandBuffer cmd = CommandBufferPool.Get();
            ImGui_ImplOpenGL2_RenderDrawData(ImGui.GetDrawData(), cmd);

            // glfwMakeContextCurrent(window);
            // glfwSwapBuffers(window);
            
            //Get a CommandBuffer from pool.

            var cameraTargetHandle =
                renderingData.cameraData.renderer.cameraColorTargetHandle;
            
            
            // // Blit from the camera target to the temporary render texture,
            // // using the first shader pass.
            // Blit(cmd, cameraTargetHandle, textureHandle, material, 0);
            // // Blit from the temporary render texture to the camera target,
            // // using the second shader pass.
            // Blit(cmd, textureHandle, cameraTargetHandle, material, 1);
            // cmd.Draw( cameraTargetHandle, cameraTargetHandle, material, 0);
            
            //Execute the command buffer and release it back to the pool.
            context.ExecuteCommandBuffer(cmd);
            CommandBufferPool.Release(cmd);
        }
        
        unsafe void ImGui_ImplOpenGL2_RenderDrawData(ImDrawData* draw_data, CommandBuffer cmd)
        {
            // Avoid rendering when minimized, scale coordinates for retina displays (screen coordinates != framebuffer coordinates)
            int fb_width = (int)(draw_data->DisplaySize.x * draw_data->FramebufferScale.x);
            int fb_height = (int)(draw_data->DisplaySize.y * draw_data->FramebufferScale.y);
            if (fb_width == 0 || fb_height == 0)
                return;
            
            // Backup GL state
            // int last_texture; glGetIntegerv(GL_TEXTURE_BINDING_2D, &last_texture);
            // int last_polygon_mode[2]; glGetIntegerv(GL_POLYGON_MODE, last_polygon_mode);
            // int last_viewport[4]; glGetIntegerv(GL_VIEWPORT, last_viewport);
            // int last_scissor_box[4]; glGetIntegerv(GL_SCISSOR_BOX, last_scissor_box);
            // GLint last_shade_model; glGetIntegerv(GL_SHADE_MODEL, &last_shade_model);
            // GLint last_tex_env_mode; glGetTexEnviv(GL_TEXTURE_ENV, GL_TEXTURE_ENV_MODE, &last_tex_env_mode);
            // glPushAttrib(GL_ENABLE_BIT | GL_COLOR_BUFFER_BIT | GL_TRANSFORM_BIT);

            // Setup desired GL state
            ImGui_ImplOpenGL2_SetupRenderState(draw_data, fb_width, fb_height);

            // Will project scissor/clipping rectangles into framebuffer space
            var clip_off = draw_data->DisplayPos;         // (0,0) unless using multi-viewports
            var clip_scale = draw_data->FramebufferScale; // (1,1) unless using retina display which are often (2,2)
            
            var meshes = new Mesh[draw_data->CmdListsCount];
            for (int i = 0; i < meshes.Length; i++)
                meshes[i] = new Mesh();
            
            var meshDataArray = Mesh.AllocateWritableMeshData(draw_data->CmdListsCount);
            var materials = new List<Material>(draw_data->CmdListsCount);
            // var defaultMaterial = new Material(Shader.Find("Universal Render Pipeline/Unlit"));
            var defaultMaterial = material;
            
            // Render command lists
            for (int i = 0; i < draw_data->CmdListsCount; i++)
            {
                ImDrawList* cmd_list = draw_data->CmdLists[i].Value;
                var vtx_buffer = cmd_list->VtxBuffer;
                var idx_buffer = cmd_list->IdxBuffer;

                var meshData = meshDataArray[i];
                meshData.SetIndexBufferParams(idx_buffer.Size, IndexFormat.UInt16);
                meshData.SetVertexBufferParams(vtx_buffer.Size,
                    new VertexAttributeDescriptor(VertexAttribute.Position, VertexAttributeFormat.Float32, 2),
                    new VertexAttributeDescriptor(VertexAttribute.TexCoord0, VertexAttributeFormat.Float32, 2, 1),
                    new VertexAttributeDescriptor(VertexAttribute.Color, VertexAttributeFormat.UNorm8, 4, 2));
                var indexData = meshData.GetIndexData<ushort>();
                UnsafeUtility.MemCpy(indexData.GetUnsafePtr(), idx_buffer.Data, idx_buffer.Size * sizeof(ushort));
                var vertexData = meshData.GetVertexData<float2>();
                Debug.Log(draw_data->DisplaySize);
                UnsafeUtility.MemCpyStride(vertexData.GetUnsafePtr(), sizeof(float2), vtx_buffer.Data, sizeof(ImDrawVert), sizeof(float2),vertexData.Length);
                foreach (var pos in vertexData) Debug.Log(pos/draw_data->DisplaySize);
                vertexData = meshData.GetVertexData<float2>(1);
                // foreach (var pos in vertexData) Debug.Log(pos);
                UnsafeUtility.MemCpyStride(vertexData.GetUnsafePtr(), sizeof(float2), vtx_buffer.Data+sizeof(float2), sizeof(ImDrawVert), sizeof(float2),vertexData.Length);
                var colorData = meshData.GetVertexData<Color32>(2);
                // foreach (var pos in colorData) Debug.Log(pos);
                UnsafeUtility.MemCpyStride(colorData.GetUnsafePtr(), sizeof(Color32), vtx_buffer.Data + sizeof(float2) * 2, sizeof(ImDrawVert), sizeof(Color32), colorData.Length);

                
                // glVertexPointer(2, GL_FLOAT, sizeof(ImDrawVert), (const GLvoid*)((const char*)vtx_buffer + offsetof(ImDrawVert, pos)));
                // glTexCoordPointer(2, GL_FLOAT, sizeof(ImDrawVert), (const GLvoid*)((const char*)vtx_buffer + offsetof(ImDrawVert, uv)));
                // glColorPointer(4, GL_UNSIGNED_BYTE, sizeof(ImDrawVert), (const GLvoid*)((const char*)vtx_buffer + offsetof(ImDrawVert, col)));
                meshData.subMeshCount = cmd_list->CmdBuffer.Size;
                for (int cmd_i = 0; cmd_i < cmd_list->CmdBuffer.Size; cmd_i++)
                {
                    var pcmd = (ImDrawCmd*)UnsafeUtility.AddressOf(ref cmd_list->CmdBuffer[cmd_i]);
                    if (pcmd->UserCallback.Value != null)
                    {
                        // User callback, registered via ImDrawList::AddCallback()
                        // (ImDrawCallback_ResetRenderState is a special callback value used by the user to request the renderer to reset render state.)
                        if (pcmd->UserCallback.Value == ImDrawCallback.ResetRenderState)
                            ImGui_ImplOpenGL2_SetupRenderState(draw_data, fb_width, fb_height);
                        else
                            pcmd->UserCallback.Value(cmd_list, pcmd);
                    }
                    else
                    {
                        // Project scissor/clipping rectangles into framebuffer space
                        float2 clip_min = new float2((pcmd->ClipRect.x - clip_off.x) * clip_scale.x,
                            (pcmd->ClipRect.y - clip_off.y) * clip_scale.y);
                        float2 clip_max = new float2((pcmd->ClipRect.z - clip_off.x) * clip_scale.x,
                            (pcmd->ClipRect.w - clip_off.y) * clip_scale.y);
                        if (clip_max.x <= clip_min.x || clip_max.y <= clip_min.y)
                            continue;

                        // Apply scissor/clipping rectangle (Y is inverted in OpenGL)
                        // glScissor((int)clip_min.x, (int)((float)fb_height - clip_max.y), (int)(clip_max.x - clip_min.x), (int)(clip_max.y - clip_min.y));

                        // Bind texture, Draw
                        var tex = pcmd->GetTexID();
                        var textureRef = UnsafeUtility.As<ImTextureID, UnityObjRef<Texture2D>>(ref tex);
                        materials.Add(new Material(defaultMaterial){mainTexture = textureRef.Value});
                        // Debug.Log(UnsafeUtility.As<ImTextureID, UnityObjRef<Texture2D>>(ref tex).Value.name);
                        // glBindTexture(GL_TEXTURE_2D, (GLuint)(intptr_t)pcmd->GetTexID());
                        // Debug.Log($"Elems: {(int)pcmd->ElemCount}, Size: {(sizeof(ImDrawIdx) == 2 ? "ushort" : "uint")}, Idx: {((int)pcmd->IdxOffset)}");
                        // glDrawElements(GL_TRIANGLES, (GLsizei)pcmd->ElemCount, sizeof(ImDrawIdx) == 2 ? GL_UNSIGNED_SHORT : GL_UNSIGNED_INT, idx_buffer + pcmd->IdxOffset);
                        meshData.SetSubMesh(cmd_i, new SubMeshDescriptor((int)pcmd->IdxOffset, (int)pcmd->ElemCount), MeshUpdateFlags.DontRecalculateBounds | MeshUpdateFlags.DontValidateIndices | MeshUpdateFlags.DontResetBoneBounds | MeshUpdateFlags.DontNotifyMeshUsers);
                    }
                }
            }
            
            Mesh.ApplyAndDisposeWritableMeshData(meshDataArray, meshes, MeshUpdateFlags.DontRecalculateBounds | MeshUpdateFlags.DontValidateIndices | MeshUpdateFlags.DontResetBoneBounds | MeshUpdateFlags.DontNotifyMeshUsers);
            foreach (var mesh in meshes) 
                mesh.bounds = new Bounds(new float3(0, 0, 0), new float3(float.MaxValue, float.MaxValue, float.MaxValue));
            
            var materialIdx = 0;
            foreach (var mesh in meshes)
            {
                materialIdx += 1;
                for (int submeshIndex = 0; submeshIndex < mesh.subMeshCount; submeshIndex++)
                {
                    cmd.DrawMesh(mesh, float4x4.identity, materials[materialIdx], submeshIndex);
                }
            }

            foreach (var mat in materials)
            {
                Object.Destroy(mat);
            }
        }
        
        unsafe void ImGui_ImplOpenGL2_SetupRenderState(ImDrawData* drawData, int fbWidth, int fbHeight)
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
#if UNITY_EDITOR
            if (EditorApplication.isPlaying)
            {
                Object.Destroy(material);
            }
            else
            {
                Object.DestroyImmediate(material);
            }
#else
            Object.Destroy(material);
#endif
        }
    }
}