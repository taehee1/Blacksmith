using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StartTimer : MonoBehaviour
{
    [Header("참조")]
    public Timer timerScript;
    public Player player;

    [Header("오브젝트")]
    public TextMeshProUGUI startTimerText; // 변수 이름을 명확하게 변경

    [Header("수치")]
    [SerializeField] private float timer = 3f;

    private void Start()
    {
        StartCoroutine(TimerCoroutine());
    }

    IEnumerator TimerCoroutine()
    {
        while (timer > 0) // 타이머가 0보다 큰 동안 반복
        {
            timer -= Time.deltaTime; // 타이머 감소
            startTimerText.text = Mathf.Ceil(timer).ToString(); // UI 업데이트
            yield return null;
        }

        startTimerText.text = "0";
        player.canMove = true;
        startTimerText.gameObject.SetActive(false);
        timerScript.StartTimer();
    }
}
