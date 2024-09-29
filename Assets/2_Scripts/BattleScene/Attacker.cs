using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    // 플레이어의 공격 정보를 담고 있는 PlayerAttack 스크립트
    [SerializeField] private PlayerAttack playerAttack;

    // 카메라 흔들림을 제어하는 CameraShake 스크립트
    private CameraShake cameraShake;

    // 공격 데미지와 히트 횟수
    private int damage;    // 공격 시 가하는 데미지
    private int hitCount;  // 한 번의 공격으로 몇 번 타격할 것인지

    private void Start()
    {
        // CameraShake 컴포넌트를 가져옴
        cameraShake = GetComponent<CameraShake>();

        // PlayerAttack으로부터 데미지와 히트 카운트를 가져옴
        damage = playerAttack.damage;
        hitCount = playerAttack.hitCount;
    }

    // 공격 범위에 적이 들어왔을 때 실행되는 함수
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 충돌한 오브젝트의 태그가 "Dummy"인 경우 (타격 가능한 적)
        if (collision.tag == "Dummy")
        {
            // 카메라 흔들림 효과 실행
            cameraShake.ShakeCamera();

            // hitCount 횟수만큼 데미지를 가함
            for (int i = 0; i < hitCount; i++)
            {
                // 더미(Dummy) 오브젝트에 데미지를 가하는 함수 호출
                collision.GetComponent<Dummy>().TakeDamage(damage);
            }
        }
    }
}
