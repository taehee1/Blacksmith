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
        // 각 아이템의 구매 버튼에 이벤트 추가
        foreach (ItemInfo item in itemInfo)
        {
            item.buyBtn.onClick.AddListener(() => BuyItem(item));  // 버튼 클릭 시 BuyItem 함수 호출
        }
    }

    public void BuyItem(ItemInfo item)
    {
        // 플레이어의 골드가 충분한지 확인
        if (GameManager.Instance.coin >= item.price)
        {
            // 아이템 구매 처리
            GameManager.Instance.coin -= item.price;  // 플레이어 골드 차감

            GameManager.Instance.safeguardCount++;

            //buyEffect.SetActive(true);
        }
        else
        {
            Debug.Log("골드가 부족합니다.");
        }
    }
}
