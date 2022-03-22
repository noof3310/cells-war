using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangebehavior : MonoBehaviour
{
    public GameObject ItemPrefab;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }

    }
    void Shoot()
    {
        // Vector3 randomPos = Random.insideUnitCircle * Radius;
        Instantiate(ItemPrefab, transform.position, Quaternion.identity);
    }

}
