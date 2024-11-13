using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class enemyCollide : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.name == "enemy")
        {
            Debug.Log("COLLIDED");
            
            restartCurrentLevel();
        }
    }
    void  restartCurrentLevel()
    {
SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
