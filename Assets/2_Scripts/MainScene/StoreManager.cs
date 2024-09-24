using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreManager : MonoBehaviour
{
    //public GameObject buyEffect;

    public ItemInfo[] itemInfo;

    [System.Serializable]
    public class ItemInfo
    {
        public Button buyBtn;
        public string id;
        public int price;
    }

    private void Start()
    {
        // �� �������� ���� ��ư�� �̺�Ʈ �߰�
        foreach (ItemInfo item in itemInfo)
        {
            item.buyBtn.onClick.AddListener(() => BuyItem(item));  // ��ư Ŭ�� �� BuyItem �Լ� ȣ��
        }
    }

    public void BuyItem(ItemInfo item)
    {
        // �÷��̾��� ��尡 ������� Ȯ��
        if (GameManager.Instance.coin >= item.price)
        {
            // ������ ���� ó��
            GameManager.Instance.coin -= item.price;  // �÷��̾� ��� ����

            GameManager.Instance.safeguardCount++;

            //buyEffect.SetActive(true);
        }
        else
        {
            Debug.Log("��尡 �����մϴ�.");
        }
    }
}
