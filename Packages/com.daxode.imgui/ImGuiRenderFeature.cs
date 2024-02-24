using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace com.daxode.imgui
{
    public class ImGuiRenderFeature : ScriptableRendererFeature
    {
        ImGuiRenderPass m_RenderPass;
        
        public override void Create()
        {
            m_RenderPass = new ImGuiRenderPass();
        }

        public override void AddRenderPasses(ScriptableRenderer renderer,
            ref RenderingData renderingData) => renderer.EnqueuePass(m_RenderPass);

        protected override void Dispose(bool disposing)
        {
            if (disposing) 
                m_RenderPass.Dispose();
        }
    }
}