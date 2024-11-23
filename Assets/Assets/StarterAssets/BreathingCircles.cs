using UnityEngine;

public class BreathingCircles : MonoBehaviour
{
    public RectTransform[] circles; // Assign your circle layers in the Inspector
    public float inhaleScale = 1.2f; // Max scale during inhale
    public float exhaleScale = 0.8f; // Min scale during exhale
    public float duration = 2f; // Time for a full breathing cycle
    public float delayBetweenCircles = 0.2f; // Delay between each circle's animation
    public float maxAlpha = 0.5f; // Maximum transparency value (0 = invisible, 1 = fully opaque)

    private float time;

    void Update()
    {
        time += Time.deltaTime;

        for (int i = 0; i < circles.Length; i++)
        {
            // Calculate the progress of each circle's animation with a delay
            float progress = Mathf.PingPong((time - i * delayBetweenCircles) / duration, 1f);
            float scale = Mathf.Lerp(exhaleScale, inhaleScale, EaseInOutSine(progress));

            // Set the scale of the circle
            circles[i].localScale = new Vector3(scale, scale, 1f);

            // Set the transparency (alpha value)
            float alpha = Mathf.Lerp(maxAlpha, 0, progress); // Decreases transparency during inhale/exhale
            Color circleColor = circles[i].GetComponent<UnityEngine.UI.Image>().color;
            circleColor.a = alpha; // Adjust alpha
            circles[i].GetComponent<UnityEngine.UI.Image>().color = circleColor;
        }
    }

    // Easing function for smoother breathing motion
    private float EaseInOutSine(float t)
    {
        return -(Mathf.Cos(Mathf.PI * t) - 1) / 2;
    }
}
