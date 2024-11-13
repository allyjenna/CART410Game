using UnityEngine;
using TMPro;

public class Calibration : MonoBehaviour
{
    public RectTransform breathingCircle; // Reference to the UI Circle
    public TMP_Text inhaleTMPText; // Reference to the "Inhale" TMP Text
    public TMP_Text exhaleTMPText; // Reference to the "Exhale" TMP Text
    public AudioSource inhaleExhaleAudio; // Optional audio for inhale/exhale prompts
    public AudioClip inhaleClip; // Audio for "Inhale"
    public AudioClip exhaleClip; // Audio for "Exhale"
    public float inhaleDuration = 4f; // Time for inhale
    public float exhaleDuration = 6f; // Time for exhale
    public float minCircleScale = 0.5f; // Minimum size of the circle
    public float maxCircleScale = 1.2f; // Maximum size of the circle

    private float timer = 0f;
    private bool isInhaling = true;

    private void Start()
    {
        UpdateTextAndAudio(true); // Start with "Inhale"
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (isInhaling)
        {
            // Smoothly expand the circle during inhale
            float progress = timer / inhaleDuration;
            breathingCircle.localScale = Vector3.Lerp(Vector3.one * minCircleScale, Vector3.one * maxCircleScale, progress);

            if (timer >= inhaleDuration)
            {
                // Switch to exhaling
                timer = 0f;
                isInhaling = false;
                UpdateTextAndAudio(false);
            }
        }
        else
        {
            // Smoothly contract the circle during exhale
            float progress = timer / exhaleDuration;
            breathingCircle.localScale = Vector3.Lerp(Vector3.one * maxCircleScale, Vector3.one * minCircleScale, progress);

            if (timer >= exhaleDuration)
            {
                // Switch to inhaling
                timer = 0f;
                isInhaling = true;
                UpdateTextAndAudio(true);
            }
        }
    }

    private void UpdateTextAndAudio(bool inhale)
    {
        // Show the appropriate TMP text
        inhaleTMPText.gameObject.SetActive(inhale);
        exhaleTMPText.gameObject.SetActive(!inhale);

        // Play the appropriate audio
        if (inhaleExhaleAudio != null)
        {
            inhaleExhaleAudio.clip = inhale ? inhaleClip : exhaleClip;
            inhaleExhaleAudio.Play();
        }
    }
}
