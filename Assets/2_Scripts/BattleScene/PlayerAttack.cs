using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    // 플레이어의 히트 스캔 오브젝트 (공격 판정을 담당하는 오브젝트)
    public GameObject hitScan;

    // 공격 데미지와 히트 횟수
    public int damage;  // 플레이어가 가하는 데미지
    public int hitCount = 3;  // 한 번의 공격으로 최대 히트 수

    private void Start()
    {
        // 무기 상태 설정
        SetUpState();
    }

    // 히트 스캔을 활성화하는 함수 (공격 애니메이션 중 호출)
    public void HitScanOn()
    {
        // 오디오 소스를 통해 공격 사운드 재생
        GetComponent<AudioSource>().Play();

        // 히트 스캔 오브젝트 활성화
        hitScan.SetActive(true);

        // 0.05초 후에 히트 스캔 오브젝트를 비활성화
        Invoke("HitScanOff", 0.05f);
    }

    // 히트 스캔 오브젝트를 비활성화하는 함수
    private void HitScanOff()
    {
        hitScan.SetActive(false);  // 히트 스캔 오브젝트 비활성화
    }

    // 무기의 상태를 초기화하는 함수 (무기 데미지를 설정)
    private void SetUpState()
    {
        // WeaponManager가 존재할 경우 무기 데미지를 가져옴
        if (WeaponManager.Instance != null)
        {
            damage = WeaponManager.Instance.weaponDamage;  // 현재 무기의 데미지 설정
        }
    }
}
