using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangebehavior : MonoBehaviour
{
    public Transform rayCast;
    public LayerMask raycastMask;
    public float rayCastLength;
    public float attackDistance;
    // public Transform player;
    public GameObject ItemPrefab;
    private GameObject coreTarget;

    private bool attackMode;
    private Rigidbody2D rb;
    private Vector2 movement;
    private RaycastHit2D hit;
    private GameObject target;
    private Animator anim;
    private float distance;
    private bool inRange;
    private bool cooling;
    private float intTimer;
    private GameObject currentTarget;
    private Enemy enemy;


    void Start()
    {
        enemy = gameObject.GetComponent(typeof(Enemy)) as Enemy;
        coreTarget = GameObject.FindWithTag("Objective");
        currentTarget = GameObject.FindWithTag("Objective");
        target = currentTarget;
        intTimer = enemy.baseTimer;
        anim = GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if (enemy.GetCurrentHealth() <= 0 && !enemy.GetDied())
        {
            enemy.SetDied(true);
        }

        if (enemy.GetDied())
        {
            Died();
        }
        else
        {
            if (target == null)
            {
                target = currentTarget;
            }
            if (inRange)
            {
                float angle = getAngle();
                string direction = "right";
                if (angle > 0 && angle < 90 || angle < 0 && angle > -90)
                {
                    hit = Physics2D.Raycast(rayCast.position, Vector2.right, rayCastLength, raycastMask);
                    RaycastDebugger(direction);
                }
                else
                {
                    direction = "left";
                    hit = Physics2D.Raycast(rayCast.position, Vector2.left, rayCastLength, raycastMask);
                    RaycastDebugger(direction);
                }
            }

            EnemyLogic();
            if (inRange == false)
            {
                anim.SetBool("canWalk", false);
                StopAttack();
            }
        }



    }

    public void TriggerCooling()
    {
        cooling = true;
    }

    void OnTriggerEnter2D(Collider2D trig)
    {
        if (trig.gameObject.name == target.name && !cooling)
        {
            // target = trig.gameObject;
            inRange = true;
        }
    }

    void EnemyLogic()
    {
        distance = Vector2.Distance(transform.position, target.transform.position);
        if (distance > attackDistance && !cooling)
        {
            Move();
            StopAttack();
        }
        else if (distance <= attackDistance && !cooling)
        {
            Debug.Log("Attack!!");
            Attack();
        }

        if (cooling)
        {
            CoolDown();
            anim.SetBool("attack", false);
        }
    }

    void Move()
    {
        anim.SetBool("canWalk", true);
        // if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Enemy_Type_1_attack"))
        // {
        Vector3 direction = target.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        // rb.rotation = angle;
        direction.Normalize();
        movement = direction;
        rb.MovePosition((Vector2)transform.position + (Vector2)(direction * enemy.moveSpeed * Time.deltaTime));
        // if (angle > 0 && angle < 90 || angle < 0 && angle > -90)
        // {
        //     rb.rotation = 180;

        // }
        // else
        // {
        //     rb.rotation = 0;

        // }
        // }
    }

    void Attack()
    {
        enemy.SetTimer(intTimer);
        attackMode = true;
        anim.SetBool("canWalk", false);
        Shoot();
        cooling = true;
        // anim.SetBool("attack", true);
    }

    void StopAttack()
    {
        // attackMode = false;
        // anim.SetBool("attack", false);
    }

    void CoolDown()
    {
        enemy.SetTimer(enemy.GetTimer() - Time.deltaTime);

        if (enemy.GetTimer() <= 0 && cooling && attackMode)
        {
            cooling = false;
            enemy.SetTimer(intTimer);
            attackMode = false;

        }
    }

    void Shoot()
    {
        // Vector3 randomPos = Random.insideUnitCircle * Radius;
        var obj = Instantiate(ItemPrefab, transform.position, Quaternion.identity);
        obj.GetComponent<BulletBehavior>().SetTarget(target);
        obj.GetComponent<BulletBehavior>().SetDamage(enemy.GetDamage());
    }

    void Died()
    {
        anim.SetBool("canWalk", false);
        anim.SetBool("attack", false);
        anim.SetBool("died", true);
    }



    void TriggerDied()
    {
        if (enemy.GetDied())
        {
            RemoveFromList(this.gameObject);  //I made it 28 just to give it leeway so the gameObject doesnt get destroyed before it invokes the method
            Destroy(this.gameObject);
        }
    }

    void RemoveFromList(GameObject gameObject)
    {
        SpawnerManager.enemyList.Remove(gameObject);
    }




    void RaycastDebugger(string direction)
    {
        if (distance > attackDistance)
        {
            if (direction == "right")
            {
                Debug.DrawRay(rayCast.position, Vector2.right * rayCastLength, Color.red);

            }
            else
            {
                Debug.DrawRay(rayCast.position, Vector2.left * rayCastLength, Color.red);

            }
        }
        else if (attackDistance >= distance)
        {
            if (direction == "right")
            {
                Debug.DrawRay(rayCast.position, Vector2.right * rayCastLength, Color.green);

            }
            else
            {
                Debug.DrawRay(rayCast.position, Vector2.left * rayCastLength, Color.green);
            }
        }
    }

    float getAngle()
    {
        Vector3 direction = target.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        return angle;
    }
}

