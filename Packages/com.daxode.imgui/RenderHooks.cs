﻿using System;
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
        }

        internal static unsafe RendererUserData* GetBackendData() 
            => ImGui.GetCurrentContext() != null ? (RendererUserData*)ImGui.GetIO()->BackendRendererUserData : null;

        static unsafe bool CreateFontsTexture()
        {
            // Build texture atlas
            var io = ImGui.GetIO();
            var bd = GetBackendData();
            io->Fonts->GetTexDataAsAlpha8(out var pixels, out var width, out var height, out var bytesperpixel);

            // Upload texture to graphics system
            // (Bilinear sampling is required by default. Set 'io.Fonts->Flags |= ImFontAtlasFlags_NoBakedLines' or 'style.AntiAliasedLinesUseTex = false' to allow point/nearest sampling)
            var texture = new Texture2D(width, height, TextureFormat.Alpha8, false, true);
            texture.name = "ImGuiFontTexture";
            bd->FontTexture = texture;
            texture.LoadRawTextureData((IntPtr)pixels, width * height * bytesperpixel);
            texture.Apply();
            
            // Store our identifier
            io->Fonts->SetTexID(UnsafeUtility.As<int, ImTextureID>(ref bd->FontTexture.InstanceID));

            return true;
        }
        
        static bool CreateDeviceObjects() 
            => CreateFontsTexture();

        internal static unsafe void NewFrame()
        {
            var bd = GetBackendData();
            Debug.Assert(bd != null, "Did you call Init()?");
            if (bd->FontTexture.InstanceID == 0)
                CreateDeviceObjects();
        }

        internal static unsafe bool Init()
        {
            var io = ImGui.GetIO();
            Debug.Assert(io->BackendRendererUserData == null, "Already initialized a renderer backend!");

            // Setup backend capabilities flags
            var bd = (RendererUserData*) UnsafeUtility.Malloc(sizeof(RendererUserData), UnsafeUtility.AlignOf<RendererUserData>(), Allocator.Persistent);
            UnsafeUtility.MemClear(bd, sizeof(RendererUserData));
            io->BackendRendererUserData = bd;
            io->BackendRendererName = (char*) new NativeText("imgui_impl_opengl2", Allocator.Persistent).GetUnsafePtr();

            return true;
        }

        static unsafe void DestroyFontsTexture()
        {
            var io = ImGui.GetIO();
            var bd = GetBackendData();
            if (bd->FontTexture.InstanceID == 0) 
                return;
            UnityEngine.Object.Destroy(bd->FontTexture.Value);
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
            Debug.Assert(bd != null, "No renderer backend to shutdown, or already shutdown?");
            var io = ImGui.GetIO();
            
            DestroyDeviceObjects();
            io->BackendRendererName = null; // currently leaks memory
            io->BackendRendererUserData = null;
            UnsafeUtility.Free(bd, Allocator.Persistent);
        }
    }
}