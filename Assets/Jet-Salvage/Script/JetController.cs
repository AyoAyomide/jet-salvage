using UnityEngine;

public class JetController : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float rotateSpeed;
    [SerializeField] float fallSpeed;
    [SerializeField] float alignSpeed;
    [SerializeField] Transform jetModelGOB;
    [SerializeField] GameObject playerHolder;
    [SerializeField] Vector3 groundOffset = new Vector3(0, -0.6f, 0);

    private Rigidbody rb;
    private float horizontalInput;
    private float verticalInput;
    private float yRot;
    private Vector3 moveDir;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerHolder.transform.position = new Vector3(0, -0.45f, 0);

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

        StickToGround(jetModelGOB, alignSpeed, rb);

        if (transform.position.y < -5)
            GameManager.Instance.GameOver();
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
        if (GameManager.Instance.gameState == GameManager.GameStates.Playing)
        {
            horizontalInput = Input.GetAxis("Horizontal");

            verticalInput = Input.GetAxis("Vertical");
        }
        else
        {
            horizontalInput = verticalInput = 0f;
        }

    }
    private void StickToGround(Transform objectToStick, float alignSpeed, Rigidbody objectToStickRB = null)
    {
        Ray ray = new Ray(transform.position + groundOffset, Vector3.down);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1.0f)) // player is on a surface
        {
            if (hit.collider.gameObject.layer == 6)
            {
                objectToStick.up = Vector3.Lerp(objectToStick.up, hit.normal, alignSpeed * Time.deltaTime);
            }
        }
        else
        {
            if (objectToStick.localRotation.y != 0)
                objectToStick.localRotation = Quaternion.identity;

            if (objectToStickRB)
            {
                rb.AddForce(Vector3.down * fallSpeed, ForceMode.Force);
            }
        }
    }

}