using UnityEngine;

public class JetController : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float rotateSpeed;

    private Rigidbody rb;
    private float horizontalInput;
    private float verticalInput;
    private float yRot;
    public Vector3 moveDir;



    private void Start()
    {
        rb = GetComponent<Rigidbody>();



    }
    private void Update()
    {
        InputDirection();

        //Single shoot
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // camAnim.Play(camAnim.clip.name);
            // Instantiate(projectilePrefab, sourceOne.position, sourceOne.rotation);
            // Instantiate(projectilePrefab, sourceTwo.position, sourceTwo.rotation);
        }
        yRot = transform.eulerAngles.y;

    }

    private void FixedUpdate()
    {
        moveDir = transform.forward * verticalInput + transform.right * horizontalInput;

        if (Mathf.Abs(verticalInput) > 0.1f || Mathf.Abs(horizontalInput) > 0.1f)
        {
            rb.AddForce(moveDir.normalized * moveSpeed, ForceMode.Acceleration);
        }

        yRot += horizontalInput * rotateSpeed;

        transform.rotation = Quaternion.Euler(0, yRot, 0);
    }
    private void InputDirection()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        verticalInput = Input.GetAxis("Vertical");
    }
}
