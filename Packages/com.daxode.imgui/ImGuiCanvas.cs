using System;
using Unity.Burst;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;

namespace com.daxode.imgui
{
    [BurstCompile]
    public class ImGuiCanvas : MonoBehaviour
    {
        // Our state
        struct MyGUIData
        {
            public bool showDemoWindow;
            public bool showAnotherWindow;
            public Color meshColor;
            public float alpha;
            public int counter;
            public UnityObjRef<Texture2D> imageToDraw;
            public bool shouldChangeColor;
            public FixedString512Bytes inputText;
        }

        MyGUIData m_GUIData;

        void Start()
        {
            m_GUIData.showAnotherWindow = true;
            m_GUIData.meshColor = new Color(0.45f, 0.55f, 0.60f, 1.00f);
            m_GUIData.imageToDraw = imageToDraw;
            m_GUIData.inputText = "This is some text in a text box.";
        }

        [SerializeField] Texture2D imageToDraw;
        [SerializeField] MeshRenderer meshRendererToChange;

        unsafe void Update()
        {
            // Start the Dear ImGui frame
            if (!ImGuiHelper.NewFrameSafe())
                return;
            
            // The GUI
            BurstedGUI(ref m_GUIData);
            if (m_GUIData.shouldChangeColor)
                meshRendererToChange.material.color = m_GUIData.meshColor;
        }


        // [BurstCompile] still need to figure out how to call float2 functions o.o - how is that the hard thing and not strings lol
        static void BurstedGUI(ref MyGUIData guiData)
        {
            // 1. Show the big demo window (Most of the sample code is in ImGui::ShowDemoWindow()! You can browse its code to learn more about Dear ImGui!).
            if (guiData.showDemoWindow)
                ImGui.ShowDemoWindow(ref guiData.showDemoWindow);

            // Create DockSpace
            {
                var dockSpaceWindowFlags = ImGuiWindowFlags.NoTitleBar | ImGuiWindowFlags.NoCollapse | ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoMove;
                dockSpaceWindowFlags |= ImGuiWindowFlags.NoBringToFrontOnFocus | ImGuiWindowFlags.NoNavFocus | ImGuiWindowFlags.NoBackground;
                
                ImGui.SetNextWindowPos(0);
                unsafe { ImGui.SetNextWindowSize(ImGui.GetIO()->DisplaySize); }
                ImGui.PushStyleVar(ImGuiStyleVar.WindowRounding, 0);
                ImGui.PushStyleVar(ImGuiStyleVar.WindowBorderSize, 0);
                ImGui.PushStyleVar(ImGuiStyleVar.WindowPadding, new float2(0));
                ImGui.Begin("Cool DockSpace", dockSpaceWindowFlags);
                unsafe { ImGui.DockSpace(ImGui.GetID("DockSpace"), 0, ImGuiDockNodeFlags.PassthruCentralNode); }
                ImGui.End();
                ImGui.PopStyleVar(3);
            }
            
            // 2. Show a simple window that we create ourselves. We use a Begin/End pair to create a named window.
            {
                // Create a window called "Hello, world!" and append into it.
                ImGui.Begin("Hello, world!"); 
                ImGui.Text("This is some useful text.");
                ImGui.Checkbox("Demo Window", ref guiData.showDemoWindow);
                ImGui.SameLine();
                ImGui.Checkbox("Another Window", ref guiData.showAnotherWindow);
                
                ImGui.SliderFloat("Some alpha value:", ref guiData.alpha, 0.0f, 1.0f);
                guiData.shouldChangeColor = ImGui.ColorEdit3("Color of mesh", ref guiData.meshColor);
                
                // Make a counter
                if (ImGui.Button("Button"))
                    guiData.counter++;
                ImGui.SameLine();
                ImGui.Text($"counter = {guiData.counter}");
                
                // Draw 20 buttons, 5 in every row
                for (int i = 0; i < 20; i++)
                {
                    if (i % 5 != 0)
                        ImGui.SameLine();
                    ImGui.Button($"No.{i} Button");
                }
            
                // Display framerate
                unsafe
                {
                    var framerate = ImGui.GetIO()->Framerate;
                    ImGui.Text($"Application average {1000.0f / framerate} ms/frame ({framerate} FPS)");
                }
                
                // Draw an image
                ImGui.Image(guiData.imageToDraw, 500);
                
                ImGui.End();
            }

            // 3. Show another simple window.
            if (guiData.showAnotherWindow)
            {
                ImGui.Begin("Another Window", ref guiData.showAnotherWindow);   // Pass a pointer to our bool variable (the window will have a closing button that will clear the bool when clicked)
                ImGui.Text("Hello from another window!");
                if (ImGui.Button("Close Me"))
                    guiData.showAnotherWindow = false;
                ImGui.Text("Some text: ");
                ImGui.SameLine();
                if (ImGui.InputTextWithHint("##InputWindow", "My Fancy Hint", ref guiData.inputText))
                    Debug.Log($"Input Text: {guiData.inputText}");
                ImGui.End();
            }
        }
    }
}
