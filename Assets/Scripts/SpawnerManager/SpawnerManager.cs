using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    // public static ObjectPooling currentResources;
    public GameObject poolObject;
    public int resourcePoolAmount;
    public bool willGrow = false;

    public List<GameObject> objectsToSpawn = new List<GameObject>();
    public List<GameObject> bossToSpawn = new List<GameObject>();
    public List<GameObject> resourceToSpawn = new List<GameObject>();
    public static List<GameObject> enemyList = new List<GameObject>();
    public static List<GameObject> whiteBloodCellList = new List<GameObject>();
    public bool isEnemySpawner;
    public bool isResourceSpawner;
    public bool shouldSpawnEnemy;
    public bool shouldSpawnWhiteBloodCell;
    public bool shouldSpawnBoss;

    public int amount;
    public int totalAmount;
    public float Radius = 50;

    public float initialTimer;
    private float timer;
    void Start()
    {
        //shouldSpawnEnemy = false;
        shouldSpawnWhiteBloodCell = false;

        ResetAll();

         if (resourceToSpawn.Count > 0 && isResourceSpawner) 
        {
            int index = Random.Range(0, objectsToSpawn.Count);
            Vector3 randomPos = Random.insideUnitCircle * Radius;
            for (int i = 0; i < resourcePoolAmount; i++) 
            {    
                GameObject obj = Instantiate(resourceToSpawn[index], randomPos ,Quaternion.identity);
                obj.SetActive(false);
                whiteBloodCellList.Add(obj);
            
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (shouldSpawnBoss && GameManager.State == GameState.FightState)
        {
            SpawnObjectAtRandom(bossToSpawn);
            shouldSpawnBoss = false;
        }

        if (isResourceSpawner && GameManager.State == GameState.RestState && whiteBloodCellList.Count > 0)
        {
            foreach (var objects in whiteBloodCellList)
            {
                Destroy(objects);
            }
            whiteBloodCellList.Clear();
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
                if(SpawnResourceAtRandom())
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
    public GameObject GetPoolObject() 
    {
        for (int i = 0; i < whiteBloodCellList.Count; i++) 
        {
            if (!whiteBloodCellList[i].activeInHierarchy) 
            {
                return whiteBloodCellList[i];
            }
        }
        if (willGrow || whiteBloodCellList.Count < resourcePoolAmount) 
        {
            int index2 = Random.Range(0, resourceToSpawn.Count);

            Vector3 randomPos = Random.insideUnitCircle * Radius;

            GameObject obj = Instantiate(resourceToSpawn[index2], randomPos, Quaternion.identity);
            whiteBloodCellList.Add(obj);

            return obj;
        }
        return null;
    }

    public void ResetAll()
    {
        amount = totalAmount;
        timer = initialTimer;
    }
    
    bool SpawnResourceAtRandom()
    {
        // if (resourceToSpawn.Count > 0)
        // {
        // int index = Random.Range(0, resourceToSpawn.Count);

        Vector3 randomPos = Random.insideUnitCircle * Radius;

        //     whiteBloodCellList.Add(Instantiate(resourceToSpawn[index], randomPos, Quaternion.identity));
        // }
        GameObject obj = GetPoolObject();
        if (obj==null) return false;
        obj.transform.position = randomPos;
        obj.SetActive(true);
        return true;

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
