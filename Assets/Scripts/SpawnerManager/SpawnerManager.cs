using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    public static List<GameObject> enemyList = new List<GameObject>();
    public static List<GameObject> whiteBloodCellList = new List<GameObject>();
    public RandomSpawner EnemyType1Spawner;
    public RandomSpawner EnemyType2Spawner;
    public RandomSpawner WhiteBloodCellSpawner;
    public int enemyAmount;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(enemyList.Count);
        Debug.Log(GameManager.State);

        if (enemyList.Count == 0 && GameManager.State == GameState.FightState)
        {
            GameManager.UpdateGameState(GameState.RushState);
            SetEnemyType1Amount(10);
            SetEnemyType2Amount(10);
        }

        switch (GameManager.State)
        {
            case GameState.SpawnState:
                EnemyType1Spawner.shouldSpawn = true;
                EnemyType2Spawner.shouldSpawn = true;
                WhiteBloodCellSpawner.shouldSpawn = false;
                break;
            case GameState.FightState:
                EnemyType1Spawner.shouldSpawn = true;
                EnemyType2Spawner.shouldSpawn = true;
                WhiteBloodCellSpawner.shouldSpawn = false;
                break;
            case GameState.RestState:
                EnemyType1Spawner.shouldSpawn = false;
                EnemyType2Spawner.shouldSpawn = false;
                WhiteBloodCellSpawner.shouldSpawn = false;
                break;
            case GameState.RushState:
                EnemyType1Spawner.shouldSpawn = false;
                EnemyType2Spawner.shouldSpawn = false;
                WhiteBloodCellSpawner.shouldSpawn = true;
                break;
            default:
                EnemyType1Spawner.shouldSpawn = false;
                EnemyType2Spawner.shouldSpawn = false;
                WhiteBloodCellSpawner.shouldSpawn = false;
                break;

        }


    }

    public void SetEnemyType1Amount(int newAmount)
    {
        EnemyType1Spawner.amount = newAmount;
    }
    public void SetEnemyType2Amount(int newAmount)
    {
        EnemyType2Spawner.amount = newAmount;
    }
    public void SetWhiteBloodCellAmount(int newAmount)
    {
        WhiteBloodCellSpawner.amount = newAmount;
    }

}
