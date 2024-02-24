using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public enum GameStates
    {
        Null,
        Playing,
        Failed,
        Passed
    }
    public GameStates gameState;

    [SerializeField] GameObject gameFailedUI;
    [SerializeField] GameObject gamePassedUI;
    [SerializeField] List<Transform> spawnPoint;
    [SerializeField] List<GameObject> enemyJets;

    [SerializeField] int maxEnemy;

    private List<GameObject> spawnedEnemyList = new List<GameObject>();
    private const string CURRENT_LEVEL = "currentLevel";
    private int currentLevel;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    private void Start()
    {
        if (!PlayerPrefs.HasKey(CURRENT_LEVEL))
            PlayerPrefs.SetInt(CURRENT_LEVEL, 1);

        currentLevel = PlayerPrefs.GetInt(CURRENT_LEVEL);

        SpawnEnemy();
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

    public void GameStart()
    {
        gameState = GameStates.Playing;
    }
    public void GameOver()
    {
        gameFailedUI.SetActive(true);
        gameState = GameStates.Failed;
    }
    public void Restart()
    {
        SceneManager.LoadScene("Main");
    }
    public void Passed()
    {
        gamePassedUI.SetActive(true);
        gameState = GameStates.Passed;

        currentLevel++;
        PlayerPrefs.SetInt(CURRENT_LEVEL, currentLevel);
    }

    private void SpawnEnemy()
    {
        for (int i = 0; i < currentLevel; i++)
        {
            if (spawnedEnemyList.Count < maxEnemy)
            {
                int randomEnemyIndex = UnityEngine.Random.Range(0, enemyJets.Count);
                int spawnPointIndex = UnityEngine.Random.Range(0, spawnPoint.Count);

                GameObject spawnedEnemy = Instantiate(enemyJets[randomEnemyIndex]);
                spawnedEnemy.transform.position = spawnPoint[spawnPointIndex].transform.position;
                spawnedEnemyList.Add(spawnedEnemy);
            }
        }

    }
}