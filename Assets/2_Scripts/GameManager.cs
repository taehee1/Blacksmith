using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        // 싱글톤 패턴: 중복 인스턴스를 방지합니다.
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 시 파괴되지 않도록 설정
        }
        else
        {
            Destroy(gameObject); // 이미 인스턴스가 존재하면 새로 생성된 것을 파괴
        }
    }

    [Header("코인")]
    public int coin;

    [Header("점수")]
    public float bestDamage;

    [Header("아이템")]
    public int safeguardCount;
}
