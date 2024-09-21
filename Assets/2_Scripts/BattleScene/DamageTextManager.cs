using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTextManager : MonoBehaviour
{
    public GameObject damageTextPrefab;     // 데미지 텍스트 프리팹
    public Transform damageTextParent;      // 텍스트가 생성될 부모 (Canvas)
    public float randomOffsetRange = 0.5f;  // 랜덤 위치 오프셋 범위

    public void ShowDamage(int damage)
    {
        // 랜덤 오프셋 생성
        Vector3 randomOffset = new Vector3(
            Random.Range(-randomOffsetRange, randomOffsetRange),
            Random.Range(-randomOffsetRange, randomOffsetRange), // Y값도 랜덤
            0
        );

        // 텍스트 생성 위치를 damageTextParent의 위치에 랜덤 오프셋을 추가
        Vector3 spawnPosition = damageTextParent.position + randomOffset;

        // 데미지 텍스트 생성
        GameObject damageTextInstance = Instantiate(damageTextPrefab, spawnPosition, Quaternion.identity, damageTextParent);
        damageTextInstance.GetComponent<DamagePopup>().Setup(damage, this);
    }

    // 애니메이션에서 호출할 함수 (텍스트 삭제)
    public void DestroyDamageText(GameObject damageText)
    {
        Destroy(damageText);
    }
}
