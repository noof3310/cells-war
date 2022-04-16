using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public GameObject ItemPrefab;
    public bool shouldSpawn = false;
    public float Radius = 50;

    public int amount = 100;
    public float initialTimer;
    private float timer;
    // public float updateInterval = 5f;
    //     void Start()
    //  {
    //      InvokeRepeating("SpawnObjectAtRandom",updateInterval,updateInterval);
    //     }

    void Start()
    {
        timer = initialTimer;
    }
    void Update()
    {
        if (shouldSpawn)
        {
            timer -= Time.deltaTime;
            if (amount > 0 && timer <= 0)
            {
                timer = initialTimer;
                SpawnObjectAtRandom();
                amount -= 1;
            }
        }

    }

    void SpawnObjectAtRandom()
    {
        Vector3 randomPos = (Vector2)transform.position;
        Debug.Log(ItemPrefab.name);
        if (ItemPrefab.name != "whitebloodcell")
        {
            GameManager.UpdateGameState(GameState.FightState);
            SpawnerManager.enemyList.Add(Instantiate(ItemPrefab, randomPos, Quaternion.identity));
        }
        else
            SpawnerManager.whiteBloodCellList.Add(Instantiate(ItemPrefab, randomPos, Quaternion.identity));

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawWireSphere(this.transform.position, Radius);
    }
}
