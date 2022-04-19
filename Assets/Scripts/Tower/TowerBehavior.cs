using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    public float attackDistance;
    public GameObject ItemPrefab;
    private GameObject target;
    private float distance;
    private bool inRange;
    private bool cooling;
    private float intTimer;
    private GameObject currentTarget;
    private Tower tower;

    void Start()
    {
        tower = gameObject.GetComponent(typeof(Tower)) as Tower;
        intTimer = tower.baseTimer;



    }

    // Update is called once per frame
    void Update()
    {
        if (tower.GetCurrentHealth() <= 0 && !tower.GetDied())
        {
            tower.SetDied(true);
        }

        if (tower.GetDied())
        {
            Died();
        }
        TowerLogic();
        if (inRange == false)
        {
            StopAttack();
        }
    }

    void OnTriggerEnter2D(Collider2D trig)
    {
        if (target == null && !cooling)
        {
            target = trig;

        }
    }
    void TowerLogic()
    {
        float distance = Vector2.Distance()
        if (distance <= attackDistance && !cooling)
        {
            Attack();
        }
        else if (distance > attackDistance)
        {
            StopAttack();
        }

        if (cooling)
        {
            CoolDown();
        }
    }

}
