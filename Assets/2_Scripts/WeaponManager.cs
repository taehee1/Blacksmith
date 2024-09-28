using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public WeaponData weaponData;

    public static WeaponManager Instance; // 싱글톤 인스턴스

    private void Awake()
    {
        // 싱글톤 패턴: 중복 인스턴스를 방지합니다.
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 시 파괴되지 않도록 설정
        }
        else
        {
            Destroy(gameObject); // 이미 인스턴스가 존재하면 새로 생성된 것을 파괴
            return; // 이후 코드를 실행하지 않음
        }

        ChangeWeapon();
    }

    [Header("현재무가정보")]
    public int weaponIndex;
    public float nextWeaponChance;
    [Space(10)]
    public string weaponName;
    public Sprite weaponImage;
    public int weaponDamage;

    [Header("추가능력치")]
    public int plusDamage;

    public void ChangeWeapon()
    {
        nextWeaponChance = weaponData.weaponInfo[weaponIndex].chance;
        weaponName = weaponData.weaponInfo[weaponIndex].name;
        weaponImage = weaponData.weaponInfo[weaponIndex].image;
        weaponDamage = weaponData.weaponInfo[weaponIndex].damage + plusDamage;
    }
}