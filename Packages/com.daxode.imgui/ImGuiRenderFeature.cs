using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace com.daxode.imgui
{
    public class ImGuiRenderFeature : ScriptableRendererFeature
    {
        [SerializeField] private Shader shader;
        private Material material;
        private ImGuiRenderPass m_ImGuiRenderPass;

        public override void Create()
        {
            if (shader == null)
            {
                return;
            }
            material = CoreUtils.CreateEngineMaterial(shader);
            m_ImGuiRenderPass = new ImGuiRenderPass(material);
        
            m_ImGuiRenderPass.renderPassEvent = RenderPassEvent.AfterRenderingSkybox;
        }

        public override void AddRenderPasses(ScriptableRenderer renderer,
            ref RenderingData renderingData)
        {
            if (renderingData.cameraData.cameraType == CameraType.Game)
            {
                renderer.EnqueuePass(m_ImGuiRenderPass);
            }
        }

        protected override void Dispose(bool disposing)
        {
            CoreUtils.Destroy(material);
        }
    }
}