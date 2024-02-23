using Unity.Collections.LowLevel.Unsafe;
using Unity.Mathematics;
using UnityEngine;

namespace com.daxode.imgui
{
    public class ImGuiCanvas : MonoBehaviour
    {
        // Our state
        bool show_demo_window = true;
        bool show_another_window = false;
        
        float4 clear_color = new float4(0.45f, 0.55f, 0.60f, 1.00f);
        float f = 0.0f;
        int counter = 0;

        unsafe void Update()
        {
            // Start the Dear ImGui frame - plan is to move this to a separate script
            if (ImGui.GetCurrentContext() == null || RenderHooks.GetBackendData() == null)
                return;
            RenderHooks.NewFrame();
            InputAndWindowHooks.NewFrame();
            ImGui.NewFrame();
            
            // 1. Show the big demo window (Most of the sample code is in ImGui::ShowDemoWindow()! You can browse its code to learn more about Dear ImGui!).
            if (show_demo_window)
                 ImGui.ShowDemoWindow((byte*) UnsafeUtility.AddressOf(ref show_demo_window));
            
            // 2. Show a simple window that we create ourselves. We use a Begin/End pair to create a named window.
            {
                ImGui.Begin("Hello, world!");                                    // Create a window called "Hello, world!" and append into it.
                ImGui.Text("This is some useful text.");                                     // Display some text (you can use a format strings too)
                ImGui.Checkbox("Demo Window", ref show_demo_window);              // Edit bools storing our window open/close state
                ImGui.Checkbox("Another Window", ref show_another_window);
                
                ImGui.SliderFloat("float", ref f, 0.0f, 1.0f);              // Edit 1 float using a slider from 0.0f to 1.0f
                ImGui.ColorEdit3("clear color", ref clear_color);                            // Edit 3 floats representing a color
                
                if (ImGui.Button("Button"))                                                 // Buttons return true when clicked (most widgets return true when edited/activated)
                    counter++;
                ImGui.SameLine();
                ImGui.Text($"counter = {counter}");

                var framerate = ImGui.GetIO()->Framerate;
                ImGui.Text($"Application average {1000.0f / framerate} ms/frame ({framerate} FPS)");
                ImGui.End();
            }

            // 3. Show another simple window.
            if (show_another_window)
            {
                ImGui.Begin("Another Window", ref show_another_window);   // Pass a pointer to our bool variable (the window will have a closing button that will clear the bool when clicked)
                ImGui.Text("Hello from another window!");
                if (ImGui.Button("Close Me", new float2(50, 50)))
                    show_another_window = false;
                ImGui.End();
            }
        }
    }
}
