using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FearManager : MonoBehaviour
{
    public enum FearState
    {
        None,
        SoundFear,
        VisualFear,
        ColorFear,
        ModelSkewFear
    }

    [Header("Biometric Settings")]
    public float biometricThreshold = 0.7f; // Threshold to trigger fears
    public float biometricValue = 0f; // Simulated biometric value for testing

    [Header("References")]
    public AreaAudioTrigger audioTrigger; // Handles sound fears
    public GlitchController glitchController; // Handles visual glitches
    public EffectController effectController; // Handles post-processing effects

    [Header("Fear State")]
    public FearState currentFearState = FearState.None;
    public string currentSceneName;

    [Header("Debugging")]
    public bool debugMode = true;

    private float logTimer = 0f; // Timer to control biometric value logging
    private float logInterval = 3f; // Log biometric value every 3 seconds

    private void Start()
    {
        // Get the current scene to determine the fear type
        currentSceneName = SceneManager.GetActiveScene().name;
        DetectFearState();
        Debug.Log($"[FearManager] Initialized for scene: {currentSceneName}");
    }

    private void Update()
    {
        // Simulate biometric value for testing
        biometricValue = Mathf.PingPong(Time.time * 0.1f, 1f);

        // Update the log timer
        logTimer += Time.deltaTime;
        if (logTimer >= logInterval)
        {
            if (debugMode)
                Debug.Log($"[FearManager] Biometric Value: {biometricValue}");
            logTimer = 0f; // Reset the timer
        }

        EvaluateFears();
    }

    private void DetectFearState()
    {
        switch (currentSceneName)
        {
            case "Test1_Sound":
                currentFearState = FearState.SoundFear;
                break;

            case "Test2_Visual":
                currentFearState = FearState.VisualFear;
                break;

            case "Test3_ModelSkew":
                currentFearState = FearState.ModelSkewFear;
                break;

            case "Test4_Colour":
                currentFearState = FearState.ColorFear;
                break;

            default:
                currentFearState = FearState.None;
                break;
        }
    }

    private void EvaluateFears()
    {
        switch (currentFearState)
        {
            case FearState.SoundFear:
                CheckSoundFears();
                break;

            case FearState.VisualFear:
                CheckVisualFears();
                break;

            case FearState.ColorFear:
                CheckColorFears();
                break;

            case FearState.ModelSkewFear:
                CheckModelSkewFear();
                break;

            case FearState.None:
            default:
                ResetFears();
                break;
        }
    }

    private void CheckSoundFears()
    {
        if (audioTrigger == null) return;

        string activeSound = audioTrigger.GetCurrentPlayingSound();
        if (!string.IsNullOrEmpty(activeSound) && biometricValue > biometricThreshold)
        {
            Debug.Log($"[FearManager] Sound fear triggered: {activeSound}");
        }
    }

    private void CheckVisualFears()
    {
        if (glitchController == null) return;

        List<string> activeGlitches = glitchController.GetActiveGlitches();
        if (activeGlitches.Count > 0 && biometricValue > biometricThreshold)
        {
            Debug.Log($"[FearManager] Visual fear(s) triggered: {string.Join(", ", activeGlitches)}");

            // Check for combinations of two glitches
            if (activeGlitches.Count == 2)
            {
                string combo = $"{activeGlitches[0]} + {activeGlitches[1]}";
                Debug.Log($"[FearManager] Visual fear combination triggered: {combo}");
            }
        }
    }

    private void CheckColorFears()
    {
        if (effectController == null) return;

        List<string> activeEffects = effectController.GetActiveEffects();
        if (activeEffects.Count > 0 && biometricValue > biometricThreshold)
        {
            Debug.Log($"[FearManager] Color fear(s) triggered: {string.Join(", ", activeEffects)}");
        }
    }

    private void CheckModelSkewFear()
    {
        // Model skewing is always active in this scene if biometrics exceed the threshold
        if (biometricValue > biometricThreshold)
        {
            Debug.Log("[FearManager] Model skew fear triggered.");
        }
    }

    private void ResetFears()
    {
        Debug.Log("[FearManager] Resetting all fears.");
    }
}
