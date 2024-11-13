using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slenderMovement : MonoBehaviour
{
   public Transform place1, place2, place3, place4, place5, place6, player;
   bool teleporting = true;
   public float teleportTime;
   int randomNum;

   void Start()
   {
    StartCoroutine(teleport()); //returns upon the first yield return, however you can yield the result, which waits until the coroutine has finished execution.
   }
   void Update()
   {
    this.transform.LookAt(new Vector3(player.position.x, this.transform.position.y, player.position.z));
   }

   IEnumerator teleport()
   {
        while (teleporting == true)
        {
            yield return new WaitForSeconds(teleportTime);
            randomNum = Random.Range(0, 12);
            if(randomNum == 0)
            {
                this.transform.position = place1.position;
            }
            if(randomNum == 1)
            {
                this.transform.position = place2.position;
            }
            if(randomNum == 2)
            {
                this.transform.position = place3.position;
            }
            if(randomNum == 3)
            {
                this.transform.position = place4.position;
            }
            if(randomNum == 4)
            {
                this.transform.position = place5.position;
            }
            if(randomNum == 5)
            {
                this.transform.position = place6.position;
            }
        }
   }
}
