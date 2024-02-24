using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] NavMeshAgent navMeshAgent;
    [SerializeField] Transform objectToStick;
    [SerializeField] Vector3 groundOffset = new Vector3(0, -0.6f, 0);

    private Transform player;

    private void Start()
    {
        GameObject currentPlayer = GameObject.FindGameObjectWithTag("Player");
        if (currentPlayer != null)
            player = currentPlayer.transform;
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.gameState == GameManager.GameStates.Playing)
            navMeshAgent.SetDestination(player.position);
    }

    private void Update()
    {
        StickToGround(objectToStick, 10f);
    }

    private void StickToGround(Transform objectToStick, float alignSpeed, Rigidbody objectToStickRB = null)
    {
        Ray ray = new Ray(transform.position + groundOffset, Vector3.down);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1.0f)) // player is on a surface
        {
            objectToStick.up = Vector3.Lerp(objectToStick.up, hit.normal, alignSpeed * Time.deltaTime);

        }
        else
        {
            objectToStick.localRotation = Quaternion.identity;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.GameOver();
        }
    }
}