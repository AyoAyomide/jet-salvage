using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Vector3 moveDir;
    [SerializeField] float moveSpeed;
    [SerializeField] float fallSpeed;
    [SerializeField] Vector3 testRot;
    [SerializeField] float rotateSpeed;
    [SerializeField] float rotateAmount;
    [SerializeField] Transform fTarget;
    private Vector3 groundOffset = new Vector3(0, -0.83f, 0);
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

        StickToGround();

        RotatePlayer();

    }

    private void FixedUpdate()
    {
        float zPos = verticalInput * moveSpeed;
        float xPos = horizontalInput * moveSpeed;





        if (verticalInput != 0 || horizontalInput != 0)
        {
              rb.AddForce(xPos, 0, zPos, ForceMode.Impulse);

        }

    }

    private void StickToGround()
    {
        Ray ray = new Ray(transform.position + groundOffset, Vector3.down);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1.0f)) // player is on a surface
        {
            Debug.Log("ray hit something");
        }
        else // player is floating
        {
            rb.AddForce(Vector3.down * fallSpeed, ForceMode.Force);
            Debug.Log("ray not hitting");
        }
    }

    private void RotatePlayer()
    {
        //  if (horizontalInput != 0)
        //  {
        float yDeg = Mathf.MoveTowardsAngle(transform.rotation.y, horizontalInput * rotateAmount, rotateSpeed);

       // transform.rotation = Quaternion.Euler(0, yDeg, 0);
        //   }
    }

    private void InputDirection()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        verticalInput = Input.GetAxis("Vertical");
    }

}
