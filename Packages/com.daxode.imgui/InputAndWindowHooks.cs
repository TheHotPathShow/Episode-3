using System;
using System.Runtime.InteropServices;
using AOT;
using Unity.Burst;
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
            public int2 DisplaySize; // Todo: Remember to update this when the window is resized
            
            public double Time;
            public float2 LastValidMousePos;
            public int MouseWindow;
            public bool HasEnteredAlready;

            public NativeArray<KeyCode> KeyMap;
        }
        
        [BurstCompile]
        internal static unsafe void NewFrame()
        {
            var io = ImGui.GetIO();
            var bd = GetBackendData();
            Debug.Assert(bd != null, "Did you call InitForXXX()?");
            
            // Setup display size (every frame to accommodate for window resizing)
            int2 display = new int2(Screen.width, Screen.height);
            io->DisplaySize = bd->DisplaySize;
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
            if (Input.mousePresent)
                io->AddMouseSourceEvent(ImGuiMouseSource.Mouse);
            else
                io->AddMouseSourceEvent(ImGuiMouseSource.TouchScreen);
            
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
                    io->AddFocusEvent(true);
                    bd->HasEnteredAlready = true;
                }
            } else if (bd->HasEnteredAlready)
            {
                CursorEnterCallback(false);
                io->AddFocusEvent(false);
                bd->HasEnteredAlready = false;
            }
            
            io->AddInputCharactersUTF8(Input.inputString);
            foreach (var keyCode in bd->KeyMap)
            {
                var key = keyCode switch
                {
                    KeyCode.A => ImGuiKey.A,
                    KeyCode.B => ImGuiKey.B,
                    KeyCode.C => ImGuiKey.C,
                    KeyCode.D => ImGuiKey.D,
                    KeyCode.E => ImGuiKey.E,
                    KeyCode.F => ImGuiKey.F,
                    KeyCode.G => ImGuiKey.G,
                    KeyCode.H => ImGuiKey.H,
                    KeyCode.I => ImGuiKey.I,
                    KeyCode.J => ImGuiKey.J,
                    KeyCode.K => ImGuiKey.K,
                    KeyCode.L => ImGuiKey.L,
                    KeyCode.M => ImGuiKey.M,
                    KeyCode.N => ImGuiKey.N,
                    KeyCode.O => ImGuiKey.O,
                    KeyCode.P => ImGuiKey.P,
                    KeyCode.Q => ImGuiKey.Q,
                    KeyCode.R => ImGuiKey.R,
                    KeyCode.S => ImGuiKey.S,
                    KeyCode.T => ImGuiKey.T,
                    KeyCode.U => ImGuiKey.U,
                    KeyCode.V => ImGuiKey.V,
                    KeyCode.W => ImGuiKey.W,
                    KeyCode.X => ImGuiKey.X,
                    KeyCode.Y => ImGuiKey.Y,
                    KeyCode.Z => ImGuiKey.Z,
                    KeyCode.Alpha0 => ImGuiKey.No0,
                    KeyCode.Alpha1 => ImGuiKey.No1,
                    KeyCode.Alpha2 => ImGuiKey.No2,
                    KeyCode.Alpha3 => ImGuiKey.No3,
                    KeyCode.Alpha4 => ImGuiKey.No4,
                    KeyCode.Alpha5 => ImGuiKey.No5,
                    KeyCode.Alpha6 => ImGuiKey.No6,
                    KeyCode.Alpha7 => ImGuiKey.No7,
                    KeyCode.Alpha8 => ImGuiKey.No8,
                    KeyCode.Alpha9 => ImGuiKey.No9,
                    KeyCode.F1 => ImGuiKey.F1,
                    KeyCode.F2 => ImGuiKey.F2,
                    KeyCode.F3 => ImGuiKey.F3,
                    KeyCode.F4 => ImGuiKey.F4,
                    KeyCode.F5 => ImGuiKey.F5,
                    KeyCode.F6 => ImGuiKey.F6,
                    KeyCode.F7 => ImGuiKey.F7,
                    KeyCode.F8 => ImGuiKey.F8,
                    KeyCode.F9 => ImGuiKey.F9,
                    KeyCode.F10 => ImGuiKey.F10,
                    KeyCode.F11 => ImGuiKey.F11,
                    KeyCode.F12 => ImGuiKey.F12,
                    KeyCode.F13 => ImGuiKey.F13,
                    KeyCode.F14 => ImGuiKey.F14,
                    KeyCode.F15 => ImGuiKey.F15,
                    KeyCode.UpArrow => ImGuiKey.UpArrow,
                    KeyCode.DownArrow => ImGuiKey.DownArrow,
                    KeyCode.LeftArrow => ImGuiKey.LeftArrow,
                    KeyCode.RightArrow => ImGuiKey.RightArrow,
                    KeyCode.Space => ImGuiKey.Space,
                    KeyCode.Quote => ImGuiKey.Apostrophe,
                    KeyCode.Comma => ImGuiKey.Comma,
                    KeyCode.Minus => ImGuiKey.Minus,
                    KeyCode.Period => ImGuiKey.Period,
                    KeyCode.Slash => ImGuiKey.Slash,
                    KeyCode.Semicolon => ImGuiKey.Semicolon,
                    KeyCode.Equals => ImGuiKey.Equal,
                    KeyCode.LeftBracket => ImGuiKey.LeftBracket,
                    KeyCode.Backslash => ImGuiKey.Backslash,
                    KeyCode.RightBracket => ImGuiKey.RightBracket,
                    KeyCode.BackQuote => ImGuiKey.Apostrophe,
                    KeyCode.Escape => ImGuiKey.Escape,
                    KeyCode.Return => ImGuiKey.Enter,
                    KeyCode.Tab => ImGuiKey.Tab,
                    KeyCode.Backspace => ImGuiKey.Backspace,
                    KeyCode.Insert => ImGuiKey.Insert,
                    KeyCode.Delete => ImGuiKey.Delete,
                    KeyCode.PageUp => ImGuiKey.PageUp,
                    KeyCode.PageDown => ImGuiKey.PageDown,
                    KeyCode.Home => ImGuiKey.Home,
                    KeyCode.End => ImGuiKey.End,
                    KeyCode.CapsLock => ImGuiKey.CapsLock,
                    KeyCode.ScrollLock => ImGuiKey.ScrollLock,
                    KeyCode.Numlock => ImGuiKey.NumLock,
                    KeyCode.Print => ImGuiKey.PrintScreen,
                    KeyCode.Pause => ImGuiKey.Pause,
                    // KeyCode.LeftShift => ImGuiKey.LeftShift,
                    // KeyCode.RightShift => ImGuiKey.RightShift,
                    // KeyCode.LeftControl => ImGuiKey.LeftCtrl,
                    // KeyCode.RightControl => ImGuiKey.RightCtrl,
                    // KeyCode.LeftAlt => ImGuiKey.LeftAlt,
                    // KeyCode.RightAlt => ImGuiKey.RightAlt,
                    // KeyCode.LeftWindows => ImGuiKey.LeftSuper,
                    // KeyCode.RightWindows => ImGuiKey.RightSuper,
                    KeyCode.Menu => ImGuiKey.Menu,
                    KeyCode.Keypad0 => ImGuiKey.Keypad0,
                    KeyCode.Keypad1 => ImGuiKey.Keypad1,
                    KeyCode.Keypad2 => ImGuiKey.Keypad2,
                    KeyCode.Keypad3 => ImGuiKey.Keypad3,
                    KeyCode.Keypad4 => ImGuiKey.Keypad4,
                    KeyCode.Keypad5 => ImGuiKey.Keypad5,
                    KeyCode.Keypad6 => ImGuiKey.Keypad6,
                    KeyCode.Keypad7 => ImGuiKey.Keypad7,
                    KeyCode.Keypad8 => ImGuiKey.Keypad8,
                    KeyCode.Keypad9 => ImGuiKey.Keypad9,
                    KeyCode.KeypadDivide => ImGuiKey.KeypadDivide,
                    KeyCode.KeypadMultiply => ImGuiKey.KeypadMultiply,
                    KeyCode.KeypadMinus => ImGuiKey.KeypadSubtract,
                    KeyCode.KeypadPlus => ImGuiKey.KeypadAdd,
                    KeyCode.KeypadEnter => ImGuiKey.KeypadEnter,
                    KeyCode.KeypadPeriod => ImGuiKey.KeypadDecimal,
                    KeyCode.KeypadEquals => ImGuiKey.KeypadEqual,
                    // KeyCode.Menu => ImGuiKey.Menu,
                    
                    // Alt, Ctrl, Shift, Super
                    KeyCode.LeftAlt => ImGuiKey.ReservedForModAlt,
                    KeyCode.RightAlt => ImGuiKey.ReservedForModAlt,
                    KeyCode.LeftShift => ImGuiKey.ReservedForModShift,
                    KeyCode.RightShift => ImGuiKey.ReservedForModShift,
                    KeyCode.LeftControl => ImGuiKey.ReservedForModCtrl,
                    KeyCode.RightControl => ImGuiKey.ReservedForModCtrl,
                    KeyCode.LeftWindows => ImGuiKey.ReservedForModSuper,
                    KeyCode.RightWindows => ImGuiKey.ReservedForModSuper,
                    
                    _ => ImGuiKey.None
                };
                if (key == ImGuiKey.None)
                    continue;
                
                if (Input.GetKeyDown(keyCode))
                    io->AddKeyEvent(key, true);
                if (Input.GetKeyUp(keyCode))
                    io->AddKeyEvent(key, false);
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
            => GUIUtility.systemCopyBuffer = Marshal.PtrToStringAnsi((IntPtr) text);

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
            io->BackendPlatformName = new NativeText("Unity Input and Window", Allocator.Persistent).GetUnsafePtr();
            io->BackendFlags |= ImGuiBackendFlags.HasMouseCursors;         // We can honor GetMouseCursor() values (optional)
            io->BackendFlags |= ImGuiBackendFlags.HasSetMousePos;          // We can honor io.WantSetMousePos requests (optional, rarely used)
            io->BackendFlags |= ImGuiBackendFlags.RendererHasVtxOffset;
            
            bd->Window = cam.GetInstanceID();
            bd->DisplaySize = new int2(cam.pixelWidth, cam.pixelHeight);
            bd->Time = 0.0;
            bd->KeyMap = new NativeArray<KeyCode>((KeyCode[])Enum.GetValues(typeof(KeyCode)), Allocator.Persistent);

            GCDefeat_SetClipboardFunc = SetClipboardText;
            GCDefeat_GetClipboardFunc = GetClipboardText;
            io->SetClipboardTextFn = (delegate* unmanaged[Cdecl]<IntPtr, byte*, void>) Marshal.GetFunctionPointerForDelegate(GCDefeat_SetClipboardFunc);
            io->GetClipboardTextFn = (delegate* unmanaged[Cdecl]<IntPtr, char*>) Marshal.GetFunctionPointerForDelegate(GCDefeat_GetClipboardFunc);
            io->ClipboardUserData = (void*)bd->Window;
            
            return true;
        }

        internal static unsafe void Shutdown()
        {
            var bd = GetBackendData();
            if (bd == null) 
                return;
            Debug.Assert(bd != null, "No platform backend to shutdown, or already shutdown?");
            var io = ImGui.GetIO();
            bd->KeyMap.Dispose();

            io->BackendPlatformName = null;
            io->BackendPlatformUserData = null;
            io->BackendFlags = 0;
            UnsafeUtility.Free(bd, Allocator.Persistent);
        }
    }
}