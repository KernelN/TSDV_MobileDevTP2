using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PixelizeFeature : ScriptableRendererFeature
{
    [System.Serializable]
    class PixelizePassSettings
    {
        public RenderPassEvent renderPassEvent = RenderPassEvent.BeforeRenderingPostProcessing;
        public int screenHeight = 144;
    }
        
    class PixelizeRenderPass : ScriptableRenderPass
    {
        PixelizePassSettings settings;
        RenderTargetIdentifier colorBuffer, pixelBuffer;
        int pixelBufferID = Shader.PropertyToID("_PixelBuffer");

        Material material;
        int pixelScreenHeight, pixelScreenWidth;
        
        public PixelizeRenderPass(PixelizePassSettings settings)
        {
            this.settings = settings;
            renderPassEvent = settings.renderPassEvent;
            
            material = CoreUtils.CreateEngineMaterial("Hidden/Pixelize");
        }
        
        // This method is called before executing the render pass.
        // It can be used to configure render targets and their clear state. Also to create temporary render target textures.
        // When empty this render pass will render to the active camera render target.
        // You should never call CommandBuffer.SetRenderTarget. Instead call <c>ConfigureTarget</c> and <c>ConfigureClear</c>.
        // The render pipeline will ensure target setup and clearing happens in a performant manner.
        public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
        {
            CameraData cData = renderingData.cameraData;
            
            colorBuffer = cData.renderer.cameraColorTargetHandle;
            RenderTextureDescriptor d = cData.cameraTargetDescriptor;

            if (pixelScreenHeight != settings.screenHeight)
            //if (true)
            {
                pixelScreenHeight = settings.screenHeight;
                pixelScreenWidth = (int)(pixelScreenHeight * cData.camera.aspect + .5f);

                material.SetVector("_BlockCount", 
                                        new Vector2(pixelScreenWidth, pixelScreenHeight));
                material.SetVector("_BlockSize",
                                        new Vector2(1f / pixelScreenWidth, 1f / pixelScreenHeight));
                material.SetVector("_HalfBlockSize", 
                                        new Vector2(.5f / pixelScreenWidth, .5f / pixelScreenHeight));
            }
            
            d.height = pixelScreenHeight;
            d.width = pixelScreenWidth;

            cmd.GetTemporaryRT(pixelBufferID, d, FilterMode.Point);
            pixelBuffer = new RenderTargetIdentifier(pixelBufferID);
        }

        // Here you can implement the rendering logic.
        // Use <c>ScriptableRenderContext</c> to issue drawing commands or execute command buffers
        // https://docs.unity3d.com/ScriptReference/Rendering.ScriptableRenderContext.html
        // You don't have to call ScriptableRenderContext.submit, the render pipeline will call it at specific points in the pipeline.
        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            CommandBuffer cmd = CommandBufferPool.Get();
            using (new ProfilingScope(cmd, new ProfilingSampler("Pixelize Pass")))
            {
                Blit(cmd, colorBuffer, pixelBuffer, material);
                Blit(cmd, pixelBuffer, colorBuffer);
            }
            
            context.ExecuteCommandBuffer(cmd);
            CommandBufferPool.Release(cmd);
        }

        // Cleanup any allocated resources that were created during the execution of this render pass.
        public override void OnCameraCleanup(CommandBuffer cmd)
        {
            if(cmd == null) throw new System.ArgumentNullException("cmd");
            cmd.ReleaseTemporaryRT(pixelBufferID);
        }
    }

    [SerializeField] PixelizePassSettings settings;
    PixelizeRenderPass customPass;

    /// <inheritdoc/>
    public override void Create()
    {
        if(settings == null) settings = new PixelizePassSettings();
        customPass = new PixelizeRenderPass(settings);
    }

    // Here you can inject one or multiple render passes in the renderer.
    // This method is called when setting up the renderer once per-camera.
    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
#if UNITY_EDITOR
        if (renderingData.cameraData.isSceneViewCamera) return;
#endif
        
        renderer.EnqueuePass(customPass);
    }
}


