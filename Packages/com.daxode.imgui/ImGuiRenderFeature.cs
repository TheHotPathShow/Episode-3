using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace com.daxode.imgui
{
    public class ImGuiRenderFeature : ScriptableRendererFeature
    {
        [SerializeField] private Shader shader;
        private Material material;
        private ImGuiRenderPass m_RenderPass;

        public override void Create()
        {
            // if (shader == null)
            //     return;
            material = CoreUtils.CreateEngineMaterial(Shader.Find("Hidden/ImGuiDrawShader"));
            m_RenderPass = new ImGuiRenderPass(material);
        }

        public override void AddRenderPasses(ScriptableRenderer renderer,
            ref RenderingData renderingData)
        {
            if (renderingData.cameraData.cameraType == CameraType.Game) 
                renderer.EnqueuePass(m_RenderPass);
        }
        
        public override void SetupRenderPasses(ScriptableRenderer renderer,
            in RenderingData renderingData)
        {
            if (renderingData.cameraData.cameraType == CameraType.Game)
            {
                // Calling ConfigureInput with the ScriptableRenderPassInput.Color argument
                // ensures that the opaque texture is available to the Render Pass.
                // m_RenderPass.ConfigureInput(ScriptableRenderPassInput.Color);
                m_RenderPass.SetTarget(renderer.cameraColorTargetHandle);
            }
        }

        protected override void Dispose(bool disposing)
        {
            CoreUtils.Destroy(material);
        }
    }
}