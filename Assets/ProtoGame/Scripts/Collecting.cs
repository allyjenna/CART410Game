using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Collecting : MonoBehaviour
{
    int papers = 0;
    public AudioSource paperPickUp;

    // Array of Image components representing the paper icons in the UI
    public Image[] paperIcons;

    void Start()
    {
        if (paperIcons.Length != 3)
        {
            Debug.LogError("Please assign exactly 3 paper icons in the Inspector.");
        }
    }

    public void OnTriggerEnter(Collider Col)
    {
        if (Col.gameObject.tag == "Paper")
        {   
            Col.gameObject.SetActive(false);  // Deactivate the paper once collected
            papers++;  // Increase the paper count
            paperPickUp.Play();  // Play pickup sound

            // Update the UI to hide the collected paper icon
            if (papers <= paperIcons.Length)
            {
                paperIcons[papers - 1].gameObject.SetActive(false);  // Hide the collected paper icon
            }
        }

        // Check if 3 papers are collected and load the "end" scene
        if (papers == 3)
        {
            SceneManager.LoadScene("end");
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
