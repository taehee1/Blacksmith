using Michsky.UI.MTP;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnforceManager : MonoBehaviour
{
    #region 싱글톤
    public static EnforceManager instance;  // 싱글톤 패턴을 위한 인스턴스 변수

    private void Init()
    {
        instance = this;  // 인스턴스 할당
    }
    #endregion

    public GameObject successResult;  // 강화 성공 시 표시되는 결과 UI 오브젝트
    public GameObject failResult;  // 강화 실패 시 표시되는 결과 UI 오브젝트

    public Toggle safeguardToggle;  // 세이프가드(보호막) 활성화 여부를 나타내는 토글 UI

    private void Awake()
    {
        Init();  // 인스턴스 초기화
    }

    // 강화 실행 함수, 성공 여부를 받아 처리
    public void Enforce(bool isSuccess)
    {
        float starCatchChance = 1;  // 스타캐치 성공 시 강화 확률 보정값

        if (isSuccess) starCatchChance = 1.05f;  // 스타캐치 성공 시 강화 확률 5% 증가
        else starCatchChance = 1f;  // 실패 시 기본 확률 유지

        Debug.Log("강화확률:" + starCatchChance * WeaponManager.Instance.nextWeaponChance);  // 최종 강화 확률 출력

        float enforceChance = Random.Range(0f, 100f);  // 0부터 100 사이의 랜덤 값 생성

        // 강화 확률을 비교하여 성공/실패 여부 결정
        if (enforceChance < WeaponManager.Instance.nextWeaponChance * starCatchChance)
        {
            // 강화 성공 처리
            successResult.SetActive(true);  // 성공 UI 활성화
            Debug.Log("강화성공");  // 성공 로그 출력
        }
        else
        {
            // 강화 실패 처리
            failResult.SetActive(true);  // 실패 UI 활성화
            Debug.Log("강화실패");  // 실패 로그 출력
        }
    }

    // 강화 성공 시 처리하는 함수
    public void Success()
    {
        WeaponManager.Instance.weaponIndex++;  // 무기 인덱스를 하나 증가 (다음 단계로)
        WeaponManager.Instance.ChangeWeapon();  // 무기 변경 처리
        StarCatchGauge.instance.isEnforcing = false;  // 강화 중 상태 해제
    }

    // 강화 실패 시 처리하는 함수
    public void Fail()
    {
        // 세이프가드가 켜져 있고, 세이프가드 카운트가 남아 있을 때
        if (safeguardToggle.isOn && GameManager.Instance.safeguardCount > 0)
        {
            GameManager.Instance.safeguardCount--;  // 세이프가드 카운트를 하나 줄임
        }
        else
        {
            WeaponManager.Instance.weaponIndex--;  // 무기 인덱스를 하나 줄임 (이전 단계로)
            WeaponManager.Instance.ChangeWeapon();  // 무기 변경 처리
        }
        StarCatchGauge.instance.isEnforcing = false;  // 강화 중 상태 해제
    }
}
