using UnityEngine;

public class BloodyTextureGenerator : MonoBehaviour
{
    // The size of the texture
    public int textureWidth = 512;
    public int textureHeight = 512;

    // The material to apply the texture to
    public Material material;

    // Blood color and transparency (RGBA)
    public Color bloodColor = new Color(1f, 0f, 0f, 1f); // Red color with full opacity
    public Color paperColor = new Color(1f, 1f, 1f, 1f); // White paper color with full opacity

    // Blood density (how often blood appears on the texture)
    public float bloodDensity = 0.2f; // Increase this value for more blood

    void Start()
    {
        // Generate the texture
        Texture2D bloodTexture = GenerateBloodyTexture(textureWidth, textureHeight);

        // Apply the texture to the material
        material.mainTexture = bloodTexture;

        // Ensure material uses an unlit shader that supports transparency
        material.shader = Shader.Find("Unlit/Transparent");

        // Set the filter mode to point to avoid texture blurring
        bloodTexture.filterMode = FilterMode.Point;

        // Apply the texture to the material
        bloodTexture.Apply();
    }

    // Generate a bloody texture with splatter effect
    Texture2D GenerateBloodyTexture(int width, int height)
    {
        // Create a new texture with the given width and height
        Texture2D texture = new Texture2D(width, height);

        // Loop through each pixel in the texture
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                // Randomly generate blood splatters with adjusted density
                float distToCenter = Mathf.Sqrt(Mathf.Pow(x - width / 2, 2) + Mathf.Pow(y - height / 2, 2));

                // Blood splatter effect - increased density for stronger blood
                if (Random.value < bloodDensity || (distToCenter < 100f && Random.value < bloodDensity))
                {
                    texture.SetPixel(x, y, bloodColor); // Set blood pixels
                }
                else
                {
                    texture.SetPixel(x, y, paperColor); // Set paper color
                }
            }
        }

        // Apply changes to the texture
        texture.Apply();

        return texture;
    }
}
