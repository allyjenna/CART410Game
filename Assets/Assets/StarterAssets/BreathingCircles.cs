using UnityEngine;
using UnityEngine.UI; // For Legacy Text component
using TMPro;

public class BreathingCircles : MonoBehaviour
{
    public RectTransform[] circles; // Array of breathing circles
    //public Text breathingText; // For Legacy Text
    public TMP_Text breathingText;
    public float inhaleDuration = 2f;
    public float exhaleDuration = 2f;
    private float timer = 0f;
    private bool isInhaling = true;

    void Start()
    {
        // Set the initial text to "Inhale"
        if (breathingText != null)
            breathingText.SetText("Inhale");
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (isInhaling)
        {
            // Animate the circles for inhaling
            float scale = Mathf.Lerp(0.5f, 1.5f, timer / inhaleDuration);
            foreach (var circle in circles)
            {
                circle.localScale = new Vector3(scale, scale, scale);
            }

            // Change text to "Exhale" after inhale duration
            if (timer >= inhaleDuration)
            {
                timer = 0f;
                isInhaling = false;

                // Change the text to "Exhale"
                if (breathingText != null)
                    breathingText.SetText("Exhale");
            }
        }
        else
        {
            // Animate the circles for exhaling
            float scale = Mathf.Lerp(1.5f, 0.5f, timer / exhaleDuration);
            foreach (var circle in circles)
            {
                circle.localScale = new Vector3(scale, scale, scale);
            }

            // Change text to "Inhale" after exhale duration
            if (timer >= exhaleDuration)
            {
                timer = 0f;
                isInhaling = true;

                // Change the text to "Inhale"
                if (breathingText != null)
                    breathingText.SetText("Inhale");
            }
        }
    }
}
