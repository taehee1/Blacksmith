using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public WeaponData weaponData;

    private void Awake()
    {
        ChangeWeapon();
    }

    [Header("현재무가정보")]
    public int weaponIndex;
    public float nextWeaponChance;
    [Space(10)]
    public string weaponName;
    public Sprite weaponImage;
    public float weaponDamage;

    public void ChangeWeapon()
    {
        nextWeaponChance = weaponData.weaponInfo[weaponIndex].chance;
        weaponName = weaponData.weaponInfo[weaponIndex].name;
        weaponImage = weaponData.weaponInfo[weaponIndex].image;
        weaponDamage = weaponData.weaponInfo[weaponIndex].damage;
    }
}
