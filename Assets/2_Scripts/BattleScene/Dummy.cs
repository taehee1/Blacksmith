using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : MonoBehaviour
{
    public DamageTextManager damageTextManager; // DamageTextManager 참조

    public void TakeDamage(int damage)
    {
        // 데미지 텍스트를 훈련봇의 위치 위로 생성
        damageTextManager.ShowDamage(damage, transform.position);

        // 훈련봇의 체력 감소 등의 로직 추가
    }
}
