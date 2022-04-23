using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehavior : MonoBehaviour
{
    private EnemyBehavior enemyBehavior;
    // Start is called before the first frame update
    void Start()
    {
        enemyBehavior = gameObject.GetComponent(typeof(EnemyBehavior)) as EnemyBehavior;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
