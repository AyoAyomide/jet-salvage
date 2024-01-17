using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float acceleration;
    [SerializeField] float turnSpeed;
    [SerializeField] Transform jetModelGOB;
    [SerializeField] Vector3 groundOffset;

    private Rigidbody rb;

    private float curYRot;

    private float horizontalInput;
    private float verticalInput;

    private bool IS_ACCELERATE;
    private bool IS_TURN;

    private Vector3 startModelOffset;

    private float turnRate;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        startModelOffset = jetModelGOB.transform.localPosition;
    }

    private void Update()
    {
        HorizontalControl();
        VerticalControl();

        turnRate = Vector3.Dot(rb.velocity.normalized, jetModelGOB.forward);
        turnRate = Mathf.Abs(turnRate);

        curYRot += horizontalInput * turnSpeed * turnRate * Time.deltaTime;
        jetModelGOB.position = transform.position + startModelOffset;

        StickToGround();


    }

    private void FixedUpdate()
    {

        if (IS_ACCELERATE)
        {
            rb.AddForce(jetModelGOB.forward * acceleration * verticalInput, ForceMode.Acceleration);
        }

    }

    private void StickToGround()
    {
        Ray ray = new Ray(transform.position + groundOffset, Vector3.down);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1.0f))
        {
            jetModelGOB.up = hit.normal;
        }
        else
        {
            jetModelGOB.up = Vector3.up;
        }

        // Interpolate between the current rotation and the target rotation
         Quaternion targetRotation = Quaternion.Euler(0, curYRot, 0);
         Quaternion upRotation = Quaternion.Lerp(jetModelGOB.rotation, targetRotation, turnSpeed * Time.deltaTime);

        // Extract the Y rotation from the interpolated rotation and apply it to jetModelGOB
        jetModelGOB.Rotate(new Vector3(0, upRotation.eulerAngles.y, 0), Space.Self);

        //jetModelGOB.Rotate(new Vector3(0, curYRot, 0), Space.Self);
    }

    private void HorizontalControl()
    {
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
