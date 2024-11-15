using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Include for Slider reference

public class Collecting : MonoBehaviour
{
    int someInt = 30;
    int papers = 0;
    public AudioSource paperPickUp;

    // Reference to the Slider UI component
    public Slider paperProgressBar;

    // Start is called before the first frame update
    void Start()
    {
        if (paperProgressBar != null)
        {
            paperProgressBar.maxValue = 3; // Total number of papers to collect
            paperProgressBar.value = 0; // Starting point of the progress bar
        }
        else
        {
            Debug.LogError("Paper Progress Bar is not assigned in the Inspector.");
        }
    }

    public void OnTriggerEnter(Collider Col)
    {
        if(Col.gameObject.tag == "Paper")
        {   
            Col.gameObject.SetActive(false);  // Deactivate the paper once collected
            papers++;  // Increase the paper count
            paperPickUp.Play();  // Play pickup sound

            // Update the slider value based on collected papers
            if (paperProgressBar != null)
            {
                paperProgressBar.value = papers;  // Update slider to reflect the collected papers
            }
        }
        
        // Check if 3 papers are collected and load the "end" scene
        if(papers == 3)
        {
            SceneManager.LoadScene("end");
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    // void OnGUI()
    // {
    //     // Display the number of collected papers
    //     GUI.skin.label.fontSize = someInt;
    //     GUI.Label(new Rect(0, 400, 500, 100), "Papers collected: " + papers + "/3");
    // }
}
