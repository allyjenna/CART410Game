using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraGlitchEffect : MonoBehaviour
{
    public Material glitchMaterial;
    [Range(0, 1)]
    public float glitchAmount = 0.5f;

    private Camera camera;

    void Start()
    {
        camera = GetComponent<Camera>();
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (glitchMaterial != null)
        {
            glitchMaterial.SetFloat("_GlitchAmount", glitchAmount);
            Graphics.Blit(source, destination, glitchMaterial);
        }
        else
        {
            Graphics.Blit(source, destination);
        }
    }
}
