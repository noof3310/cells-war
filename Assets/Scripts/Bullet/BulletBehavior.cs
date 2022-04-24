using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    private float damage;
    // public float rotationOffet;
    private GameObject target;
    private Rigidbody2D rb;
    private Vector2 movement;
    private RaycastHit2D hit;
    private Animator anim;
    private bool destroy;

    // public Vector3 temp;
    void Start()
    {
        destroy = false;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null && !destroy)
        {
            // Destroy(this.gameObject);
            this.gameObject.SetActive(false);
        }
        else if (!destroy)
        {
            Move();
        }

    }

    void Move()
    {
        Vector3 direction = target.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        direction.Normalize();
        movement = direction;
        transform.Translate(direction * speed * Time.deltaTime);
        // transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        // if (angle > 0 && angle < 90 || angle < 0 && angle > -90)
        // {
        //     transform.rotation = new Quaternion(0, 0, 0, 1);

        // }
        // else
        // {
        //     transform.rotation = new Quaternion(0, 0, 0, 1);
        // }

    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.name == target.name)
        {
            destroy = true;
            anim.SetBool("destroy", true);
        }
    }

    void TriggerDestroy()
    {
        if (destroy && target != null)
        {
            TakeDamage();
            // Destroy(this.gameObject);
            this.gameObject.SetActive(false);

        }
        else if (target == null)
        {

            // Destroy(this.gameObject);
            this.gameObject.SetActive(false);
        }
    }

    public void TakeDamage()
    {
        if (target.tag == "Objective")
        {
            target.GetComponent<Objective>().TakenDamage(damage);
        }
        else if (target.tag == "Enemy")
        {
            target.transform.parent.GetComponent<Enemy>().TakenDamage(damage);
        }
        else if (target.tag == "Tower")
        {
            target.GetComponent<Tower>().TakenDamage(damage);

        }

    }

    public void SetTarget(GameObject currentTarget)
    {
        target = currentTarget;
    }
    public void SetDamage(float currentDamage)
    {
        damage = currentDamage;
    }
    public void Reuse(){
        destroy = false;
        // GetComponent<Animator>();
    }
}
