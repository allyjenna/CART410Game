using UnityEngine;

public class BreathingCircles : MonoBehaviour
{
    public RectTransform[] circles; // Array to hold the circles
    public float inhaleScale = 1.2f; // Max scale during inhale
    public float exhaleScale = 0.8f; // Min scale during exhale
    public float duration = 3f; // Time for a full breathing cycle
    public float delayBetweenCircles = 0.3f; // Delay between each circle's animation
    public float maxAlpha = 0.6f; // Maximum transparency value (0 = invisible, 1 = fully opaque)
    public Color circleColor = new Color(0.5f, 0.8f, 1f); // Soft blue color for circles

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
            float alpha = Mathf.Lerp(maxAlpha, 0, Mathf.Pow(progress, 2)); // Easing transparency for a smoother fade
            Color circleColorWithAlpha = circleColor;
            circleColorWithAlpha.a = alpha;

            // Apply the color and alpha to each circle
            circles[i].GetComponent<UnityEngine.UI.Image>().color = circleColorWithAlpha;
        }
    }

    // Easing function for smoother breathing motion (smooth in and out)
    private float EaseInOutSine(float t)
    {
        return -(Mathf.Cos(Mathf.PI * t) - 1) / 2;
    }
}
