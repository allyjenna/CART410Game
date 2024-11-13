using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    public GameObject play, quit;
    public string gameSceneName;

  public void quitGame()
  {
    Application.Quit();
    Debug.Log("end");
  } 
  public void playGame()
  {
    SceneManager.LoadScene("Level1");
  }
}
