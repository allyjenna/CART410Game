using UnityEngine;
using System.Collections.Generic;


public class GlitchController : MonoBehaviour
{
    [Header("Effect References")]
    public CameraGlitchEffect cameraGlitchEffect;
    public CameraPixelationEffect cameraPixelationEffect;
    public Kino.DigitalGlitch digitalGlitchEffect;
    public Kino.AnalogGlitch analogGlitchEffect;

    [Header("Effect Durations")]
    public float effectDuration = 5f; // Duration for each set of active effects
    public float transitionSpeed = 1f; // Speed of oscillations

    [Header("Effect Intensities")]
    public float maxGlitchAmount = 0.3f;
    public int minPixelSize = 99; // Pixelation oscillates between 99 and 100
    public int maxPixelSize = 100;
    public int disabledPixelSize = 1000; // Effectively disables pixelation by making pixels very small
    public float maxDigitalIntensity = 0.5f;
    public float maxScanLineJitter = 0.5f;
    public float maxVerticalJump = 0.3f;
    public float maxHorizontalShake = 0.2f;
    public float maxColorDrift = 0.3f;

    private float timer = 0f;
    private int activeEffectsCount = 0; // Tracks the number of active effects
    private bool[] activeEffects; // Array to track which effects are currently active

    private float glitchOscillationTime = 0f;
    private float pixelationOscillationTime = 0f;
    private float digitalGlitchOscillationTime = 0f;
    private float analogGlitchOscillationTime = 0f;

    private void Start()
    {
        activeEffects = new bool[4]; // Initialize the active effects array
        RandomizeActiveEffects(); // Randomize the first set of effects
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= effectDuration)
        {
            timer = 0f;
            RandomizeActiveEffects();
        }

        // Update only the active effects
        if (activeEffects[0]) UpdateCameraGlitchEffect();
        else DisableCameraGlitchEffect();

        if (activeEffects[1]) UpdateCameraPixelationEffect();
        else ResetCameraPixelationEffect(); // Set to disabledPixelSize instead of solid color

        if (activeEffects[2]) UpdateDigitalGlitchEffect();
        else DisableDigitalGlitchEffect();

