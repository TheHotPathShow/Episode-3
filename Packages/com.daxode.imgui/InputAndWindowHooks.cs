using System.Runtime.InteropServices;
using AOT;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Mathematics;
using UnityEngine;

namespace com.daxode.imgui
{
    internal static class InputAndWindowHooks
    {
        static unsafe PlatformData* GetBackendData() 
            => ImGui.GetCurrentContext() != null ? (PlatformData*)ImGui.GetIO()->BackendPlatformUserData : null;

        struct PlatformData
        {
            public bool InstalledCallbacks;
            public int Window;
            public double Time;
            public float2 LastValidMousePos;
            public int MouseWindow;
            public bool HasEnteredAlready;
        }
        
        internal static unsafe void NewFrame()
        {
            var io = ImGui.GetIO();
            var bd = GetBackendData();
            Debug.Assert(bd != null, "Did you call InitForXXX()?");

            var cam = Camera.main;
            if (cam == null)
                return;
            
            // Setup display size (every frame to accommodate for window resizing)
            int2 display = new int2(Screen.currentResolution.width, Screen.currentResolution.height);
            io->DisplaySize = new float2(cam.pixelWidth, cam.pixelHeight);
            if (math.all(io->DisplaySize > 0))
                io->DisplayFramebufferScale = new float2(display.x / io->DisplaySize.x, display.y / io->DisplaySize.y);
            io->FontGlobalScale = 1f;

            // Setup time step
            // (Accept glfwGetTime() not returning a monotonically increasing value. Seems to happens on disconnecting peripherals and probably on VMs and Emscripten, see #6491, #6189, #6114, #3644)
            double current_time = Time.timeAsDouble;
            if (current_time <= bd->Time)
                current_time = bd->Time + 0.00001f;
            io->DeltaTime = bd->Time > 0.0 ? (float)(current_time - bd->Time) : (float)(1.0f / 60.0f);
            bd->Time = current_time;
            
            UpdateMouseData();
            // UpdateMouseCursor();

            // Update game controllers (if enabled and available)
            // UpdateGamepads();
            
            // -WindowFocusCallback
            // -CursorEnterCallback
            // -CursorPosCallback
            ImGuiModFlags mods = 0;
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                mods |= ImGuiModFlags.Shift;
            if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
                mods |= ImGuiModFlags.Ctrl;
            if (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt))
                mods |= ImGuiModFlags.Alt;
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0))
                MouseButtonCallback(ImGuiMouseButton.Left, Input.GetMouseButtonDown(0) ? (byte)1 : (byte)0, (int)mods);
            if (Input.GetMouseButtonDown(1) || Input.GetMouseButtonUp(1))
                MouseButtonCallback(ImGuiMouseButton.Right, Input.GetMouseButtonDown(1) ? (byte)1 : (byte)0, (int)mods);
            if (Input.GetMouseButtonDown(2) || Input.GetMouseButtonUp(2))
                MouseButtonCallback(ImGuiMouseButton.Middle, Input.GetMouseButtonDown(2) ? (byte)1 : (byte)0, (int)mods);
            
            if (Input.mouseScrollDelta.magnitude != 0.0f)
                io->AddMouseWheelEvent(Input.mouseScrollDelta.x, Input.mouseScrollDelta.y);

            if (Input.mousePosition.x >= 0 && Input.mousePosition.x <= io->DisplaySize.x &&
                Input.mousePosition.y >= 0 && Input.mousePosition.y <= io->DisplaySize.y)
            {
                if (!bd->HasEnteredAlready)
                {
                    CursorEnterCallback(true);
                    ImGui.GetIO()->AddFocusEvent(1);
                    bd->HasEnteredAlready = true;
                }
            } else if (bd->HasEnteredAlready)
            {
                CursorEnterCallback(false);
                ImGui.GetIO()->AddFocusEvent(0);
                bd->HasEnteredAlready = false;
            }
            
            // -MouseButtonCallback
            // -ScrollCallback
            // -KeyCallback
            // -CharCallback
            // -MonitorCallback
        }
        
        static unsafe void CursorEnterCallback(bool entered)
        {
            var bd = GetBackendData();
            var io = ImGui.GetIO();
            if (entered)
            {
                bd->MouseWindow = bd->Window;
                io->AddMousePosEvent(bd->LastValidMousePos.x, bd->LastValidMousePos.y);
            }
            else if (bd->MouseWindow == bd->Window)
            {
                bd->LastValidMousePos = io->MousePos;
                bd->MouseWindow = 0;
                io->AddMousePosEvent(-float.MaxValue, -float.MaxValue);
            }
        }
        
        unsafe static void MouseButtonCallback(ImGuiMouseButton button, byte pressedDown, int mods)
        {
            // Debug.Log($"Button: {button}, Down: {pressedDown}, Mods: {mods} At: {ImGui.GetIO()->MousePos}");
            // UpdateKeyModifiers(window);
            var io = ImGui.GetIO();
            if (button is >= 0 and < ImGuiMouseButton.COUNT)
                io->AddMouseButtonEvent((int)button, pressedDown);
        }
        
        static unsafe void UpdateMouseData()
        {
            var bd = GetBackendData();
            var io = ImGui.GetIO();
            var window = bd->Window;
            
            // (Optional) Set OS mouse position from Dear ImGui if requested (rarely used, only when ImGuiConfigFlags_NavEnableSetMousePos is enabled by user)
            // if (io->WantSetMousePos != 0)
            //     glfwSetCursorPos(window, (double)io.MousePos.x, (double)io.MousePos.y);
            
            var mouse = ((float3) Input.mousePosition).xy;
            bd->LastValidMousePos = mouse;
            io->AddMousePosEvent(mouse.x, io->DisplaySize.y-mouse.y);
        }
        
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        unsafe delegate char* GetClipboardFunc(void* user_data);
        [MonoPInvokeCallback(typeof(GetClipboardFunc))]
        static unsafe char* GetClipboardText(void* user_data)
            => (char*) new NativeText(GUIUtility.systemCopyBuffer, Allocator.Persistent).GetUnsafePtr();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        unsafe delegate void SetClipboardFunc(void* user_data, char* text);
        [MonoPInvokeCallback(typeof(SetClipboardFunc))]
        static unsafe void SetClipboardText(void* user_data, char* text) 
            => GUIUtility.systemCopyBuffer = new string(text);

        static GetClipboardFunc GCDefeat_GetClipboardFunc;
        static SetClipboardFunc GCDefeat_SetClipboardFunc;

        internal static unsafe bool Init()
        {
            var cam = Camera.main;
            if (cam == null)
                return false;
            var io = ImGui.GetIO();
            
            Debug.Assert(io->BackendPlatformUserData == null, "Already initialized a platform backend!");

            // Setup backend capabilities flags
            var bd = (PlatformData*) UnsafeUtility.Malloc(sizeof(PlatformData), UnsafeUtility.AlignOf<PlatformData>(), Allocator.Persistent);
            UnsafeUtility.MemClear(bd, sizeof(PlatformData));
            io->BackendPlatformUserData = bd;
            io->BackendPlatformName = (char*) new NativeText("imgui_impl_glfw", Allocator.Persistent).GetUnsafePtr();
            io->BackendFlags |= ImGuiBackendFlags.HasMouseCursors;         // We can honor GetMouseCursor() values (optional)
            io->BackendFlags |= ImGuiBackendFlags.HasSetMousePos;          // We can honor io.WantSetMousePos requests (optional, rarely used)
            io->BackendFlags |= ImGuiBackendFlags.RendererHasVtxOffset;
            
            bd->Window = cam.GetInstanceID();
            bd->Time = 0.0;

            GCDefeat_SetClipboardFunc = SetClipboardText;
            GCDefeat_GetClipboardFunc = GetClipboardText;
            io->SetClipboardTextFn = (delegate* unmanaged[Cdecl]<void*, char*, void>) Marshal.GetFunctionPointerForDelegate(GCDefeat_SetClipboardFunc);
            io->GetClipboardTextFn = (delegate* unmanaged[Cdecl]<void*, char*>) Marshal.GetFunctionPointerForDelegate(GCDefeat_GetClipboardFunc);
            io->ClipboardUserData = (void*)bd->Window;
            
            return true;
        }

        internal static unsafe void Shutdown()
        {
            var bd = GetBackendData();
            Debug.Assert(bd != null, "No platform backend to shutdown, or already shutdown?");
            var io = ImGui.GetIO();

            io->BackendPlatformName = null;
            io->BackendPlatformUserData = null;
            io->BackendFlags = 0;
            UnsafeUtility.Free(bd, Allocator.Persistent);
        }
    }
}