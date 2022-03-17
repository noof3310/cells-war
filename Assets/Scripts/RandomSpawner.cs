using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public GameObject ItemPrefab;
    public bool shouldSpawn = true;
    public float Radius = 50;

    public int amount = 100;
    // public float updateInterval = 5f;
//     void Start()
//  {
//      InvokeRepeating("SpawnObjectAtRandom",updateInterval,updateInterval);
//     }

    async void Update()
    {
        if(amount > 0){
            SpawnObjectAtRandom();
            amount -=1;
        }
    }

    void SpawnObjectAtRandom() {
        Vector3 randomPos = Random.insideUnitCircle * Radius;

        Instantiate(ItemPrefab,randomPos,Quaternion.identity);
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.green;

        Gizmos.DrawWireSphere(this.transform.position, Radius);
    }
}
