using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    [Header("수치")] // 인스펙터에서 수치 관련 변수들을 그룹화해서 표시
    public float moveSpeed = 5f;  // 플레이어의 이동 속도
    public float jumpPower = 5f;  // 플레이어의 점프력

    // 상태 관련 변수
    public bool canMove = false;  // 플레이어가 움직일 수 있는지 여부
    private bool isRight = true;  // 플레이어가 오른쪽을 보고 있는지 여부
    private bool canJump = true;  // 플레이어가 점프할 수 있는지 여부

    private Rigidbody2D rb;  // 플레이어의 Rigidbody2D 컴포넌트 (물리 이동을 담당)
    private Animator anim;   // 플레이어의 Animator 컴포넌트 (애니메이션을 담당)

    private void Awake()
    {
        // 컴포넌트 참조 획득
        rb = GetComponent<Rigidbody2D>();  // Rigidbody2D 초기화
        anim = GetComponent<Animator>();   // Animator 초기화
    }

    private void Update()
    {
        // 플레이어가 움직일 수 없으면 아무 동작도 하지 않음
        if (!canMove)
        {
            return;
        }

        // 플레이어 이동 및 점프 처리
        Move();  // 이동 처리
        Jump();  // 점프 처리
    }

    // 플레이어 이동을 처리하는 함수
    void Move()
    {
        // 입력에 따른 수평 방향 값 (-1, 0, 1)
        float horizontal = Input.GetAxisRaw("Horizontal");

        // Rigidbody2D를 통해 플레이어 이동 처리
        rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);

        // 애니메이션: 움직이는 중인지 여부에 따라 "Run" 애니메이션 재생
        if (horizontal != 0)
        {
            anim.SetBool("Run", true);
        }
        else
        {
            anim.SetBool("Run", false);
        }

        // 플레이어 방향 전환 (오른쪽에서 왼쪽 또는 왼쪽에서 오른쪽으로)
        if (isRight && horizontal < 0)  // 오른쪽 보고 있다가 왼쪽으로 이동할 때
        {
            isRight = !isRight;  // 방향 반전
            Vector3 localScale = transform.localScale;  // 현재 스케일 값 가져옴
            localScale.x *= -1f;  // X축을 반전시킴 (플립)
            transform.localScale = localScale;  // 반영
        }
        else if (!isRight && horizontal > 0)  // 왼쪽 보고 있다가 오른쪽으로 이동할 때
        {
            isRight = !isRight;  // 방향 반전
            Vector3 localScale = transform.localScale;  // 현재 스케일 값 가져옴
            localScale.x *= -1f;  // X축을 반전시킴
            transform.localScale = localScale;  // 반영
        }
    }

    // 플레이어 점프를 처리하는 함수
    void Jump()
    {
        // 스페이스바를 누르고 점프 가능 상태일 때만 점프
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            canJump = false;  // 점프 중일 때는 다시 점프 못하도록 설정
            anim.SetTrigger("Jump");  // 점프 애니메이션 트리거

            // Rigidbody2D를 이용해 위쪽으로 속도 부여 (점프)
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
        }
    }

    // 충돌 감지 함수 (2D 충돌 처리)
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 바닥 (Ground 태그를 가진 오브젝트)에 닿으면 점프 가능 상태로 전환
        if (collision.gameObject.tag == "Ground")
        {
            canJump = true;  // 다시 점프할 수 있게 설정
        }
    }
}