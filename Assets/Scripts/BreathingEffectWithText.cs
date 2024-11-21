using UnityEngine;
using UnityEngine.UI;

public class BreathingEffectWithText : MonoBehaviour
{
    public float inhaleFOV = 50f; // FOV when zoomed in (inhale)
    public float exhaleFOV = 60f; // FOV when zoomed out (exhale)
    public float inhaleDuration = 2f; // Duration of the inhale (seconds)
    public float exhaleDuration = 2f; // Duration of the exhale (seconds)

    public Text breathingText; // Reference to the UI Text element

    private Camera mainCamera;
    private float targetFOV;
    private float currentDuration;
    private bool isInhaling = true;

    void Start()
    {
        mainCamera = GetComponent<Camera>();
        if (mainCamera == null)
        {
            Debug.LogError("BreathingEffect script must be attached to a Camera!");
            enabled = false;
            return;
        }

        if (breathingText == null)
        {
            Debug.LogError("Please assign a UI Text object to the BreathingEffectWithText script!");
            enabled = false;
            return;
        }

        targetFOV = inhaleFOV;
        currentDuration = inhaleDuration;

        // Set initial text
        UpdateBreathingText();
    }

    void Update()
    {
        // Smoothly interpolate the camera's FOV to the target FOV
        mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, targetFOV, Time.deltaTime / currentDuration);

        // Switch between inhale and exhale
        if (Mathf.Abs(mainCamera.fieldOfView - targetFOV) < 0.01f)
        {
            if (isInhaling)
            {
                isInhaling = false;
                targetFOV = exhaleFOV;
                currentDuration = exhaleDuration;
            }
            else
            {
                isInhaling = true;
                targetFOV = inhaleFOV;
                currentDuration = inhaleDuration;
            }

            // Update text when switching phases
            UpdateBreathingText();
        }
    }

    private void UpdateBreathingText()
    {
        if (breathingText != null)
        {
            breathingText.text = isInhaling ? "Inhale..." : "Exhale...";
        }
    }
}
