using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.UI;

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
    [SerializeField] private GameObject target;

    private Animator anim;
    private float distance;
    private bool inRange;
    private bool cooling;
    private float intTimer;
    private GameObject currentTarget;
    private Enemy enemy;

    public float speed = 200f;
    public float nextWaypointDistance = 3f;

    public float keepDistance = 10f;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;


    void Start()
    {
        enemy = gameObject.GetComponent(typeof(Enemy)) as Enemy;
        coreTarget = GameObject.FindWithTag("Objective");
        currentTarget = GameObject.FindWithTag("Objective");
        target = currentTarget;
        intTimer = enemy.baseTimer;
        anim = GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody2D>();

        // target = GameObject.FindWithTag("Player"); // change to objective

        seeker = GetComponent<Seeker>();

        InvokeRepeating("UpdatePath", 3f, 2f);
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.transform.position, OnPathComplete);
        }
    }
    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
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
                string facing = "right";
                if (angle > 0 && angle < 90 || angle < 0 && angle > -90)
                {
                    hit = Physics2D.Raycast(rayCast.position, Vector2.right, rayCastLength, raycastMask);
                    RaycastDebugger(facing);
                }
                else
                {
                    facing = "left";
                    hit = Physics2D.Raycast(rayCast.position, Vector2.left, rayCastLength, raycastMask);
                    RaycastDebugger(facing);
                }
            }


            EnemyLogic();
            if (inRange == false)
            {
                anim.SetBool("canWalk", false);
                StopAttack();
            }
        }

        //Pathfinding Move
        if (!enemy.GetDied())
        {
            if (path == null)
            {
                return;
            }

            // Debug.Log(path.vectorPath.Count);

            if (currentWaypoint >= path.vectorPath.Count)
            {
                reachedEndOfPath = true;
                return;
            }
            else
            {
                reachedEndOfPath = false;
            }

            if ((path.vectorPath[currentWaypoint] - target.transform.position).magnitude <= keepDistance)
            {
                // Debug.Log("stop");
                return;
            }

            Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
            Vector2 force = direction * speed * Time.deltaTime;

            rb.AddForce(force);

            float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

            if (distance < nextWaypointDistance)
            {
                currentWaypoint++;
            }

            if (rb.velocity.x >= 0.01f)
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
            else if (rb.velocity.x <= -0.01f)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }

        }



    }

    public void TriggerCooling()
    {
        cooling = true;
    }

    void OnTriggerStay2D(Collider2D trig)
    {
        if (reachedEndOfPath && trig.CompareTag("Tower") && !cooling)
        {
            target = trig.gameObject;
            inRange = true;
        }
        else if (trig.CompareTag("Objective") && !cooling)
        {
            inRange = true;

        }
    }


    void EnemyLogic()
    {
        distance = Vector2.Distance(transform.position, target.transform.position);
        if (distance > attackDistance && !cooling)
        {
            //Move();
            StopAttack();
        }
        else if (distance <= attackDistance && !cooling)
        {
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
        if (target.tag == "Tower" && target.transform.parent.gameObject.GetComponent<Tower>().GetCurrentHealth() > 0)
        {
            BulletController.current.GetBullet(ItemPrefab, transform.position, target, enemy.GetDamage());
        }
        else if (target.tag == "Objective" && target.GetComponent<Objective>().GetCurrentHealth() > 0)
        {
            BulletController.current.GetBullet(ItemPrefab, transform.position, target, enemy.GetDamage());
        }
        else if (target.tag == "Enemy" && target.transform.parent.gameObject.GetComponent<Enemy>().GetCurrentHealth() > 0)
        {
            BulletController.current.GetBullet(ItemPrefab, transform.position, target, enemy.GetDamage());
        }
        // var obj = Instantiate(ItemPrefab, transform.position, Quaternion.identity);
        // obj.GetComponent<BulletBehavior>().SetTarget(target);
        // obj.GetComponent<BulletBehavior>().SetDamage(enemy.GetDamage());
        // BulletController.current.GetBullet(ItemPrefab,transform.position,target,enemy.GetDamage());
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
            foreach (Image img in gameObject.GetComponent<EnemyBuffUIManager>().uiUse)
                Destroy(img.gameObject);
            gameObject.GetComponent<EnemyBuffUIManager>().uiUse.Clear();
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

