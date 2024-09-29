using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    //참조 변수
    private Player player;  // Player 스크립트 참조 (플레이어 상태 확인용)

    [Header("수치")]  // 인스펙터에서 수치 관련 변수들을 그룹화해서 표시
    public float comboDelay;  // 콤보 공격 사이의 딜레이 시간

    private int currentCombo;  // 현재 콤보 상태 (1, 2, 3번 공격 중 어느 단계인지)
    private bool canNext = true;  // 다음 콤보를 입력할 수 있는지 여부
    private bool canAttack = true;  // 공격이 가능한 상태인지 여부
    private float comboTimer;  // 콤보 입력 제한 시간 타이머

    private Animator anim;  // Animator 컴포넌트 (애니메이션 처리)

    private void Awake()
    {
        // 참조 변수 초기화
        player = GetComponent<Player>();  // Player 스크립트 가져오기
        anim = GetComponent<Animator>();  // Animator 컴포넌트 가져오기
    }

    private void Update()
    {
        // 플레이어가 움직일 수 없는 상태라면 공격을 실행하지 않음
        if (!player.canMove)
        {
            return;
        }

        Attack();      // 공격 처리
        ComboDelay();  // 콤보 딜레이 처리
    }

    // 공격 입력 및 콤보 처리
    private void Attack()
    {
        // Z 키를 눌렀을 때, 다음 콤보가 가능하고, 공격 가능한 상태라면
        if (Input.GetKeyDown(KeyCode.Z) && canNext && canAttack)
        {
            canNext = false;  // 다음 콤보 입력을 잠시 막음 (한 번에 하나의 공격만 처리)

            // 콤보에 따라 다른 공격 애니메이션 실행
            if (currentCombo == 0)
            {
                anim.SetTrigger("Attack1");  // 첫 번째 공격
            }
            else if (currentCombo == 1)
            {
                anim.SetTrigger("Attack2");  // 두 번째 공격
            }
            else if (currentCombo == 2)
            {
                anim.SetTrigger("Attack3");  // 세 번째 공격
                canAttack = false;  // 세 번째 공격 이후에는 잠시 공격 불가
            }

            comboTimer = 0;  // 콤보 타이머 초기화

            currentCombo++;  // 콤보 단계 증가

            // 콤보가 3번 이상 되면 콤보 초기화
            if (currentCombo > 2)
            {
                ResetCombo();  // 콤보 초기화 함수 호출
            }
        }
    }

    // 콤보 딜레이를 체크하는 함수
    private void ComboDelay()
    {
        comboTimer += Time.deltaTime;  // 콤보 타이머 증가

        // 콤보 타이머가 설정된 딜레이를 초과하면 다시 공격 가능 상태로
        if (comboTimer > comboDelay)
        {
            canAttack = true;  // 공격 가능 상태로 변경
        }
    }

    // 콤보를 초기화하는 함수 (다음 공격을 처음 단계로)
    public void ResetCombo()
    {
        currentCombo = 0;  // 콤보 단계를 초기화
    }

    // 다음 공격이 가능하다고 설정하는 함수 (애니메이션 이벤트와 연동될 수 있음)
    public void CanNext()
    {
        canNext = true;  // 다음 콤보 입력을 허용
    }
}