using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 20f;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Animator animator;

    float xMov;
    float yMov;
    Vector2 movHorizontal;
    Vector2 movVertical;
    Vector2 velocity;

    string direction = "left";

    // Update is called once per frame
    void Update()
    {
        xMov = Input.GetAxisRaw("Horizontal");
        yMov = Input.GetAxisRaw("Vertical");

        movHorizontal = transform.right * xMov;
        movVertical = transform.up * yMov;

        velocity = (movHorizontal + movVertical).normalized * moveSpeed;

        animator.SetFloat("Speed", velocity.sqrMagnitude);

        if (xMov > 0)
        {
            spriteRenderer.flipX = true;
            direction = "right";
        }
        else if (xMov < 0)
        {
            spriteRenderer.flipX = false;
            direction = "left";
        }
        
        if (yMov > 0)
        {
            direction = "up";
        }
        else if (yMov < 0)
        {
            direction = "down";
        }

        rb.MovePosition(rb.position + velocity * Time.deltaTime);
    }

    public string getDirection()
    {
        return direction;
    }

    public Vector3 getPosition()
    {
        Vector3 position = rb.position;
        return position;
    }
}