        if (activeEffects[3]) UpdateAnalogGlitchEffect();
        else DisableAnalogGlitchEffect();
    }

    private void UpdateCameraGlitchEffect()
    {
        if (cameraGlitchEffect != null)
        {
            glitchOscillationTime += Time.deltaTime * transitionSpeed;
            float intensityModifier = activeEffectsCount == 2 ? 0.5f : 1f; // Scale down intensity if two effects are active
            float glitchAmount = Mathf.Lerp(0f, maxGlitchAmount * intensityModifier, Mathf.Sin(glitchOscillationTime) * 0.5f + 0.5f);
            cameraGlitchEffect.glitchAmount = glitchAmount;
            //Debug.Log($"Camera Glitch Amount: {glitchAmount}");
        }
    }

    private void DisableCameraGlitchEffect()
    {
        if (cameraGlitchEffect != null)
            cameraGlitchEffect.glitchAmount = 0f;
    }

    private void UpdateCameraPixelationEffect()
    {
        if (cameraPixelationEffect != null)
        {
            pixelationOscillationTime += Time.deltaTime * transitionSpeed;

            // Oscillate only between 99 and 100
            int pixelSize = Mathf.RoundToInt(Mathf.Lerp(minPixelSize, maxPixelSize, Mathf.Sin(pixelationOscillationTime) * 0.5f + 0.5f));
            cameraPixelationEffect.pixelSize = pixelSize;

            //Debug.Log($"Pixelation Size: {pixelSize}");
        }
    }

    private void ResetCameraPixelationEffect()
    {
        if (cameraPixelationEffect != null)
        {
            // Set to a very high value to disable the effect
            cameraPixelationEffect.pixelSize = disabledPixelSize; // Effectively turns off visible pixelation
        }
    }

    private void UpdateDigitalGlitchEffect()
    {
        if (digitalGlitchEffect != null)
        {
            digitalGlitchOscillationTime += Time.deltaTime * transitionSpeed;
            float intensityModifier = activeEffectsCount == 2 ? 0.5f : 1f; // Scale down intensity if two effects are active
            float intensity = Mathf.Lerp(0f, maxDigitalIntensity * intensityModifier, Mathf.Sin(digitalGlitchOscillationTime) * 0.5f + 0.5f);
            digitalGlitchEffect.intensity = intensity;
            //Debug.Log($"Digital Glitch Intensity: {intensity}");
        }
    }

    private void DisableDigitalGlitchEffect()
    {
        if (digitalGlitchEffect != null)
            digitalGlitchEffect.intensity = 0f;
    }

    private void UpdateAnalogGlitchEffect()
    {
        if (analogGlitchEffect != null)
        {
            analogGlitchOscillationTime += Time.deltaTime * transitionSpeed;
            float intensityModifier = activeEffectsCount == 2 ? 0.5f : 1f; // Scale down intensity if two effects are active

            float scanLineJitter = Mathf.Lerp(0f, maxScanLineJitter * intensityModifier, Mathf.Sin(analogGlitchOscillationTime) * 0.5f + 0.5f);
            float verticalJump = Mathf.Lerp(0f, maxVerticalJump * intensityModifier, Mathf.Cos(analogGlitchOscillationTime) * 0.5f + 0.5f);
            float horizontalShake = Mathf.Lerp(0f, maxHorizontalShake * intensityModifier, Mathf.Sin(analogGlitchOscillationTime * 1.5f) * 0.5f + 0.5f);
            float colorDrift = Mathf.Lerp(0f, maxColorDrift * intensityModifier, Mathf.Cos(analogGlitchOscillationTime * 2f) * 0.5f + 0.5f);

            analogGlitchEffect.scanLineJitter = scanLineJitter;
            analogGlitchEffect.verticalJump = verticalJump;
            analogGlitchEffect.horizontalShake = horizontalShake;
            analogGlitchEffect.colorDrift = colorDrift;

            //Debug.Log($"Analog Glitch - ScanLine: {scanLineJitter}, VerticalJump: {verticalJump}, HorizontalShake: {horizontalShake}, ColorDrift: {colorDrift}");
        }
    }

    private void DisableAnalogGlitchEffect()
    {
        if (analogGlitchEffect != null)
        {
            analogGlitchEffect.scanLineJitter = 0f;
            analogGlitchEffect.verticalJump = 0f;
            analogGlitchEffect.horizontalShake = 0f;
            analogGlitchEffect.colorDrift = 0f;
        }
    }

    private void RandomizeActiveEffects()
    {
        // Reset all effects
        for (int i = 0; i < activeEffects.Length; i++)
        {
            activeEffects[i] = false;
        }

        // Randomize the number of active effects (1 or 2)
        activeEffectsCount = Random.Range(1, 3);

        // Randomly activate effects
        for (int i = 0; i < activeEffectsCount; i++)
        {
            int randomEffect;
            do
            {
                randomEffect = Random.Range(0, activeEffects.Length);
            } while (activeEffects[randomEffect]); // Ensure no duplicates

            activeEffects[randomEffect] = true;
        }

        Debug.Log($"Active Effects: {GetActiveEffectsDebugString()}");
    }

    private string GetActiveEffectsDebugString()
    {
        string[] effectNames = { "Camera Glitch", "Pixelation", "Digital Glitch", "Analog Glitch" };
        string activeEffectsString = "";

        for (int i = 0; i < activeEffects.Length; i++)
        {
            if (activeEffects[i])
            {
                activeEffectsString += effectNames[i] + ", ";
            }
        }

        return activeEffectsString.TrimEnd(',', ' ');
    }

    public List<string> GetActiveGlitches()
    {
        List<string> activeGlitches = new List<string>();

        if (cameraGlitchEffect != null && cameraGlitchEffect.glitchAmount > 0)
            activeGlitches.Add("Camera Glitch");

        if (cameraPixelationEffect != null && cameraPixelationEffect.pixelSize < disabledPixelSize)
            activeGlitches.Add("Pixelation");

        if (digitalGlitchEffect != null && digitalGlitchEffect.intensity > 0)
            activeGlitches.Add("Digital Glitch");

        if (analogGlitchEffect != null && (analogGlitchEffect.scanLineJitter > 0 || analogGlitchEffect.colorDrift > 0))
            activeGlitches.Add("Analog Glitch");

        return activeGlitches;
    }

}
