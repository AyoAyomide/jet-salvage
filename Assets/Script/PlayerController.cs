using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Vector3 moveDir;
    [SerializeField] float moveSpeed;
    [SerializeField] float fallSpeed;
    [SerializeField] float rotateAmount;
    [SerializeField] Transform fTarget;
    [SerializeField] Transform jetModelGOB;
    [SerializeField] CinemachineVirtualCamera vCam;
    [SerializeField] Vector3 groundOffset = new Vector3(0, -0.83f, 0);
    private Rigidbody rb;

    private float horizontalInput;
    private float verticalInput;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        InputDirection();




    }

    private void FixedUpdate()
    {
        StickToGround();

        DutchCamera();

        float zPos = verticalInput * moveSpeed;
        float xPos = horizontalInput * moveSpeed;

        if (Mathf.Abs(verticalInput) > 0.1f || Mathf.Abs(horizontalInput) > 0.1f)
        {
            Vector3 force = new Vector3(xPos, 0, zPos);
            rb.AddForce(force, ForceMode.Acceleration);
        }

        //if (verticalInput != 0 || horizontalInput != 0)
        //  {
        //   rb.AddForce(xPos, 0, zPos, ForceMode.Impulse);
        // rb.AddForce(transform.forward * moveSpeed * verticalInput, ForceMode.Acceleration);
        //}

    }

    private void StickToGround()
    {
        Ray ray = new Ray(transform.position + groundOffset, Vector3.down);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1.0f)) // player is on a surface
        {
            jetModelGOB.up = hit.normal;
            Debug.Log("ray hit something");
        }
        else // player is floating
        {
            jetModelGOB.up = Vector3.up;
            rb.AddForce(Vector3.down * fallSpeed, ForceMode.Force);
            Debug.Log("ray not hitting");
        }
    }

    private void DutchCamera()
    {
        vCam.m_Lens.Dutch = horizontalInput * rotateAmount;
    }

    private void InputDirection()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        verticalInput = Input.GetAxis("Vertical");
    }

}
