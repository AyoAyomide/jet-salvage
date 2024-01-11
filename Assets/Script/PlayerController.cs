using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float acceleration;
    [SerializeField] float turnSpeed;
    [SerializeField] Transform jetModelGOB;

    private Rigidbody rb;

    private float curYRot;

    private float horizontalInput;
    private float verticalInput;

    private bool IS_ACCELERATE;
    private bool IS_TURN;

    private Vector3 startModelOffset;

    private void Start() {

        rb = gameObject.GetComponent<Rigidbody>();
        startModelOffset = jetModelGOB.transform.localPosition;
    }
    
    private void Update() {

        curYRot += horizontalInput * turnSpeed * Time.deltaTime;
        //jetModelGOB.position = transform.position + startModelOffset;
        jetModelGOB.eulerAngles = new Vector3(0,curYRot,0);

        HorizontalControl();
        VerticalControl();
    }

    private void FixedUpdate() {

        if(IS_ACCELERATE){
            rb.AddForce(jetModelGOB.forward * acceleration * verticalInput, ForceMode.Acceleration);
        }
        
    }

    private void HorizontalControl(){
        horizontalInput = Input.GetAxisRaw("Horizontal");
        IS_TURN = horizontalInput != 0 ? true : false;

        Debug.Log("move left");
    }
    private void VerticalControl()
    {
        // Get vertical input
        verticalInput = Input.GetAxisRaw("Vertical");
        IS_ACCELERATE = verticalInput != 0 ? true : false;

        Debug.Log("move forward");
    }
}
