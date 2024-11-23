using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BreathingLayersEffect : MonoBehaviour
{
    [Header("Camera Breathing Settings")]
    public float inhaleFOV = 50f;
    public float exhaleFOV = 60f;
    public float inhaleDuration = 2f;
    public float exhaleDuration = 2f;

    [Header("Layered Effect Settings")]
    public RectTransform layerParent; // Parent object for layers
    public int numberOfLayers = 5; // Number of expanding layers
    public float maxScale = 1.5f; // Maximum scale for outermost layer
    public float minScale = 0.5f; // Minimum scale for innermost layer
    public float fadeStrength = 0.5f; // Transparency fade between layers
    public Color baseColor = new Color(0.2f, 0.8f, 1f, 0.5f);
    public float layerSizeMultiplier = 800f; // Base size of each layer for proximity adjustment

    private List<RectTransform> layers = new List<RectTransform>();
    private bool isInhaling = true;
    private float targetFOV;
    private float currentDuration;

    private Camera mainCamera;

    void Start()
    {
        // Assign main camera
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("No main camera found in the scene!");
            enabled = false;
            return;
        }

        // Generate layers
        GenerateLayers();

        targetFOV = inhaleFOV;
        currentDuration = inhaleDuration;
    }

    void Update()
    {
        // Adjust camera FOV
        mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, targetFOV, Time.deltaTime / currentDuration);

        // Animate layers
        float scaleFactor = isInhaling ? maxScale : minScale;
        float baseAlpha = isInhaling ? 1f : 0.3f;

        for (int i = 0; i < layers.Count; i++)
        {
            float layerScale = Mathf.Lerp(layers[i].localScale.x, scaleFactor * (1f - (i * 0.1f)), Time.deltaTime / currentDuration);
            layers[i].localScale = new Vector3(layerScale, layerScale, 1f);

            // Fade alpha for transparency
            Image layerImage = layers[i].GetComponent<Image>();
            if (layerImage != null)
            {
                float alpha = Mathf.Lerp(layerImage.color.a, baseAlpha - (i * fadeStrength), Time.deltaTime / currentDuration);
                layerImage.color = new Color(baseColor.r, baseColor.g, baseColor.b, Mathf.Clamp01(alpha));
            }
        }

        // Switch phases when animation is complete
        if (Mathf.Abs(mainCamera.fieldOfView - targetFOV) < 0.01f)
        {
            isInhaling = !isInhaling;
            targetFOV = isInhaling ? inhaleFOV : exhaleFOV;
            currentDuration = isInhaling ? inhaleDuration : exhaleDuration;
        }
    }

    void GenerateLayers()
    {
        // Clear existing layers
        foreach (RectTransform layer in layers)
        {
            Destroy(layer.gameObject);
        }
        layers.Clear();

        // Generate new layers
        for (int i = 0; i < numberOfLayers; i++)
        {
            GameObject layerObject = new GameObject("Layer" + i, typeof(RectTransform), typeof(Image));
            RectTransform layerTransform = layerObject.GetComponent<RectTransform>();
            layerTransform.SetParent(layerParent, false);

            layerTransform.anchorMin = new Vector2(0.5f, 0.5f);
            layerTransform.anchorMax = new Vector2(0.5f, 0.5f);
            layerTransform.pivot = new Vector2(0.5f, 0.5f);

            // Adjust size for proximity
            float layerSize = layerSizeMultiplier * (1f - (i * 0.1f));
            layerTransform.sizeDelta = new Vector2(layerSize, layerSize);

            Image layerImage = layerObject.GetComponent<Image>();
            layerImage.color = new Color(baseColor.r, baseColor.g, baseColor.b, baseColor.a - (i * fadeStrength));
            layerImage.raycastTarget = false;

            layers.Add(layerTransform);
        }
    }
}
