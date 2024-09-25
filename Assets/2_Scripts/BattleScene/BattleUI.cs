using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour
{
    //������ UI
    [Header("����")]
    public Dummy dummy;

    [Header("����UI")]
    public Image image;
    public TextMeshProUGUI index;
    public new TextMeshProUGUI name;
    public TextMeshProUGUI damage;

    [Header("��ȭUI")]
    public TextMeshProUGUI coin;

    [Header("���UI")]
    public GameObject resultPanel;

    public TextMeshProUGUI bestDamageText;
    public TextMeshProUGUI currentDamageText;
    public TextMeshProUGUI earnCoinText;

    [Header("����UI")]
    public GameObject settingUI;

    private bool isSettingOpen;

    private void Update()
    {
        WeaponUIUpdate();
        CoinUIUpdate();
        SettingOpen();
    }

    private void WeaponUIUpdate()
    {
        // WeaponManager�� �̱��� �ν��Ͻ��� �����Ͽ� UI�� ������Ʈ
        if (WeaponManager.Instance != null)
        {
            image.sprite = WeaponManager.Instance.weaponImage;
            index.text = $"{WeaponManager.Instance.weaponIndex} ��";
            name.text = $"{WeaponManager.Instance.weaponName}";
            damage.text = $"������ : {WeaponManager.Instance.weaponDamage}";
        }
        else
        {
            Debug.Log("WeaponManager �ν��Ͻ��� ã�� �� �����ϴ�");
        }
    }

    private void CoinUIUpdate()
    {
        if (GameManager.Instance != null)
        {
            coin.text = GameManager.Instance.coin.ToString("N0");
        }
        else
        {
            Debug.Log("GameManager �ν��Ͻ��� ã�� �� �����ϴ�");
        }
    }

    public void ResultUI()
    {
        resultPanel.SetActive(true);
        if (dummy.totalDamage >= GameManager.Instance.bestDamage)
        {
            GameManager.Instance.bestDamage = dummy.totalDamage;
        }

        bestDamageText.text = $"�ְ� ������: {GameManager.Instance.bestDamage}";
        currentDamageText.text = $"������: {dummy.totalDamage}";
        earnCoinText.text = $"+Coin: {dummy.totalDamage}";
    }

    private void SettingOpen()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SettingUI();
        }
    }

    private void SettingUI()
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
}
