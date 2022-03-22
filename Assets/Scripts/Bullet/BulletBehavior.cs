using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    public int speed;
    public int damage;
    // public float rotationOffet;
    private static GameObject target;
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
        target = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (!destroy)
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
        if (collider.gameObject.name == "Player")
        {
            destroy = true;
            anim.SetBool("destroy", true);
        }
    }

    void TriggerDestroy()
    {
        if (destroy)
        {
            Destroy(this.gameObject);
        }
    }
}
