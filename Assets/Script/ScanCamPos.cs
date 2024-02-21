using System;
using UnityEngine;

public class ScanCamPos : MonoBehaviour
{
    [SerializeField] Transform vCam;

    private void FixedUpdate(){
        if(vCam.position.z > transform.position.z){
            Vector3 newPos = transform.position;
            newPos.z += 950 * 2;
            transform.position = newPos;
        }
    }
}