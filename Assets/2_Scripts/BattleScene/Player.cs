using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    [Header("��ġ")] // �ν����Ϳ��� ��ġ ���� �������� �׷�ȭ�ؼ� ǥ��
    public float moveSpeed = 5f;  // �÷��̾��� �̵� �ӵ�
    public float jumpPower = 5f;  // �÷��̾��� ������

    // ���� ���� ����
    public bool canMove = false;  // �÷��̾ ������ �� �ִ��� ����
    private bool isRight = true;  // �÷��̾ �������� ���� �ִ��� ����
    private bool canJump = true;  // �÷��̾ ������ �� �ִ��� ����

    private Rigidbody2D rb;  // �÷��̾��� Rigidbody2D ������Ʈ (���� �̵��� ���)
    private Animator anim;   // �÷��̾��� Animator ������Ʈ (�ִϸ��̼��� ���)

    private void Awake()
    {
        // ������Ʈ ���� ȹ��
        rb = GetComponent<Rigidbody2D>();  // Rigidbody2D �ʱ�ȭ
        anim = GetComponent<Animator>();   // Animator �ʱ�ȭ
    }

    private void Update()
    {
        // �÷��̾ ������ �� ������ �ƹ� ���۵� ���� ����
        if (!canMove)
        {
            return;
        }

        // �÷��̾� �̵� �� ���� ó��
        Move();  // �̵� ó��
        Jump();  // ���� ó��
    }

    // �÷��̾� �̵��� ó���ϴ� �Լ�
    void Move()
    {
        // �Է¿� ���� ���� ���� �� (-1, 0, 1)
        float horizontal = Input.GetAxisRaw("Horizontal");

        // Rigidbody2D�� ���� �÷��̾� �̵� ó��
        rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);

        // �ִϸ��̼�: �����̴� ������ ���ο� ���� "Run" �ִϸ��̼� ���
        if (horizontal != 0)
        {
            anim.SetBool("Run", true);
        }
        else
        {
            anim.SetBool("Run", false);
        }

        // �÷��̾� ���� ��ȯ (�����ʿ��� ���� �Ǵ� ���ʿ��� ����������)
        if (isRight && horizontal < 0)  // ������ ���� �ִٰ� �������� �̵��� ��
        {
            isRight = !isRight;  // ���� ����
            Vector3 localScale = transform.localScale;  // ���� ������ �� ������
            localScale.x *= -1f;  // X���� ������Ŵ (�ø�)
            transform.localScale = localScale;  // �ݿ�
        }
        else if (!isRight && horizontal > 0)  // ���� ���� �ִٰ� ���������� �̵��� ��
        {
            isRight = !isRight;  // ���� ����
            Vector3 localScale = transform.localScale;  // ���� ������ �� ������
            localScale.x *= -1f;  // X���� ������Ŵ
            transform.localScale = localScale;  // �ݿ�
        }
    }

    // �÷��̾� ������ ó���ϴ� �Լ�
    void Jump()
    {
        // �����̽��ٸ� ������ ���� ���� ������ ���� ����
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            canJump = false;  // ���� ���� ���� �ٽ� ���� ���ϵ��� ����
            anim.SetTrigger("Jump");  // ���� �ִϸ��̼� Ʈ����

            // Rigidbody2D�� �̿��� �������� �ӵ� �ο� (����)
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
        }
    }

    // �浹 ���� �Լ� (2D �浹 ó��)
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �ٴ� (Ground �±׸� ���� ������Ʈ)�� ������ ���� ���� ���·� ��ȯ
        if (collision.gameObject.tag == "Ground")
        {
            canJump = true;  // �ٽ� ������ �� �ְ� ����
        }
    }
}