using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float baseMoveSpeed = 20f;
    [SerializeField] private float moveSpeed;
    [SerializeField] float weightForRushState = 3;

    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Animator animator;

    float xMov;
    float yMov;
    Vector2 movHorizontal;
    Vector2 movVertical;
    Vector2 velocity;

    string direction = "left";
    void Start()
    {
        moveSpeed = baseMoveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.State == GameState.RushState)
        {
            SetMoveSpeed(baseMoveSpeed * weightForRushState);
        }
        else
        {
            SetMoveSpeed(baseMoveSpeed);

        }
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

    public float GetMoveSpeed()
    {
        return moveSpeed;
    }
    public void SetMoveSpeed(float newValue)
    {
        moveSpeed = newValue;
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
