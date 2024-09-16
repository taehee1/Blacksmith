using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [Header("����UI")]
    public WeaponManager weaponManager;
    [Space(10)]
    public Image image;
    public TextMeshProUGUI index;
    public TextMeshProUGUI name;
    public TextMeshProUGUI chance;
    public TextMeshProUGUI damage;

    [Header("�޴�UI")]
    public GameObject menuUI;

    private bool isMenuOpen;

    [Header("����UI")]
    public GameObject settingUI;

    private bool isSettingOpen;

    private void Update()
    {
        WeaponUIUpdate();
    }

    public void WeaponUIUpdate()
    {
        image.sprite = weaponManager.weaponImage;
        index.text = $"{weaponManager.weaponIndex} ��";
        name.text = $"{weaponManager.weaponName}";
        chance.text = $"����Ȯ�� : {weaponManager.nextWeaponChance}%";
        damage.text = $"������ : {weaponManager.weaponDamage}";
    }

    public void MenuUI()
    {
        if (isMenuOpen)
        {
            menuUI.SetActive(!isMenuOpen);
            isMenuOpen = false;
        }
        else
        {
            menuUI.SetActive(!isMenuOpen);
            isMenuOpen = true;
        }
    }

    public void SettingUI()
    {
        if (isSettingOpen)
        {
            settingUI.SetActive(!isSettingOpen);
            isSettingOpen = false;
        }
        else
        {
            settingUI.SetActive(!isSettingOpen);
            isSettingOpen = true;
        }
    }
}
