using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour
{
    //전투씬 UI
    [Header("참조")]
    public Dummy dummy;

    [Header("무기UI")]
    public Image image;
    public TextMeshProUGUI index;
    public new TextMeshProUGUI name;
    public TextMeshProUGUI damage;

    [Header("재화UI")]
    public TextMeshProUGUI coin;

    [Header("결과UI")]
    public GameObject resultPanel;

    public TextMeshProUGUI bestDamageText;
    public TextMeshProUGUI currentDamageText;
    public TextMeshProUGUI earnCoinText;

    private void Update()
    {
        WeaponUIUpdate();
        CoinUIUpdate();
    }

    private void WeaponUIUpdate()
    {
        // WeaponManager의 싱글톤 인스턴스를 참조하여 UI를 업데이트
        if (WeaponManager.Instance != null)
        {
            image.sprite = WeaponManager.Instance.weaponImage;
            index.text = $"{WeaponManager.Instance.weaponIndex} 강";
            name.text = $"{WeaponManager.Instance.weaponName}";
            damage.text = $"데미지 : {WeaponManager.Instance.weaponDamage}";
        }
        else
        {
            Debug.Log("WeaponManager 인스턴스를 찾을 수 없습니다");
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
            Debug.Log("GameManager 인스턴스를 찾을 수 없습니다");
        }
    }

    public void ResultUI()
    {
        resultPanel.SetActive(true);
        if (dummy.totalDamage >= GameManager.Instance.bestDamage)
        {
            GameManager.Instance.bestDamage = dummy.totalDamage;
        }

        bestDamageText.text = $"최고 데미지: {GameManager.Instance.bestDamage}";
        currentDamageText.text = $"데미지: {dummy.totalDamage}";
        earnCoinText.text = $"+Coin: {dummy.totalDamage}";
    }
}
