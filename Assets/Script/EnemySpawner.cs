using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float moveSpeed = 6;
    [SerializeField] Vector3 distance;
    [SerializeField] Vector3 distanceBetweenEnemy;

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position + distanceBetweenEnemy + distance, moveSpeed * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        transform.LookAt(target);
    }

}