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
        public System.Action onBuyAction; // 아이템 구매 후 처리할 로직
    }

    private void Start()
    {
        // 각 아이템의 구매 버튼에 이벤트 추가
        foreach (ItemInfo item in itemInfo)
        {
            ItemInfo currentItem = item;  // 클로저 문제를 해결하기 위해 로컬 변수 사용
            item.buyBtn.onClick.AddListener(() => BuyItem(currentItem));  // 버튼 클릭 시 BuyItem 함수 호출
        }

        itemInfo[0].onBuyAction = () => {
            GameManager.Instance.safeguardCount++;  //하락방지권 갯수 증가
        };

        itemInfo[1].onBuyAction = () => {
            WeaponManager.Instance.plusDamage += 50;  //무기 데미지 증가
            WeaponManager.Instance.ChangeWeapon();    // 무기 변경
        };
    }

    public void BuyItem(ItemInfo item)
    {
        // 플레이어의 골드가 충분한지 확인
        if (GameManager.Instance.coin >= item.price)
        {
            // 아이템 구매 처리
            GameManager.Instance.coin -= item.price;  // 플레이어 골드 차감

            // 아이템별로 추가 로직이 있는 경우 실행
            item.onBuyAction?.Invoke();

            // 구매 효과 적용
            ShowBuyEffect(item.buyBtn);

            Debug.Log($"아이템 {item.id} 구매 완료. 남은 골드: {GameManager.Instance.coin}");
        }
        else
        {
            Debug.Log("골드가 부족합니다.");
        }
    }

    private void ShowBuyEffect(Button button)
    {
        // 구매 효과 애니메이션 및 사운드 재생
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
