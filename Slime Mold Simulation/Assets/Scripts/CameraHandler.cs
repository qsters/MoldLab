using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        Graphics.Blit(Simulation.trailTexture, dest);
    }
}