using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace com.daxode.imgui
{
    public class ImGuiRenderFeature : ScriptableRendererFeature
    {
        Material material;
        ImGuiRenderPass m_RenderPass;
        FrameHandler frameHandler;
        
        public override void Create()
        {
            material = CoreUtils.CreateEngineMaterial(Shader.Find("Hidden/ImGuiDrawShader"));
            m_RenderPass = new ImGuiRenderPass(material);
        }

        public override void AddRenderPasses(ScriptableRenderer renderer,
            ref RenderingData renderingData) => renderer.EnqueuePass(m_RenderPass);

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                m_RenderPass.Dispose();
                CoreUtils.Destroy(material);
                // DestroyImmediate(frameHandler.gameObject);
            }
        }
    }
    
    // Run early in the frame to clear the screen
    [ExecuteInEditMode]
    [DefaultExecutionOrder(0)]
    public class FrameHandler : MonoBehaviour
    {
        void Start()
        {
            if (FindFirstObjectByType<FrameHandler>() != null)
                DestroyImmediate(gameObject);
        }

        // void Update()
        // {
        //     // Start the Dear ImGui frame
        //     RenderHooks.NewFrame();
        //     InputAndWindowHooks.NewFrame();
        //     ImGui.NewFrame();
        // }
    }
}