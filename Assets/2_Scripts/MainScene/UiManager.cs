using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    //인게임 UI

    [Header("무기UI")]
    public Image image;
    public TextMeshProUGUI index;
    public new TextMeshProUGUI name;
    public TextMeshProUGUI chance;
    public TextMeshProUGUI damage;

    [Header("재화UI")]
    public TextMeshProUGUI coin;
    

    [Header("메뉴UI")]
    public GameObject menuUI;
    private bool isMenuOpen;

    [Header("설정UI")]
    public GameObject settingUI;
    private bool isSettingOpen;

    [Header("상점UI")]
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
        index.text = $"{WeaponManager.Instance.weaponIndex} 강";
        name.text = $"{WeaponManager.Instance.weaponName}";
        chance.text = $"성공확률 : {WeaponManager.Instance.nextWeaponChance}%";
        damage.text = $"데미지 : {WeaponManager.Instance.weaponDamage}";
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
            EventSystem.current.SetSelectedGameObject(null); // 버튼 비활성화
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
            EventSystem.current.SetSelectedGameObject(null); // 버튼 비활성화
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
            EventSystem.current.SetSelectedGameObject(null); // 버튼 비활성화
        }
    }
}
