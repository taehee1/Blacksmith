using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public WeaponData weaponData;

    public static WeaponManager Instance; // �̱��� �ν��Ͻ�

    private void Awake()
    {
        // �̱��� ����: �ߺ� �ν��Ͻ��� �����մϴ�.
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �� ��ȯ �� �ı����� �ʵ��� ����
        }
        else
        {
            Destroy(gameObject); // �̹� �ν��Ͻ��� �����ϸ� ���� ������ ���� �ı�
            return; // ���� �ڵ带 �������� ����
        }

        ChangeWeapon();
    }

    [Header("���繫������")]
    public int weaponIndex;
    public float nextWeaponChance;
    [Space(10)]
    public string weaponName;
    public Sprite weaponImage;
    public int weaponDamage;

    [Header("�߰��ɷ�ġ")]
    public int plusDamage;

    public void ChangeWeapon()
    {
        nextWeaponChance = weaponData.weaponInfo[weaponIndex].chance;
        weaponName = weaponData.weaponInfo[weaponIndex].name;
        weaponImage = weaponData.weaponInfo[weaponIndex].image;
        weaponDamage = weaponData.weaponInfo[weaponIndex].damage + plusDamage;
    }
}