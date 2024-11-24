using UnityEngine;
using UnityEngine.SceneManagement;

public class CustomSceneManager : MonoBehaviour
{
    private float calibrationDuration = 20f; // Duration for calibration scenes
    private bool isCalibrationActive = false; // Flag to check if calibration is running
    private float calibrationTimer = 0f;

    private string[] sceneOrder = {
        "Calibration",
        "Test1_Sound",
        "Calibration",
        "Test2_Visual",
        "Calibration",
        "Test3_ModelSkew",
        "Calibration",
        "Test4_Colour",
        "Game"
    };

    private int currentSceneIndex = 0; // Tracks the current scene in the custom order
    private bool waitingForSceneLoad = false; // Prevent updates during scene load

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject); // Persist this object across scenes

        // Identify the current scene if starting mid-sequence
        string currentSceneName = SceneManager.GetActiveScene().name;

        for (int i = 0; i < sceneOrder.Length; i++)
        {
            if (sceneOrder[i] == currentSceneName)
            {
                currentSceneIndex = i;
                Debug.Log($"[CustomSceneManager] Starting directly in scene: {currentSceneName} at index {i}");
                InitializeScene(currentSceneName);
                return;
            }
        }

        // If the scene isn't in the list, log an error
        Debug.LogError($"[CustomSceneManager] Scene '{currentSceneName}' not found in the scene order!");
    }

    private void Update()
    {
        if (waitingForSceneLoad) return; // Skip updates while loading a scene

        if (isCalibrationActive)
        {
            calibrationTimer += Time.deltaTime;
            Debug.Log($"[CustomSceneManager] Calibration Timer: {calibrationTimer:F2}/{calibrationDuration}");

            if (calibrationTimer >= calibrationDuration)
            {
                Debug.Log("[CustomSceneManager] Calibration complete. Loading next scene.");
                isCalibrationActive = false;
                calibrationTimer = 0f;
                LoadNextScene();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"[CustomSceneManager] Collision detected with object: {other.gameObject.name}");

        if ((other.CompareTag("Player") && CheckForFlagCollision(other)) ||
            (other.CompareTag("Flag") && CheckForPlayerCollision(other)))
        {
            Debug.Log("[CustomSceneManager] Valid collision detected between Player and Flag.");
            if (!isCalibrationActive && !IsCalibrationScene())
            {
                Debug.Log("[CustomSceneManager] Player collided with Flag. Loading next scene.");
                LoadNextScene();
            }
        }
        else
        {
            Debug.Log("[CustomSceneManager] Collision ignored. Either tags do not match or conditions not met.");
        }
    }

    public void OnPlayerFlagCollision()
    {
        if (!isCalibrationActive && !IsCalibrationScene())
        {
            Debug.Log("[CustomSceneManager] Received collision event from Flag. Loading next scene.");
            LoadNextScene();
        }
        else
        {
            Debug.Log("[CustomSceneManager] Collision event received during calibration. Ignored.");
        }
    }


    private bool CheckForPlayerCollision(Collider collider)
    {
        return collider.gameObject.CompareTag("Player");
    }

    private bool CheckForFlagCollision(Collider collider)
    {
        return collider.gameObject.CompareTag("Flag");
    }

    private void LoadNextScene()
    {
        currentSceneIndex++;

        if (currentSceneIndex < sceneOrder.Length)
        {
            LoadScene(sceneOrder[currentSceneIndex]);
        }
        else
        {
            Debug.Log("[CustomSceneManager] No more scenes to load. Game completed.");
        }
    }

    private void LoadScene(string sceneName)
    {
        if (SceneManager.GetActiveScene().name == sceneName)
        {
            Debug.Log($"[CustomSceneManager] Scene {sceneName} is already active. No need to reload.");
            InitializeScene(sceneName); // Ensure scene is initialized properly
            return;
        }

        Debug.Log($"[CustomSceneManager] Loading scene: {sceneName}");
        waitingForSceneLoad = true; // Prevent updates during scene load
        SceneManager.LoadScene(sceneName);
        StartCoroutine(InitializeSceneCoroutine(sceneName));
    }

    private System.Collections.IEnumerator InitializeSceneCoroutine(string sceneName)
    {
        // Wait for the scene to fully load
        yield return null;

        Debug.Log($"[CustomSceneManager] Scene {sceneName} loaded.");
        waitingForSceneLoad = false; // Scene is now ready
        InitializeScene(sceneName);
    }

    private void InitializeScene(string sceneName)
    {
        if (sceneName == "Calibration")
        {
            Debug.Log("[CustomSceneManager] Calibration started.");
            isCalibrationActive = true;
            calibrationTimer = 0f;
        }
        else
        {
            isCalibrationActive = false;
        }
    }

    private bool IsCalibrationScene()
    {
        return sceneOrder[currentSceneIndex] == "Calibration";
    }
}
