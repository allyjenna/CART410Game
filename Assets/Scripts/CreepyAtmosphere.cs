using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class CreepyAtmosphere : MonoBehaviour
{
    private Volume volume;
    private ColorAdjustments colorAdjustments;
    private Vignette vignette;

    // Parameters for the creepy effect
    [Range(-100, 100)]
    public float targetSaturation = -100f; // Desaturate to make colors look washed out
    [Range(-100, 100)]
    public float targetContrast = 30f;     // Increase contrast for a harsher look
    public Color tintColor = new Color(0.6f, 0.2f, 0.2f); // Tint for a disturbing reddish hue
    public float vignetteIntensity = 0.5f; // Intensity of the vignette for a closed-in feel

    private float originalSaturation;
    private float originalContrast;
    private Color originalColor;
    private float originalVignetteIntensity;

    private void Start()
    {
        // Get the Volume component from the scene
        volume = GetComponent<Volume>();

        // Ensure the Volume component exists and has overrides
        if (volume != null && volume.profile != null)
        {
            // Check if Color Adjustments and Vignette are in the profile
            if (volume.profile.TryGet(out colorAdjustments) && volume.profile.TryGet(out vignette))
            {
                // Store original settings to revert if needed
                originalSaturation = colorAdjustments.saturation.value;
                originalContrast = colorAdjustments.contrast.value;
                originalColor = colorAdjustments.colorFilter.value;
                originalVignetteIntensity = vignette.intensity.value;

                // Set initial values for the creepy effect
                ApplyCreepyEffect();
            }
            else
            {
                Debug.LogError("Color Adjustments or Vignette are missing from the Volume Profile.");
            }
        }
        else
        {
            Debug.LogError("Volume or Volume Profile is missing on this GameObject.");
        }
    }

    private void ApplyCreepyEffect()
    {
        if (colorAdjustments != null)
        {
            // Apply creepy settings
            colorAdjustments.saturation.value = targetSaturation;
            colorAdjustments.contrast.value = targetContrast;
            colorAdjustments.colorFilter.value = tintColor;
        }

        if (vignette != null)
        {
            // Apply vignette effect
            vignette.intensity.value = vignetteIntensity;
            vignette.smoothness.value = 0.8f; // Hard edges for a harsher vignette
        }
    }

    private void OnDisable()
    {
        // Reset to original settings if the script is disabled
        if (colorAdjustments != null)
        {
            colorAdjustments.saturation.value = originalSaturation;
            colorAdjustments.contrast.value = originalContrast;
            colorAdjustments.colorFilter.value = originalColor;
        }

        if (vignette != null)
        {
            vignette.intensity.value = originalVignetteIntensity;
        }
    }
}
