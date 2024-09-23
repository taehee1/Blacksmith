using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dummy : MonoBehaviour
{
    public DamageTextManager damageTextManager; // DamageTextManager 참조
    public TextMeshProUGUI dpsText; // DPS UI 텍스트
    public TextMeshProUGUI totalDamageText; // 총 피해량 UI 텍스트

    public float totalDamage = 0;
    private float damagePerSecond = 0;
    private float damageTimer = 0;

    private void Start()
    {
        // 1초마다 DPS 업데이트 코루틴 시작
        StartCoroutine(UpdateDPS());
    }

    public void TakeDamage(int damage)
    {
        totalDamage += damage; // 총 피해량 증가

        GameManager.Instance.coin += damage;

        // 데미지 텍스트를 훈련봇의 위치 위로 생성
        damageTextManager.ShowDamage(damage);

        // 타이머 업데이트
        if (damageTimer == 0)
        {
            damageTimer = Time.time; // 첫 공격 시 현재 시간으로 초기화
        }
    }


    private IEnumerator UpdateDPS()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f); // 1초 대기

            if (damageTimer > 0)
            {
                float elapsedTime = Time.time - damageTimer; // 경과 시간 계산
                if (elapsedTime > 0)
                {
                    damagePerSecond = totalDamage / elapsedTime; // DPS 계산
                }
            }

            // UI 업데이트
            totalDamageText.text = "합계: " + totalDamage;
            dpsText.text = "초당 피해량: " + damagePerSecond.ToString("F2");
        }
    }

    /*(1초동안의 피해량 계산)
    public DamageTextManager damageTextManager; // DamageTextManager 참조
    public TextMeshProUGUI dpsText; // DPS UI 텍스트
    public TextMeshProUGUI totalDamageText; // 총 피해량 UI 텍스트

    private float totalDamage = 0;
    private float currentDamageThisSecond = 0; // 현재 1초 동안의 피해량

    private void Start()
    {
        // 1초마다 DPS 업데이트 코루틴 시작
        StartCoroutine(UpdateDPS());
    }

    public void TakeDamage(int damage)
    {
        totalDamage += damage; // 총 피해량 증가
        currentDamageThisSecond += damage; // 현재 1초 동안의 피해량 증가

        // 데미지 텍스트를 훈련봇의 위치로 생성
        damageTextManager.ShowDamage(damage);
    }


    private IEnumerator UpdateDPS()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f); // 1초 대기

            // 현재 1초 동안의 DPS 계산
            dpsText.text = "초당 피해량: " + currentDamageThisSecond.ToString("F2");

            // 현재 1초 동안의 피해량 초기화
            currentDamageThisSecond = 0;

            // UI 업데이트
            totalDamageText.text = "합계: " + totalDamage;
        }
    }
    */
}