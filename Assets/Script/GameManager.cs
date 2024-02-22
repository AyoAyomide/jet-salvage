using UnityEngine;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void StickToGround(Transform objectToStick, float alignSpeed, Vector3 groundOffset, Rigidbody objectToStickRB = null, float fallSpeed = 0)
    {
        Ray ray = new Ray(transform.position + groundOffset, Vector3.down);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1.0f)) // player is on a surface
        {
            objectToStick.up = Vector3.Lerp(objectToStick.up, hit.normal, alignSpeed * Time.deltaTime);

            if (hit.collider.gameObject.name == "Ground")
            {
                objectToStick.localEulerAngles = Vector3.Lerp(objectToStick.up, Vector3.zero, alignSpeed * Time.deltaTime);
            }
        }
        else
        {
            if (objectToStickRB)
            {
                objectToStickRB.AddForce(Vector3.down * fallSpeed, ForceMode.Force);

            }
        }
    }

}