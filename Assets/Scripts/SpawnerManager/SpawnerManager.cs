using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    public List<GameObject> objectsToSpawn = new List<GameObject>();
    public List<GameObject> bossToSpawn = new List<GameObject>();
    public List<GameObject> resourceToSpawn = new List<GameObject>();
    public static List<GameObject> enemyList = new List<GameObject>();
    public static List<GameObject> whiteBloodCellList = new List<GameObject>();
    public bool isEnemySpawner;
    public bool isResourceSpawner;
    public bool shouldSpawnEnemy;
    public bool shouldSpawnWhiteBloodCell;
    public int amount;
    public int totalAmount;
    public float Radius = 50;

    public float initialTimer;
    private float timer;
    private bool shouldSpawnBoss;
    void Start()
    {
        //shouldSpawnEnemy = false;
        shouldSpawnWhiteBloodCell = false;
        shouldSpawnBoss = false;
        ResetAll();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Game level: " + GameManager.level);

        if (shouldSpawnBoss && GameManager.State == GameState.FightState)
        {
            Debug.Log("Boss spawn");
            SpawnObjectAtRandom(bossToSpawn);
            shouldSpawnBoss = false;
        }

        if (isResourceSpawner && GameManager.State == GameState.SpawnState)
        {
            foreach (var objects in whiteBloodCellList)
            {
                Destroy(objects);
            }
            amount = totalAmount;
            GameManager.UpdateGameState(GameState.FightState);

        }

        if (isEnemySpawner && shouldSpawnEnemy && GameManager.State == GameState.FightState)
        {
            timer -= Time.deltaTime;
            if (amount > 0 && timer <= 0)
            {
                timer = initialTimer;
                SpawnObjectAtRandom(objectsToSpawn);
                amount -= 1;
            }
        }
        if (isResourceSpawner && shouldSpawnWhiteBloodCell && GameManager.State == GameState.RushState)
        {
            timer -= Time.deltaTime;
            if (amount > 0 && timer <= 0)
            {
                timer = initialTimer;
                SpawnResourceAtRandom();
                amount -= 1;
            }
        }

        if (isEnemySpawner && enemyList.Count == 0 && amount == 0 && GameManager.State == GameState.FightState)
        {
            GameManager.UpdateGameState(GameState.RushState);
            amount = totalAmount;
            GameManager.level += 1;
            if (GameManager.level % GameManager.levelBossSpawn == 0)
            {
                shouldSpawnBoss = true;
            }
        }


        switch (GameManager.State)
        {
            case GameState.SpawnState:
                shouldSpawnEnemy = true;
                shouldSpawnWhiteBloodCell = false;
                break;
            case GameState.FightState:
                shouldSpawnEnemy = true;
                shouldSpawnWhiteBloodCell = false;
                break;
            case GameState.RestState:
                shouldSpawnEnemy = false;
                shouldSpawnWhiteBloodCell = false;
                break;
            case GameState.RushState:
                shouldSpawnEnemy = false;
                shouldSpawnWhiteBloodCell = true;
                break;
            default:
                shouldSpawnEnemy = false;
                shouldSpawnWhiteBloodCell = false;
                break;

        }


    }

    public void ResetAll()
    {
        amount = totalAmount;
        timer = initialTimer;
    }

    void SpawnResourceAtRandom()
    {
        if (resourceToSpawn.Count > 0)
        {
            int index = Random.Range(0, resourceToSpawn.Count);

            Vector3 randomPos = Random.insideUnitCircle * Radius;

            whiteBloodCellList.Add(Instantiate(resourceToSpawn[index], randomPos, Quaternion.identity));
        }
    }

    void SpawnObjectAtRandom(List<GameObject> objectsToSpawn)
    {
        if (objectsToSpawn.Count > 0)
        {
            int index = Random.Range(0, objectsToSpawn.Count);

            Vector3 randomPos = new Vector2(Random.Range(30.0f, 40.0f), Random.Range(20.0f, 30.0f));
            if (Random.Range(0, 2) == 1) {
                randomPos.x = -randomPos.x;
            }
            if (Random.Range(0, 2) == 1) {
                randomPos.y = -randomPos.y;
            }

            enemyList.Add(Instantiate(objectsToSpawn[index], randomPos, Quaternion.identity));
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawWireSphere(this.transform.position, Radius);
    }

}
