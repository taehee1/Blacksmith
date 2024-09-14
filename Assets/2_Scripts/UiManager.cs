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
        index.text = $"{weaponManager.weaponIndex} 강";
        name.text = $"{weaponManager.weaponName}";
        chance.text = $"성공확률 : {weaponManager.nextWeaponChance}%";
        damage.text = $"데미지 : {weaponManager.weaponDamage}";
    }
}
