using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    [Header("수치")]
    public float moveSpeed = 5f;
    public float jumpPower = 5f;

    //상태
    public bool canMove = false;
    private bool isRight = true;
    private bool canJump = true;

    private Rigidbody2D rb;
    private Animator anim;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!canMove)
        {
            return;
        }
        Move();
        Jump();
    }

    void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");

        rb.velocity = new Vector2 (horizontal * moveSpeed, rb.velocity.y);

        if (horizontal != 0)
        {
            anim.SetBool("Run", true);
        }
        else
        {
            anim.SetBool("Run", false);
        }

        if (isRight && horizontal < 0)
        {
            isRight = !isRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
        else if (!isRight && horizontal > 0)
        {
            isRight = !isRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            canJump = false;
            anim.SetTrigger("Jump");

            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            canJump = true;
        }
    }
}