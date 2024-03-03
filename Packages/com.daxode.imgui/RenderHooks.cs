using System;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

namespace com.daxode.imgui
{
    internal static class RenderHooks
    {
        internal struct RendererUserData
        {
            public UnityObjRef<Texture2D> FontTexture;
            public int LastFrameCount;
        }

        public static unsafe bool IsNewFrame => GetBackendData()->LastFrameCount != Time.renderedFrameCount;

        internal static unsafe RendererUserData* GetBackendData() 
            => ImGui.GetCurrentContext() != null ? (RendererUserData*)ImGui.GetIO()->BackendRendererUserData : null;

        static unsafe bool CreateFontsTexture()
        {
            // Build texture atlas
            var io = ImGui.GetIO();
            var bd = GetBackendData();
            io->Fonts->GetTexDataAsRGBA32(out var pixels, out var width, out var height, out var bytesperpixel);

            // Upload texture to graphics system
            // (Bilinear sampling is required by default. Set 'io.Fonts->Flags |= ImFontAtlasFlags_NoBakedLines' or 'style.AntiAliasedLinesUseTex = false' to allow point/nearest sampling)
            var texture = new Texture2D(width, height, TextureFormat.RGBA32, false, true);
            texture.name = "ImGuiFontTexture";
            bd->FontTexture = texture;
            texture.LoadRawTextureData((IntPtr)pixels, width * height * bytesperpixel);
            texture.Apply();
            
            // Store our identifier
            io->Fonts->SetTexID(bd->FontTexture);

            return true;
        }
        
        static bool CreateDeviceObjects() 
            => CreateFontsTexture();

        internal static unsafe void NewFrame()
        {
            var bd = GetBackendData();
            Debug.Assert(bd != null, "Did you call Init()?");
            if (!bd->FontTexture.IsValid)
                CreateDeviceObjects();
            bd->LastFrameCount = Time.renderedFrameCount;
        }

        internal static unsafe bool Init()
        {
            var io = ImGui.GetIO();
            Debug.Assert(io->BackendRendererUserData == null, "Already initialized a renderer backend!");

            // Setup backend capabilities flags
            var bd = (RendererUserData*) UnsafeUtility.Malloc(sizeof(RendererUserData), UnsafeUtility.AlignOf<RendererUserData>(), Allocator.Persistent);
            UnsafeUtility.MemClear(bd, sizeof(RendererUserData));
            bd->LastFrameCount = -1;
            io->BackendRendererUserData = bd;
            io->BackendRendererName = new NativeText("Unity Renderer", Allocator.Persistent).GetUnsafePtr();

            return true;
        }

        static unsafe void DestroyFontsTexture()
        {
            var io = ImGui.GetIO();
            var bd = GetBackendData();
            if (!bd->FontTexture.IsValid) 
                return;
#if UNITY_EDITOR
            UnityEngine.Object.DestroyImmediate(bd->FontTexture.Value);
#else
            UnityEngine.Object.Destroy(bd->FontTexture.Value);
#endif
            io->Fonts->SetTexID(default);
            bd->FontTexture = default;
        }
        
        static void DestroyDeviceObjects()
        {
            DestroyFontsTexture();
        }

        internal static unsafe void Shutdown()
        {
            var bd = GetBackendData();
            if (bd == null) 
                return;
            Debug.Assert(bd != null, "No renderer backend to shutdown, or already shutdown?");
            var io = ImGui.GetIO();
            
            DestroyDeviceObjects();
            io->BackendRendererName = null; // currently leaks memory
            io->BackendRendererUserData = null;
            ImGui.GetIO();
            UnsafeUtility.Free(bd, Allocator.Persistent);
        }
    }
}