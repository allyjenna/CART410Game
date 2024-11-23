using UnityEngine;

public class FlagTriggerHandler : MonoBehaviour
{
    public CustomSceneManager sceneManager; // Reference to the SceneManager

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("[FlagTriggerHandler] Player collided with Flag. Notifying SceneManager.");
            if (sceneManager != null)
            {
                sceneManager.OnPlayerFlagCollision();
            }
            else
            {
                Debug.LogWarning("[FlagTriggerHandler] SceneManager reference is missing!");
            }
        }
    }
}
