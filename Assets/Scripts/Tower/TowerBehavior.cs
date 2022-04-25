using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    public TowerBuff buffEffect;
    public float buffRange = 2f;
    public float attackDistance;
    public GameObject ItemPrefab;
    public string targetTagName = "Enemy";
    [SerializeField] private GameObject target;
    private BuildManager buildManager;
    private bool inRange;
    private bool attackMode;
    private bool cooling = false;
    private float intTimer;
    private Tower tower;
    public List<Tower> towerInRange = new List<Tower>();

    void Start()
    {
        buildManager = GameObject.Find("BuildManager").GetComponent<BuildManager>();
        tower = gameObject.GetComponent(typeof(Tower)) as Tower;
        intTimer = tower.baseTimer;
        attackMode = false;
        target = null;

        if (tower.isBuffTower)
        {
            BuffTowerInRange();
        }
    }

    // Update is called once per frame
    void Update()
    {
        intTimer = tower.GetBuffedTimer();

        if (tower.isBuffTower)
        {
            BuffTowerInRange();
        }

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
        attackMode = true;
        cooling = true;
        tower.SetTimer(intTimer);

    }

    void Shoot()
    {
        // Vector3 randomPos = Random.insideUnitCircle * Radius;
        if (target.tag == "Tower" && target.transform.parent.gameObject.GetComponent<Tower>().GetCurrentHealth() > 0)
        {
            BulletController.current.GetBullet(ItemPrefab,transform.position,target,tower.GetDamage());
        }
        else if (target.tag == "Objective" && target.GetComponent<Objective>().GetCurrentHealth() > 0)
        {
            BulletController.current.GetBullet(ItemPrefab,transform.position,target,tower.GetDamage());
        }
        else if (target.tag == "Enemy" && target.transform.parent.gameObject.GetComponent<Enemy>().GetCurrentHealth() > 0)
        {
            BulletController.current.GetBullet(ItemPrefab,transform.position,target,tower.GetDamage());
        }


        // var obj = Instantiate(ItemPrefab, transform.position, Quaternion.identity);
        // obj.GetComponent<BulletBehavior>().SetTarget(target);
        // obj.GetComponent<BulletBehavior>().SetDamage(tower.GetDamage());
        // BulletController.current.GetBullet(ItemPrefab,transform.position,target,tower.GetDamage());
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

    public void Died()
    {
        buildManager.updatePath(Vector3Int.FloorToInt(transform.position));
        if (tower.isBuffTower) CancleBuff();
        RemoveFromList(this.gameObject);  //I made it 28 just to give it leeway so the gameObject doesnt get destroyed before it invokes the method
        Destroy(this.gameObject);
        // foreach (Image img in gameObject.GetComponent<EnemyBuffUIManager>().uiUse)
        //     Destroy(img.gameObject);
    }

    void RemoveFromList(GameObject gameObject)
    {
        SpawnerManager.enemyList.Remove(gameObject);
    }

    void BuffTowerInRange()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, buffRange);
        foreach (var collider in colliders)
        {
            var go = collider.gameObject;
            if (!(go == gameObject) && go.tag == "Tower" && !collider.isTrigger)
            {
                Tower tower = go.GetComponent<Tower>();
                if (!towerInRange.Contains(tower))
                {
                    towerInRange.Add(tower);
                    tower.GetBuffed(buffEffect);
                    Debug.Log("Buff " + tower + " " + buffEffect);
                }
            }
        }
    }

    void CancleBuff()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, buffRange);
        foreach (var collider in colliders)
        {
            var go = collider.gameObject;
            if (!(go == gameObject) && go.tag == "Tower" && !collider.isTrigger)
            {
                Tower tower = go.GetComponent<Tower>();
                if (towerInRange.Contains(tower))
                {
                    tower.CancleBuff(buffEffect);
                    Debug.Log("Cancle Buff " + tower + " " + buffEffect);
                }
            }
        }
    }
}