using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float forwardSpeed;
    [SerializeField] float turnSpeed;

    [SerializeField] Rigidbody rb;

    private Vector3 jetDirection;
    private void Start()
    {

    }

    private void Update()
    {

        MoveForward();

        VerticalControl();
    }

    void FixedUpdate()
    {
        // Apply force to the rigid body in FixedUpdate
        rb.AddForce(jetDirection);
    }

    private void MoveForward()
    {
        if (jetDirection.z < 30)
            jetDirection.z += forwardSpeed * Time.deltaTime;
    }

    private void VerticalControl()
    {
        // Get vertical input
        float verticalInput = Input.GetAxis("Horizontal");

        jetDirection.x = turnSpeed * verticalInput;
    }
}
