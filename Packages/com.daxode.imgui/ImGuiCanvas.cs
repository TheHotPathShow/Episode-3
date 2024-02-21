using System;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace com.daxode.imgui
{
    public partial class ImGuiCanvas : MonoBehaviour
    {
        public unsafe void Start()
        {
            // glfwSetErrorCallback(glfw_error_callback);
            // if (!glfwInit())
            //     return; // 1

            // Create window with graphics context
            // GLFWwindow* window = glfwCreateWindow(1280, 720, "Dear ImGui GLFW+OpenGL2 example", nullptr, nullptr);
            // if (window == nullptr)
            //     return; // 1
            
            // glfwMakeContextCurrent(window);
            // glfwSwapInterval(1); // Enable vsync

            // Setup Dear ImGui context
            IMGUI_CHECKVERSION();
            ImGui.CreateContext();
            var io = ImGui.GetIO();
            io->ConfigFlags |= ImGuiConfigFlags.NavEnableKeyboard;     // Enable Keyboard Controls
            io->ConfigFlags |= ImGuiConfigFlags.NavEnableGamepad;      // Enable Gamepad Controls

            // Setup Dear ImGui style
            ImGui.StyleColorsDark();

            // Setup Platform/Renderer backends
            ImGui_ImplGlfw_InitForOpenGL(true);
            ImGui_ImplOpenGL2_Init();
            
            // Load Fonts
            // - If no fonts are loaded, dear imgui will use the default font. You can also load multiple fonts and use ImGui::PushFont()/PopFont() to select them.
            // - AddFontFromFileTTF() will return the ImFont* so you can store it if you need to select the font among multiple.
            // - If the file cannot be loaded, the function will return a nullptr. Please handle those errors in your application (e.g. use an assertion, or display an error and quit).
            // - The fonts will be rasterized at a given size (w/ oversampling) and stored into a texture when calling ImFontAtlas::Build()/GetTexDataAsXXXX(), which ImGui_ImplXXXX_NewFrame below will call.
            // - Use '#define IMGUI_ENABLE_FREETYPE' in your imconfig file to use Freetype for higher quality font rendering.
            // - Read 'docs/FONTS.md' for more instructions and details.
            // - Remember that in C/C++ if you want to include a backslash \ in a string literal you need to write a double backslash \\ !
            io->Fonts->AddFontDefault();
            //io.Fonts->AddFontFromFileTTF("c:\\Windows\\Fonts\\segoeui.ttf", 18.0f);
            //io.Fonts->AddFontFromFileTTF("../../misc/fonts/DroidSans.ttf", 16.0f);
            //io.Fonts->AddFontFromFileTTF("../../misc/fonts/Roboto-Medium.ttf", 16.0f);
            //io.Fonts->AddFontFromFileTTF("../../misc/fonts/Cousine-Regular.ttf", 15.0f);
            //ImFont* font = io.Fonts->AddFontFromFileTTF("c:\\Windows\\Fonts\\ArialUni.ttf", 18.0f, nullptr, io.Fonts->GetGlyphRangesJapanese());
            //IM_ASSERT(font != nullptr);
        }
        
        // Our state
        bool show_demo_window = true;
        bool show_another_window = false;
        float4 clear_color = new float4(0.45f, 0.55f, 0.60f, 1.00f);

        float f = 0.0f;
        int counter = 0;

        unsafe void Update()
        {
            var io = ImGui.GetIO();
            
            // glfwPollEvents();

            // Start the Dear ImGui frame
            ImGui_ImplOpenGL2_NewFrame();
            ImGui_ImplGlfw_NewFrame();
            ImGui.NewFrame();

            // 1. Show the big demo window (Most of the sample code is in ImGui::ShowDemoWindow()! You can browse its code to learn more about Dear ImGui!).
            // if (show_demo_window)
            //     ImGui.ShowDemoWindow(&show_demo_window);

            // 2. Show a simple window that we create ourselves. We use a Begin/End pair to create a named window.
            {

                ImGui.Begin("Hello, world!");                          // Create a window called "Hello, world!" and append into it.

                ImGui.Text("This is some useful text.");               // Display some text (you can use a format strings too)
                ImGui.Checkbox("Demo Window", ref show_demo_window);      // Edit bools storing our window open/close state
                ImGui.Checkbox("Another Window", ref show_another_window);

                ImGui.SliderFloat("float", ref f, 0.0f, 1.0f);            // Edit 1 float using a slider from 0.0f to 1.0f
                ImGui.ColorEdit3("clear color", ref clear_color); // Edit 3 floats representing a color

                if (ImGui.Button("Button"))                            // Buttons return true when clicked (most widgets return true when edited/activated)
                    counter++;
                ImGui.SameLine();
                ImGui.Text($"counter = {counter}");

                ImGui.Text($"Application average {1000.0f / io->Framerate} ms/frame ({io->Framerate} FPS)");
                ImGui.End();
            }

            // 3. Show another simple window.
            if (show_another_window)
            {
                ImGui.Begin("Another Window", ref show_another_window);   // Pass a pointer to our bool variable (the window will have a closing button that will clear the bool when clicked)
                ImGui.Text("Hello from another window!");
                if (ImGui.Button("Close Me"))
                    show_another_window = false;
                ImGui.End();
            }
        }

        unsafe void OnDestroy()
        {
            // Cleanup
            ImGui_ImplOpenGL2_Shutdown();
            ImGui_ImplGlfw_Shutdown();
            Debug.Log(((IntPtr)ImGui.GetIO()->BackendPlatformUserData).ToString());
            Debug.Log(((IntPtr)ImGui.GetIO()->BackendRendererUserData).ToString());
            
            // ImGui.DestroyContext();

            // glfwDestroyWindow(window);
            // glfwTerminate();
            
            return; // 0
        }

        ImGuiRenderPass m_ImGuiRenderPass;
        [SerializeField] Shader shader;
        void OnEnable()
        {
            m_ImGuiRenderPass = new ImGuiRenderPass(CoreUtils.CreateEngineMaterial(shader));
            RenderPipelineManager.beginCameraRendering += OnCameraRender;
        }
        void OnDisable()
        {
            RenderPipelineManager.beginCameraRendering -= OnCameraRender;
            m_ImGuiRenderPass.Dispose();
        }
        void OnCameraRender(ScriptableRenderContext ctx, Camera cam) 
            => cam.GetUniversalAdditionalCameraData().scriptableRenderer.EnqueuePass(m_ImGuiRenderPass);

        static unsafe PlatformData* ImGui_ImplGlfw_GetBackendData() 
            => ImGui.GetCurrentContext() != null ? (PlatformData*)ImGui.GetIO()->BackendPlatformUserData : null;

        struct PlatformData
        {
            public bool InstalledCallbacks;
            public int Window;
            public double Time;
        }
        
        // GLFW
        unsafe void ImGui_ImplGlfw_NewFrame()
        {
            var io = ImGui.GetIO();
            var bd = ImGui_ImplGlfw_GetBackendData();
            Debug.Assert(bd != null, "Did you call ImGui_ImplGlfw_InitForXXX()?");

            var cam = Camera.main;
            if (cam == null)
                return;
            
            // Setup display size (every frame to accommodate for window resizing)
            int2 display = new int2(cam.pixelWidth, cam.pixelHeight);
            // glfwGetFramebufferSize(bd->Window, &display_w, &display_h);
            io->DisplaySize = new float2(cam.pixelWidth, cam.pixelHeight);
            if (math.all(io->DisplaySize > 0))
                io->DisplayFramebufferScale = new float2(display.x / io->DisplaySize.x, display.y / io->DisplaySize.y);

            // Setup time step
            // (Accept glfwGetTime() not returning a monotonically increasing value. Seems to happens on disconnecting peripherals and probably on VMs and Emscripten, see #6491, #6189, #6114, #3644)
            double current_time = Time.timeAsDouble;
            if (current_time <= bd->Time)
                current_time = bd->Time + 0.00001f;
            io->DeltaTime = bd->Time > 0.0 ? (float)(current_time - bd->Time) : (float)(1.0f / 60.0f);
            bd->Time = current_time;

            // ImGui_ImplGlfw_UpdateMouseData();
            // ImGui_ImplGlfw_UpdateMouseCursor();

            // Update game controllers (if enabled and available)
            // ImGui_ImplGlfw_UpdateGamepads();
        }
        
        unsafe void ImGui_ImplGlfw_InstallCallbacks()
        {
            var bd = ImGui_ImplGlfw_GetBackendData();
            Debug.Assert(bd->InstalledCallbacks == false, "Callbacks already installed!");

            // bd->PrevUserCallbackWindowFocus = glfwSetWindowFocusCallback(window, ImGui_ImplGlfw_WindowFocusCallback);
            // bd->PrevUserCallbackCursorEnter = glfwSetCursorEnterCallback(window, ImGui_ImplGlfw_CursorEnterCallback);
            // bd->PrevUserCallbackCursorPos = glfwSetCursorPosCallback(window, ImGui_ImplGlfw_CursorPosCallback);
            // bd->PrevUserCallbackMousebutton = glfwSetMouseButtonCallback(window, ImGui_ImplGlfw_MouseButtonCallback);
            // bd->PrevUserCallbackScroll = glfwSetScrollCallback(window, ImGui_ImplGlfw_ScrollCallback);
            // bd->PrevUserCallbackKey = glfwSetKeyCallback(window, ImGui_ImplGlfw_KeyCallback);
            // bd->PrevUserCallbackChar = glfwSetCharCallback(window, ImGui_ImplGlfw_CharCallback);
            // bd->PrevUserCallbackMonitor = glfwSetMonitorCallback(ImGui_ImplGlfw_MonitorCallback);
            bd->InstalledCallbacks = true;
        }
        
        unsafe void ImGui_ImplGlfw_RestoreCallbacks(int window)
        {
            var bd = ImGui_ImplGlfw_GetBackendData();
            Debug.Assert(bd->InstalledCallbacks == true, "Callbacks not installed!");
            Debug.Assert(bd->Window == window);

            // glfwSetWindowFocusCallback(window, bd->PrevUserCallbackWindowFocus);
            // glfwSetCursorEnterCallback(window, bd->PrevUserCallbackCursorEnter);
            // glfwSetCursorPosCallback(window, bd->PrevUserCallbackCursorPos);
            // glfwSetMouseButtonCallback(window, bd->PrevUserCallbackMousebutton);
            // glfwSetScrollCallback(window, bd->PrevUserCallbackScroll);
            // glfwSetKeyCallback(window, bd->PrevUserCallbackKey);
            // glfwSetCharCallback(window, bd->PrevUserCallbackChar);
            // glfwSetMonitorCallback(bd->PrevUserCallbackMonitor);
            bd->InstalledCallbacks = false;
            // bd->PrevUserCallbackWindowFocus = null;
            // bd->PrevUserCallbackCursorEnter = nullptr;
            // bd->PrevUserCallbackCursorPos = nullptr;
            // bd->PrevUserCallbackMousebutton = nullptr;
            // bd->PrevUserCallbackScroll = nullptr;
            // bd->PrevUserCallbackKey = nullptr;
            // bd->PrevUserCallbackChar = nullptr;
            // bd->PrevUserCallbackMonitor = nullptr;
        }

        unsafe bool ImGui_ImplGlfw_InitForOpenGL(bool install_callbacks = true)
        {
            var cam = Camera.main;
            if (cam == null)
                return false;
            var io = ImGui.GetIO();
            
            Debug.Assert(io->BackendPlatformUserData == null, "Already initialized a platform backend!");
            //printf("GLFW_VERSION: %d.%d.%d (%d)", GLFW_VERSION_MAJOR, GLFW_VERSION_MINOR, GLFW_VERSION_REVISION, GLFW_VERSION_COMBINED);

            // Setup backend capabilities flags
            var bd = (PlatformData*) UnsafeUtility.Malloc(sizeof(PlatformData), UnsafeUtility.AlignOf<PlatformData>(), Allocator.Persistent);
            UnsafeUtility.MemClear(bd, sizeof(PlatformData));
            io->BackendPlatformUserData = bd;
            io->BackendPlatformName = (char*) new NativeText("imgui_impl_glfw", Allocator.Persistent).GetUnsafePtr();
            io->BackendFlags |= ImGuiBackendFlags.HasMouseCursors;         // We can honor GetMouseCursor() values (optional)
            io->BackendFlags |= ImGuiBackendFlags.HasSetMousePos;          // We can honor io.WantSetMousePos requests (optional, rarely used)

            bd->Window = cam.GetInstanceID();
            bd->Time = 0.0;

            // io.SetClipboardTextFn = ImGui_ImplGlfw_SetClipboardText;
            // io.GetClipboardTextFn = ImGui_ImplGlfw_GetClipboardText;
            io->ClipboardUserData = (void*)bd->Window;

        //     // Create mouse cursors
        //     // (By design, on X11 cursors are user configurable and some cursors may be missing. When a cursor doesn't exist,
        //     // GLFW will emit an error which will often be printed by the app, so we temporarily disable error reporting.
        //     // Missing cursors will return nullptr and our _UpdateMouseCursor() function will use the Arrow cursor instead.)
        //     GLFWerrorfun prev_error_callback = glfwSetErrorCallback(nullptr);
        //     bd->MouseCursors[ImGuiMouseCursor_Arrow] = glfwCreateStandardCursor(GLFW_ARROW_CURSOR);
        //     bd->MouseCursors[ImGuiMouseCursor_TextInput] = glfwCreateStandardCursor(GLFW_IBEAM_CURSOR);
        //     bd->MouseCursors[ImGuiMouseCursor_ResizeNS] = glfwCreateStandardCursor(GLFW_VRESIZE_CURSOR);
        //     bd->MouseCursors[ImGuiMouseCursor_ResizeEW] = glfwCreateStandardCursor(GLFW_HRESIZE_CURSOR);
        //     bd->MouseCursors[ImGuiMouseCursor_Hand] = glfwCreateStandardCursor(GLFW_HAND_CURSOR);
        // #if GLFW_HAS_NEW_CURSORS
        //     bd->MouseCursors[ImGuiMouseCursor_ResizeAll] = glfwCreateStandardCursor(GLFW_RESIZE_ALL_CURSOR);
        //     bd->MouseCursors[ImGuiMouseCursor_ResizeNESW] = glfwCreateStandardCursor(GLFW_RESIZE_NESW_CURSOR);
        //     bd->MouseCursors[ImGuiMouseCursor_ResizeNWSE] = glfwCreateStandardCursor(GLFW_RESIZE_NWSE_CURSOR);
        //     bd->MouseCursors[ImGuiMouseCursor_NotAllowed] = glfwCreateStandardCursor(GLFW_NOT_ALLOWED_CURSOR);
        // #else
        //     bd->MouseCursors[ImGuiMouseCursor_ResizeAll] = glfwCreateStandardCursor(GLFW_ARROW_CURSOR);
        //     bd->MouseCursors[ImGuiMouseCursor_ResizeNESW] = glfwCreateStandardCursor(GLFW_ARROW_CURSOR);
        //     bd->MouseCursors[ImGuiMouseCursor_ResizeNWSE] = glfwCreateStandardCursor(GLFW_ARROW_CURSOR);
        //     bd->MouseCursors[ImGuiMouseCursor_NotAllowed] = glfwCreateStandardCursor(GLFW_ARROW_CURSOR);
        // #endif
        
        // glfwSetErrorCallback(prev_error_callback);
        // #if GLFW_HAS_GETERROR && !defined(__EMSCRIPTEN__) // Eat errors (see #5908)
        //     (void)glfwGetError(nullptr);
        // #endif

        // Chain GLFW callbacks: our callbacks will call the user's previously installed callbacks, if any.
        if (install_callbacks)
            ImGui_ImplGlfw_InstallCallbacks();

        // Set platform dependent data in viewport
        // ImGuiViewport* main_viewport = ImGui::GetMainViewport();
        // #if !UNITY_64
        //     main_viewport->PlatformHandleRaw = glfwGetWin32Window(bd->Window);
        // #elif defined(__APPLE__)
        //     main_viewport->PlatformHandleRaw = (void*)glfwGetCocoaWindow(bd->Window);
        // #else
        //     IM_UNUSED(main_viewport);
        // #endif

            // Windows: register a WndProc hook so we can intercept some messages.
        // #ifdef _WIN32
        //     bd->GlfwWndProc = (WNDPROC)::GetWindowLongPtrW((HWND)main_viewport->PlatformHandleRaw, GWLP_WNDPROC);
        //     IM_ASSERT(bd->GlfwWndProc != nullptr);
        //     ::SetWindowLongPtrW((HWND)main_viewport->PlatformHandleRaw, GWLP_WNDPROC, (LONG_PTR)ImGui_ImplGlfw_WndProc);
        // #endif

            // bd->ClientApi = client_api;
            return true;
        }

        unsafe void ImGui_ImplGlfw_Shutdown()
        {
            var bd = ImGui_ImplGlfw_GetBackendData();
            Debug.Assert(bd != null, "No platform backend to shutdown, or already shutdown?");
            var io = ImGui.GetIO();

            if (bd->InstalledCallbacks)
                ImGui_ImplGlfw_RestoreCallbacks(bd->Window);
            
            // for (ImGuiMouseCursor cursor_n = 0; cursor_n < ImGuiMouseCursor_COUNT; cursor_n++)
            //     glfwDestroyCursor(bd->MouseCursors[cursor_n]);

            io->BackendPlatformName = null;
            io->BackendPlatformUserData = null;
            io->BackendFlags &= ~(ImGuiBackendFlags.HasMouseCursors | ImGuiBackendFlags.HasSetMousePos | ImGuiBackendFlags.HasGamepad);
            UnsafeUtility.Free(bd, Allocator.Persistent);
        }
        
        // Open GL2
        struct RendererUserData
        {
            public UnityObjRef<Texture2D> FontTexture;
        }

        static unsafe RendererUserData* ImGui_ImplOpenGL2_GetBackendData() 
            => ImGui.GetCurrentContext() != null ? (RendererUserData*)ImGui.GetIO()->BackendRendererUserData : null;

        unsafe bool ImGui_ImplOpenGL2_CreateFontsTexture()
        {
            // Build texture atlas
            var io = ImGui.GetIO();
            var bd = ImGui_ImplOpenGL2_GetBackendData();
            io->Fonts->GetTexDataAsRGBA32(out var pixels, out var width, out var height, out var bytesperpixel);   // Load as RGBA 32-bit (75% of the memory is wasted, but default font is so small) because it is more likely to be compatible with user's existing shaders. If your ImTextureId represent a higher-level concept than just a GL texture id, consider calling GetTexDataAsAlpha8() instead to save on GPU memory.

            // Upload texture to graphics system
            // (Bilinear sampling is required by default. Set 'io.Fonts->Flags |= ImFontAtlasFlags_NoBakedLines' or 'style.AntiAliasedLinesUseTex = false' to allow point/nearest sampling)
            var texture = new Texture2D(width, height, TextureFormat.RGBA32, false, true);
            texture.name = "ImGuiFontTexture";
            bd->FontTexture = texture;
            texture.LoadRawTextureData((IntPtr)pixels, width * height * bytesperpixel);
            texture.Apply();
            
            // Store our identifier
            io->Fonts->SetTexID(UnsafeUtility.As<int, ImTextureID>(ref bd->FontTexture.InstanceID));

            return true;
        }
        
        bool ImGui_ImplOpenGL2_CreateDeviceObjects() 
            => ImGui_ImplOpenGL2_CreateFontsTexture();

        unsafe void ImGui_ImplOpenGL2_NewFrame()
        {
            var bd = ImGui_ImplOpenGL2_GetBackendData();
            Debug.Assert(bd != null, "Did you call ImGui_ImplOpenGL2_Init()?");
            if (bd->FontTexture.InstanceID == 0)
                ImGui_ImplOpenGL2_CreateDeviceObjects();
        }

        unsafe bool ImGui_ImplOpenGL2_Init()
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

        unsafe void ImGui_ImplOpenGL2_DestroyFontsTexture()
        {
            var io = ImGui.GetIO();
            var bd = ImGui_ImplOpenGL2_GetBackendData();
            if (bd->FontTexture.InstanceID == 0) 
                return;
            Destroy(bd->FontTexture.Value);
            io->Fonts->SetTexID(default);
            bd->FontTexture = default;
        }
        
        void ImGui_ImplOpenGL2_DestroyDeviceObjects()
        {
            ImGui_ImplOpenGL2_DestroyFontsTexture();
        }
        
        unsafe void ImGui_ImplOpenGL2_Shutdown()
        {
            var bd = ImGui_ImplOpenGL2_GetBackendData();
            Debug.Assert(bd != null, "No renderer backend to shutdown, or already shutdown?");
            var io = ImGui.GetIO();
            
            ImGui_ImplOpenGL2_DestroyDeviceObjects();
            io->BackendRendererName = null; // currently leaks memory
            io->BackendRendererUserData = null;
            UnsafeUtility.Free(bd, Allocator.Persistent);
        }

        // ImGui
        unsafe void IMGUI_CHECKVERSION()
            => ImGui.DebugCheckVersionAndDataLayout(ImGui.VERSION, 
                sizeof(ImGuiIO), sizeof(ImGuiStyle), sizeof(float2),
                sizeof(float4), sizeof(ImDrawVert), sizeof(ImDrawIdx));
    }
}
