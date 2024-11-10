using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNext : MonoBehaviour
{
    // Tag the flag GameObject with "Flag" in the Inspector to ensure proper detection.
    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object is the player (by tag or by checking for a specific component)
        if (other.CompareTag("Player"))
        {
            // Load the next scene in the Build Settings
            LoadNextScene();
        }
    }

    private void LoadNextScene()
    {
        // Get the current active scene index
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Calculate the index of the next scene
        int nextSceneIndex = currentSceneIndex + 1;

        // Check if there is a next scene to load
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            // Load the next scene
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("No more scenes to load. You are at the last scene.");
        }
    }
}
