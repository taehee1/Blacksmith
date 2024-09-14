using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public WeaponManager weaponManager;

    public Image image;
    public TextMeshProUGUI index;
    public TextMeshProUGUI name;
    public TextMeshProUGUI chance;
    public TextMeshProUGUI damage;

    private void Update()
    {
        UIUpdate();
    }

    public void UIUpdate()
    {
        image.sprite = weaponManager.weaponImage;
        index.text = $"{weaponManager.weaponIndex} ��";
        name.text = $"{weaponManager.weaponName}";
        chance.text = $"����Ȯ�� : {weaponManager.nextWeaponChance}%";
        damage.text = $"������ : {weaponManager.weaponDamage}";
    }
}
