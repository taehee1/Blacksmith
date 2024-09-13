using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class WeaponData : ScriptableObject
{
    #region 싱글톤
    private static WeaponData instance;

    public static WeaponData Instance
    {
        get { return instance; }
    }
    private void Awake()
    {
        instance = this;
    }
    #endregion

    public WeaponInfo[] weaponInfo;

    [Header("현재무가정보")]
    public int weaponIndex;
    [Space(10)]
    public string weaponName;
    //public string weaponExplain; 나중에 설명추가
    public Sprite weaponImage;
    public float weaponDamage;

    public void ChangeWeapon()
    {
        weaponIndex++;
        weaponName = weaponInfo[weaponIndex].name;
        weaponImage = weaponInfo[weaponIndex].image;
        weaponDamage = weaponInfo[weaponIndex].damage;
}
}

[System.Serializable]
public class WeaponInfo
{
    public string name;
    //public string explain; 나중에 설명추가
    public Sprite image;
    public float damage;
    public float chance;
}
