using System.Collections.Generic;
using UnityEngine;
public class PassGate : MonoBehaviour
{
    [SerializeField] List<Transform> gateSpawnPoint;

    private void Start()
    {
        int spawnPos = UnityEngine.Random.Range(0, gateSpawnPoint.Count);
        Vector3 spawnPoint = gateSpawnPoint[spawnPos].position;
        spawnPoint.y = -3.5f;
        transform.position = spawnPoint;
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
            GameManager.Instance.Passed();
    }

}