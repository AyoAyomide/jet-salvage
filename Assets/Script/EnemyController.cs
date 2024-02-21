using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] NavMeshAgent navMeshAgent;
    [SerializeField] Transform player;
    
    private void FixedUpdate() {
        navMeshAgent.SetDestination(player.position);
    }
}