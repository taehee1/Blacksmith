using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    //�ΰ��� UI

    [Header("����UI")]
    public Image image;
    public TextMeshProUGUI index;
    public new TextMeshProUGUI name;
    public TextMeshProUGUI chance;
    public TextMeshProUGUI damage;

    [Header("��ȭUI")]
    public TextMeshProUGUI coin;
    

    [Header("�޴�UI")]
    public GameObject menuUI;
    private bool isMenuOpen;

    [Header("����UI")]
    public GameObject settingUI;
    private bool isSettingOpen;

    [Header("����UI")]
    public GameObject storeUI;
    private bool isStoreOpen;

    private void Update()
    {
        WeaponUIUpdate();
        CoinUIUpdate();
        MenuOpen();
    }

    private void WeaponUIUpdate()
    {
        image.sprite = WeaponManager.Instance.weaponImage;
        index.text = $"{WeaponManager.Instance.weaponIndex} ��";
        name.text = $"{WeaponManager.Instance.weaponName}";
        chance.text = $"����Ȯ�� : {WeaponManager.Instance.nextWeaponChance}%";
        damage.text = $"������ : {WeaponManager.Instance.weaponDamage}";
    }

    private void CoinUIUpdate()
    {
        coin.text = GameManager.Instance.coin.ToString("N0");
    }

    private void MenuOpen()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MenuUI();
        }
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
            EventSystem.current.SetSelectedGameObject(null); // ��ư ��Ȱ��ȭ
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
            EventSystem.current.SetSelectedGameObject(null); // ��ư ��Ȱ��ȭ
        }
    }

    public void StoreUI()
    {
        if (isStoreOpen)
        {
            storeUI.SetActive(!isStoreOpen);
            isStoreOpen = false;
        }
        else
        {
            storeUI.SetActive(!isStoreOpen);
            isStoreOpen = true;
            EventSystem.current.SetSelectedGameObject(null); // ��ư ��Ȱ��ȭ
        }
    }
}
