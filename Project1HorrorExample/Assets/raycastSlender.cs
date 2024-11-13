using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class raycastSlender : MonoBehaviour
{
    public GameObject playerObj;
    public Transform slenderTransform;
    public bool detected;
    public Vector3 offset;

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = slenderTransform.forward;
        RaycastHit hit;
        if(Physics.Raycast(slenderTransform.position + offset, direction, out hit, Mathf.Infinity))
        {
            Debug.DrawLine(slenderTransform.position, hit.point, Color.red, Mathf.Infinity);
            if(hit.collider.gameObject == playerObj)
            {
                detected = true;
                
            }
            else 
            {
                detected = false;
                
            }
        }
    }
}
