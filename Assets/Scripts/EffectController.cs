using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using System.Collections.Generic;


public class EffectController : MonoBehaviour
{
    public PostProcessVolume postProcessVolume;

    private ChromaticAberration chromaticAberration;
    private Bloom bloom;
    private ColorGrading colorGrading;
    private LensDistortion lensDistortion;
    private AutoExposure autoExposure;

    private float timer = 0f;

    private void Start()
    {
        // Retrieve specific post-processing effects
        if (postProcessVolume.profile.TryGetSettings(out chromaticAberration))
            Debug.Log("Chromatic Aberration available.");
        if (postProcessVolume.profile.TryGetSettings(out bloom))
            Debug.Log("Bloom available.");
        if (postProcessVolume.profile.TryGetSettings(out colorGrading))
            Debug.Log("Color Grading available.");
        if (postProcessVolume.profile.TryGetSettings(out lensDistortion))
            Debug.Log("Lens Distortion available.");
        if (postProcessVolume.profile.TryGetSettings(out autoExposure))
            Debug.Log("Auto Exposure available.");

        // Set chromatic aberration to a constant high intensity
        if (chromaticAberration != null)
        {
            chromaticAberration.intensity.value = 1f; // Set to maximum for continuous effect
            Debug.Log("Chromatic Aberration set to constant high intensity");
        }

        // Start the effect cycling with updates for other effects
        InvokeRepeating("ApplyDistortedEffects", 0f, 0.02f); // Update every 0.02 seconds
    }

    private void ApplyDistortedEffects()
    {
        timer += 0.02f; // Increment time for smooth transitions

        // Randomize intensities slightly each frame for a hint of unpredictability
        float randomOffset = Random.Range(0.9f, 1.1f);

        // Apply sporadic bloom with moderate intensity for occasional flashes
        if (bloom != null)
        {
            float intensity = 5f + Mathf.Abs(Mathf.Sin(timer * Mathf.PI * 0.5f)) * 8f * randomOffset; // Ranges from 5 to 13
            bloom.intensity.value = Mathf.Clamp(intensity, 5f, 13f); // Keeps bloom strong but less chaotic
            Debug.Log($"Bloom Intensity: {bloom.intensity.value}");
        }

        // Subtle, slower color grading changes for an eerie atmosphere without flickering
        if (colorGrading != null)
        {
            // Smoother, subtle contrast and saturation oscillation
            float contrast = Mathf.Sin(timer * Mathf.PI * 0.25f) * 50f; // Range -50 to +50 for softer effect
            float saturation = Mathf.Sin(timer * Mathf.PI * 0.25f) * 100f; // Range -100 to +100 for balanced color shifts

            // Gentle color filter cycling between muted shades for a subtle color tint
            Color colorFilter = Color.Lerp(Color.red, Color.blue, Mathf.PingPong(timer * 0.2f, 1f));
            colorFilter = Color.Lerp(colorFilter, Color.green, Mathf.PingPong(timer * 0.1f, 1f));

            colorGrading.contrast.value = contrast;
            colorGrading.saturation.value = saturation;
            colorGrading.colorFilter.value = colorFilter;

            Debug.Log($"Color Grading - Contrast: {contrast}, Saturation: {saturation}, Color Filter: {colorFilter}");
        }

        // Apply more frequent lens distortion for rapid pulsing effect
        if (lensDistortion != null)
        {
            float distortionIntensity = Mathf.Sin(timer * Mathf.PI * 1.5f) * -40f * randomOffset; // Higher frequency with range -40 to 20
            lensDistortion.intensity.value = Mathf.Clamp(distortionIntensity, -40f, 20f); // Keeps distortion extreme and frequent
            lensDistortion.scale.value = 0.85f; // Slightly smaller scale for more intense distortion
            Debug.Log($"Lens Distortion Intensity: {distortionIntensity}");
        }

        // Apply auto exposure for subtle brightness shifts
        if (autoExposure != null)
        {
            // Smoothly oscillate min and max exposure to create a "breathing" light effect
            float minExposure = Mathf.Clamp(Mathf.Sin(timer * Mathf.PI * 0.5f) - 1f, -2f, 0f); // Range -2 to 0
            float maxExposure = Mathf.Clamp(Mathf.Sin(timer * Mathf.PI * 0.5f) + 1f, 0f, 2f); // Range 0 to 2

            autoExposure.minLuminance.value = minExposure;
            autoExposure.maxLuminance.value = maxExposure;

            Debug.Log($"Auto Exposure - Min: {minExposure}, Max: {maxExposure}");
        }
    }

    public List<string> GetActiveEffects()
    {
        List<string> activeEffects = new List<string>();

        if (chromaticAberration != null && chromaticAberration.intensity.value > 0.5f)
            activeEffects.Add("Chromatic Aberration");

        if (bloom != null && bloom.intensity.value > 5f)
            activeEffects.Add("Bloom");

        if (colorGrading != null && (colorGrading.saturation.value < 0 || colorGrading.contrast.value != 0))
            activeEffects.Add("Color Grading");

        if (lensDistortion != null && lensDistortion.intensity.value != 0)
            activeEffects.Add("Lens Distortion");

        if (autoExposure != null && autoExposure.minLuminance.value < 0)
            activeEffects.Add("Auto Exposure");

        return activeEffects;
    }

}
