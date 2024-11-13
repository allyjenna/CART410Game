using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraPixelationEffect : MonoBehaviour
{
    public Material pixelationMaterial;

    // Extended range for finer control
    [Range(0.1f, 1000f)]
    public float pixelSize = 8f;

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (pixelationMaterial != null)
        {
            // Pass pixel size to the shader
            pixelationMaterial.SetFloat("_PixelSize", Mathf.Max(0.1f, pixelSize)); // Prevent values <= 0
            Graphics.Blit(source, destination, pixelationMaterial);
        }
        else
        {
            Graphics.Blit(source, destination);
        }
    }
}
