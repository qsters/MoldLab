using UnityEngine;
using UnityEngine.Experimental.Rendering;

namespace Helpers
{
    public class TextureHelper
    {
        public static void UpdateAndSetRenderTextures()
        {
            UpdateRenderTexture(ref Simulation.trailTexture);

            SetRenderTextures();
        }

        private static void SetRenderTextures()
        {
            // Setting Textures that may need to be updated, not including the Clearing texture
            Simulation.singleton.textureCS.SetTexture(Simulation.updateTrailKernel, "trailTexture",
                Simulation.trailTexture);
            Simulation.singleton.textureCS.SetTexture(Simulation.fadeTextureKernel, "textureToFade",
                Simulation.trailTexture);
            Simulation.singleton.textureCS.SetTexture(Simulation.invertTextureKernel, "textureToInvert",
                Simulation.trailTexture);

            Simulation.singleton.sporesCS.SetTexture(Simulation.updateSporePosKernel, "sensingTexture",
                Simulation.trailTexture);
        }

        private static void UpdateRenderTexture(ref RenderTexture texture)
        {
            if (texture == null || !texture.IsCreated() || texture.width != Simulation.simulationData.screenWidth ||
                texture.height != Simulation.simulationData.screenHeight || texture.graphicsFormat != GraphicsFormat.R16G16B16A16_SFloat)
            {
                if (texture != null)
                {
                    texture.Release();
                }

                texture = new RenderTexture(Simulation.simulationData.screenWidth, Simulation.simulationData.screenHeight, 1);
                texture.enableRandomWrite = true;
                texture.graphicsFormat = GraphicsFormat.R16G16B16A16_SFloat;

                texture.autoGenerateMips = false;
                texture.Create();
            }

            texture.wrapMode = TextureWrapMode.Clamp;
            texture.filterMode = FilterMode.Point;
        }

        // Clears the textures to black, assuming all textures are the same size as the screen.
        public static void ClearRenderTextures(RenderTexture[] textures)
        {
            foreach (var texture in textures)
            {
                Simulation.singleton.textureCS.SetBuffer(Simulation.clearTextureKernel, "settings",
                    Simulation.simulationDataBuffer);
                Simulation.singleton.textureCS.SetTexture(Simulation.clearTextureKernel, "textureToClear", texture);
                ComputeHelper.Dispatch(Simulation.singleton.textureCS, Simulation.simulationData.screenWidth, Simulation.simulationData.screenHeight, 1,
                    Simulation.clearTextureKernel);
            }
        }
    }
}