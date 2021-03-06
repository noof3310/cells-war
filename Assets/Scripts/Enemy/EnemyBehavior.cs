using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Pathfinding;

public class EnemyBehavior : MonoBehaviour
{
    public Transform rayCast;
    public LayerMask raycastMask;
    public float rayCastLength;
    public float attackDistance;
    public bool isBoss;
    // public Transform Objective;
    private GameObject coreTarget;
    private bool attackMode;
    private bool shouldAttack;
    private Rigidbody2D rb;
    private Vector2 movement;
    private RaycastHit2D hit;

    [SerializeField] private GameObject target;

    private Animator anim;
    private float distance;
    private bool inRange;
    private bool cooling;
    private float intTimer;
    // private GameObject currentTarget;
    private Enemy enemy;

    public float speed = 200f;
    public float nextWaypointDistance = 3f;

    public float keepDistance = 5f;

    private bool donotPathFind = false;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    [SerializeField] private List<Collider2D> colliders = new List<Collider2D>();
    public List<Collider2D> GetColliders() { return colliders; }

    void Start()
    {
        enemy = gameObject.GetComponent(typeof(Enemy)) as Enemy;
        coreTarget = GameObject.FindWithTag("Objective");
        // currentTarget = GameObject.FindWithTag("Objective");
        target = GameObject.FindWithTag("Objective");
        intTimer = enemy.baseTimer;
        anim = GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody2D>();
        // target = GameObject.FindWithTag("Player"); // change to objective

        seeker = GetComponent<Seeker>();

        InvokeRepeating("UpdatePath", 3f, 2f);
        
        donotPathFind = Random.Range(1,100) > 80;
        if(isBoss){
            donotPathFind = false;
        } 

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
                target = coreTarget;
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
            if (!enemy.GetDied())
                EnemyLogic();
            if (inRange == false)
            {
                // anim.SetBool("canWalk", false);
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
            if(!donotPathFind){
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
                // if (Random.Range(1,100) > 80)
                // {
                //     target = GameObject.FindWithTag("Tower");
                // }

                Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
                Vector2 force = direction * speed * Time.deltaTime;

                rb.AddForce(force);

                float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

                if (distance < nextWaypointDistance)
                {
                    currentWaypoint++;
                }
            }

            if (rb.velocity.x >= 0.01f)
            {
                if (transform.localScale.x < 0)
                    transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
            else if (rb.velocity.x <= -0.01f)
            {
                if (transform.localScale.x > 0)
                    transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
        }



    }

    public float GetDistanceWithCoreTarget()
    {

        float distance = Vector2.Distance(transform.position, coreTarget.transform.position);
        return distance;
    }

    public void TriggerCooling()
    {
        cooling = true;
        if (isBoss)
        {
            BossTakeDamage();
        }
        else
        {
            TakeDamage();
        }
    }

    public void TakeDamage()
    {
        if (target.tag == "Objective")
        {
            donotPathFind = false;
            target.GetComponent<Objective>().TakenDamage(enemy.GetDamage());
        }
        else if (target.tag == "Tower")
        {
            target.GetComponent<Tower>().TakenDamage(enemy.GetDamage());
            if(donotPathFind) {
                Move();
            }
        }
    }
    public void BossTakeDamage()
    {
        if (target.tag == "Objective")
        {
            donotPathFind = false;
            target.GetComponent<Objective>().TakenDamage(enemy.GetDamage());
        }
        else if (target.tag == "Tower")
        {

            foreach (Collider2D col in colliders)
            {
                if (col.name.Contains("Tower"))
                {
                    distance = Vector2.Distance(transform.position, col.transform.position);
                    if (distance <= attackDistance)
                    {
                        col.GetComponent<Tower>().TakenDamage(enemy.GetDamage());
                    }
                }

            }

            List<Collider2D> tempColliders = new List<Collider2D>();

            foreach (Collider2D col in colliders)
            {
                if (!col.name.Contains("Tower"))
                {
                    tempColliders.Add(col);
                }
            }

            colliders = tempColliders;

        }
    }

    private void OnTriggerExit2D(Collider2D trig)
    {
        if (reachedEndOfPath && trig.CompareTag("Tower") && trig.name.Contains("Tower") && !cooling && isBoss)
        {
            inRange = true;

            if (!colliders.Contains(trig)) { colliders.Remove(trig); }
        }
    }

    void OnTriggerStay2D(Collider2D trig)
    {

        if (reachedEndOfPath && trig.CompareTag("Tower") && !cooling && !isBoss && GetDistanceWithCoreTarget() > attackDistance)
        {
            target = trig.gameObject;
            inRange = true;
        }
        else if (trig.CompareTag("Objective") && !cooling && GetDistanceWithCoreTarget() <= attackDistance)
        {
            colliders.Clear();
            target = coreTarget;
            inRange = true;

        }
        else if (reachedEndOfPath && trig.CompareTag("Tower") && trig.name.Contains("Tower") && !cooling && isBoss && GetDistanceWithCoreTarget() > attackDistance)
        {
            inRange = true;

            if (!colliders.Contains(trig)) { colliders.Add(trig); }
            float minDistance = 1000f;
            Collider2D selectedCol = new Collider2D();
            foreach (Collider2D col in colliders)
            {
                distance = Vector2.Distance(transform.position, col.transform.position);
                if (distance <= minDistance)
                {
                    minDistance = distance;
                    selectedCol = col;
                }
            }
            target = selectedCol.gameObject;

        }
    }

     void Move()
    {
        anim.SetBool("canWalk", true);
        // if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Enemy_Type_1_attack"))
        // {
        Vector3 direction = target.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        reachedEndOfPath = true;
        // rb.rotation = angle;
        direction.Normalize();
        movement = direction;
        int speed2 = (int) enemy.moveSpeed / 2; 
        rb.MovePosition((Vector2)transform.position + (Vector2)(direction * speed2 * Time.deltaTime));
        rb.AddForce(direction);
    }

    void EnemyLogic()
    {
        distance = Vector2.Distance(transform.position, target.transform.position);
        if (distance > attackDistance && !cooling)
        {
            if(donotPathFind)
            {
                Move();
            }
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


    void Attack()
    {
        enemy.SetTimer(intTimer);
        attackMode = true;
        anim.SetBool("canWalk", false);
        anim.SetBool("attack", true);
    }

    void StopAttack()
    {
        // attackMode = false;
        anim.SetBool("attack", false);
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

    void Died()
    {
        anim.SetBool("canWalk", false);
        anim.SetBool("attack", false);
        anim.SetBool("died", true);
    }

    void TriggerDied()
    {
        if (enemy.GetDied() && !isBoss)
        {
            RemoveFromList(this.gameObject);  //I made it 28 just to give it leeway so the gameObject doesnt get destroyed before it invokes the method
            foreach (Image img in gameObject.GetComponent<EnemyBuffUIManager>().uiUse)
                Destroy(img.gameObject);
            gameObject.GetComponent<EnemyBuffUIManager>().uiUse.Clear();
            Destroy(gameObject.GetComponent<EnemyHealthBar>().uiUse.gameObject);
            Destroy(this.gameObject);

        }
        else if (enemy.GetDied() && isBoss)
        {
            RemoveFromList(this.gameObject);  //I made it 28 just to give it leeway so the gameObject doesnt get destroyed before it invokes the method

            foreach (Image img in gameObject.GetComponent<EnemyBuffUIManager>().uiUse)
                Destroy(img.gameObject);
            gameObject.GetComponent<EnemyBuffUIManager>().uiUse.Clear();
            Destroy(gameObject.GetComponent<EnemyHealthBar>().uiUse.gameObject);
            Destroy(gameObject.GetComponent<BossUIManager>().uiUse.gameObject);
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
        else if (attackDistance > distance)
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

