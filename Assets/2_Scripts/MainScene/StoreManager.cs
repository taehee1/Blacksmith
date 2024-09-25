using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreManager : MonoBehaviour
{
    public GameObject buyEffect;

    public ItemInfo[] itemInfo;

    [System.Serializable]
    public class ItemInfo
    {
        public Button buyBtn;
        public int id;
        public int price;
        public System.Action onBuyAction; // ������ ���� �� ó���� ����
    }

    private void Start()
    {
        // �� �������� ���� ��ư�� �̺�Ʈ �߰�
        foreach (ItemInfo item in itemInfo)
        {
            ItemInfo currentItem = item;  // Ŭ���� ������ �ذ��ϱ� ���� ���� ���� ���
            item.buyBtn.onClick.AddListener(() => BuyItem(currentItem));  // ��ư Ŭ�� �� BuyItem �Լ� ȣ��
        }

        itemInfo[0].onBuyAction = () => {
            GameManager.Instance.safeguardCount++;  //�϶������� ���� ����
        };

        itemInfo[1].onBuyAction = () => {
            WeaponManager.Instance.plusDamage += 50;  //���� ������ ����
            WeaponManager.Instance.ChangeWeapon();    // ���� ����
        };
    }

    public void BuyItem(ItemInfo item)
    {
        // �÷��̾��� ��尡 ������� Ȯ��
        if (GameManager.Instance.coin >= item.price)
        {
            // ������ ���� ó��
            GameManager.Instance.coin -= item.price;  // �÷��̾� ��� ����

            // �����ۺ��� �߰� ������ �ִ� ��� ����
            item.onBuyAction?.Invoke();

            // ���� ȿ�� ����
            ShowBuyEffect(item.buyBtn);

            Debug.Log($"������ {item.id} ���� �Ϸ�. ���� ���: {GameManager.Instance.coin}");
        }
        else
        {
            Debug.Log("��尡 �����մϴ�.");
        }
    }

    private void ShowBuyEffect(Button button)
    {
        // ���� ȿ�� �ִϸ��̼� �� ���� ���
        buyEffect.GetComponent<RectTransform>().anchoredPosition = button.GetComponent<RectTransform>().anchoredPosition;
        buyEffect.SetActive(true);
        buyEffect.GetComponentInParent<AudioSource>().Play();
        Invoke("OffEffect", 0.2f);
    }

    private void OffEffect()
    {
        buyEffect.SetActive(false);
    }
}
