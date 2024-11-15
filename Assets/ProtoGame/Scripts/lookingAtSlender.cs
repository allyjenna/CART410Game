using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class lookingAtSlender : MonoBehaviour
{
    public RawImage staticImage;
    public Color color;
    public float drain, recharge, hp, hpDmg; //health & image 
    public bool looking; //looking at slender
    public float audioLouder, audioLower;
    public AudioSource staticAudio;
    public Slider healthBar; // Add a public Slider for health bar
    int someInt = 30;
    public raycastSlender detetedScript;

    void Start()
    {
        color.a = 0f;
        hp = 1000f;

        // Set the slider’s max value to match the initial health value
        healthBar.maxValue = hp;
        healthBar.value = hp;
    }

    void OnBecameVisible()
    {
        looking = true;
    }

    void OnBecameInvisible()
    {
        looking = false;
    }

    void FixedUpdate()
    {
        staticImage.color = color;

        if (detetedScript.detected)
        {
            if (looking)
            {
                color.a += drain * Time.deltaTime;
                hp -= hpDmg * Time.deltaTime;
                staticAudio.volume += audioLouder * Time.deltaTime;
            }
        }

        if (!looking || !detetedScript.detected)
        {
            color.a -= recharge * Time.deltaTime;
            staticAudio.volume -= audioLower * Time.deltaTime;
        }

        // Update the health bar value
        healthBar.value = hp;

        // Check if health is below 1
        if (hp < 1)
        {
            SceneManager.LoadScene("menu");
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
