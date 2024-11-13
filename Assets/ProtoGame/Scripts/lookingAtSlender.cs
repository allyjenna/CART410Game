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
    int someInt = 30;
    public raycastSlender detetedScript;


    void Start()
    {
        color.a = 0f;
        hp = 1000f;

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
    if(detetedScript.detected == true)
    {
         if (looking == true)
        {
            color.a = color.a + drain * Time.deltaTime;
            hp = hp - hpDmg * Time.deltaTime;
            staticAudio.volume = staticAudio.volume + audioLouder * Time.deltaTime;
        }
    }
     

        if (looking == false || detetedScript.detected == false)
        {
            color.a = color.a - recharge * Time.deltaTime;
            staticAudio.volume = staticAudio.volume - audioLower * Time.deltaTime;
        }
        if(hp < 1)
        {
            SceneManager.LoadScene("menu");
             Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        }
    }

    void OnGUI(){
        GUI.skin.label.fontSize = someInt;
        GUI.Label (new Rect (400, 0, 500, 100), "Health :" + hp + "/1000");
    
    }

}
