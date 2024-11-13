using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraPixelationEffect : MonoBehaviour
{
    public Material pixelationMaterial;
    [Range(1, 100)]
    public float pixelSize = 8f;

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (pixelationMaterial != null)
        {
            pixelationMaterial.SetFloat("_PixelSize", pixelSize);
            Graphics.Blit(source, destination, pixelationMaterial);
        }
        else
        {
            Graphics.Blit(source, destination);
        }
    }
}
