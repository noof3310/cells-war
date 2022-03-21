using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_behavior : MonoBehaviour
{
    public Transform rayCast;
    public LayerMask raycastMask;
    public float rayCastLength;
    public float attackDistance;
    public float moveSpeed;
    public float timer;
    // public Transform player;
    public static bool attackMode;

    private Rigidbody2D rb;
    private Vector2 movement;
    private RaycastHit2D hit;
    private GameObject target;
    private Animator anim;
    private float distance;
    private bool inRange;
    private bool cooling;
    private bool died;
    private float intTimer;

    void Start()
    {
        target = Enemy.target;
        died = false;
        intTimer = timer;
        anim = GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Enemy.hp.GetHealth() <= 0 && !died)
        {
            died = true;
        }

        if (died)
        {
            Died();
        }
        else
        {
            if (inRange)
            {
                float angle = getAngle();
                string direction = "right";
                if (angle > 0 && angle < 90 || angle < 0 && angle > -90)
                {
                    hit = Physics2D.Raycast(rayCast.position, Vector2.right, rayCastLength, raycastMask);
                    // RaycastDebugger(direction);
                }
                else
                {
                    direction = "left";
                    hit = Physics2D.Raycast(rayCast.position, Vector2.left, rayCastLength, raycastMask);
                    // RaycastDebugger(direction);
                }
            }

            if (hit.collider != null)
            {
                inRange = true;
                EnemyLogic();
            }
            else if (hit.collider == null)
            {
                inRange = false;
                EnemyLogic();
            }

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
        if (trig.gameObject.tag == "Player" && !cooling)
        {
            target = trig.gameObject;
            inRange = true;
        }
    }

    void EnemyLogic()
    {
        distance = Vector2.Distance(transform.position, target.transform.position);

        if (distance > attackDistance && !inRange && !cooling)
        {
            Move();
            StopAttack();
        }
        else if (inRange && !cooling)
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
        rb.MovePosition((Vector2)transform.position + (Vector2)(direction * moveSpeed * Time.deltaTime));

        // if(angle > 0 && angle < 90 || angle < 0 && angle > -90){
        //     transform.rotation = new Quaternion(0,0,0,1);

        // } else {
        //     transform.rotation = new Quaternion(0,-1,0,0);

        // }
        // }
    }

    void Attack()
    {
        timer = intTimer;
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
        timer -= Time.deltaTime;
        if (timer <= 0 && cooling && attackMode)
        {
            cooling = false;
            timer = intTimer;
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
        if (died)
        {
            Destroy(this.gameObject);
        }
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

