using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class WeaponData : ScriptableObject
{
    public WeaponInfo[] weaponInfo;
}

[System.Serializable]
public class WeaponInfo
{
    public float chance;
    public string name;
    public Sprite image;
    public int damage;
}
