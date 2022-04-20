using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    public float attackDistance;
    public GameObject ItemPrefab;
    public string targetTagName = "Enemy";
    [SerializeField] private GameObject target;
    private bool inRange;
    private bool attackMode;
    private bool cooling = false;
    private float intTimer;
    private Tower tower;

    void Start()
    {
        tower = gameObject.GetComponent(typeof(Tower)) as Tower;
        intTimer = tower.baseTimer;
        attackMode = false;
        target = null;
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
        if (target != null)
        {
            TowerLogic();

        }

        if (cooling)
        {
            CoolDown();
        }
        if (inRange == false)
        {
            StopAttack();
        }
    }

    // void OnTriggerEnter2D(Collider2D trig)
    // {
    //     if (trig.CompareTag(targetTagName) && !cooling)
    //     {
    //         target = trig.gameObject;
    //     }
    // }
    void OnTriggerStay2D(Collider2D trig)
    {
        if (trig.CompareTag(targetTagName) && !cooling)
        {
            target = trig.gameObject;
        }
    }
    void TowerLogic()
    {
        float distance = Vector2.Distance((Vector2)target.transform.position, (Vector2)transform.position);
        if (distance <= attackDistance && !cooling)
        {
            Attack();
        }
        else if (distance > attackDistance)
        {
            StopAttack();
        }

    }

    void Attack()
    {
        Shoot();
        Debug.Log("Tower Shoot!!");
        attackMode = true;
        cooling = true;
        tower.SetTimer(intTimer);

    }

    void Shoot()
    {
        // Vector3 randomPos = Random.insideUnitCircle * Radius;
        var obj = Instantiate(ItemPrefab, transform.position, Quaternion.identity);
        obj.GetComponent<BulletBehavior>().SetTarget(target);
        obj.GetComponent<BulletBehavior>().SetDamage(tower.GetDamage());
    }

    void StopAttack()
    {

    }

    void CoolDown()
    {
        tower.SetTimer(tower.GetTimer() - Time.deltaTime);
        if (tower.GetTimer() <= 0 && cooling && attackMode)
        {
            cooling = false;
            tower.SetTimer(intTimer);
            attackMode = false;

        }
    }

    void Died()
    {
        RemoveFromList(this.gameObject);  //I made it 28 just to give it leeway so the gameObject doesnt get destroyed before it invokes the method
        Destroy(this.gameObject);
        // foreach (Image img in gameObject.GetComponent<EnemyBuffUIManager>().uiUse)
        //     Destroy(img.gameObject);
    }

    void RemoveFromList(GameObject gameObject)
    {
        SpawnerManager.enemyList.Remove(gameObject);
    }




}
