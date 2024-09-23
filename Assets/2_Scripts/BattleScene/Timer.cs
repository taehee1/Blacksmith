using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [Header("참조")]
    public BattleUI battleUI;

    [Header("오브젝트")]
    public TextMeshProUGUI timerText;

    [Header("수치")]
    [SerializeField] private float timer = 60;

    private bool isTimerRunning = false; // 타이머 상태를 관리하는 변수

    private void Start()
    {
        timer = 60;
    }

    private void Update()
    {
        if (isTimerRunning) // 타이머가 실행 중일 때만 감소
        {
            timer -= Time.deltaTime;

            timerText.text = $"{Mathf.Max(timer, 0):N0}s"; // 0 이하로 내려가지 않도록 설정

            TimeOver();
        }
    }

    public void StartTimer() // 타이머 시작 메서드
    {
        isTimerRunning = true;
    }

    private void TimeOver()
    {
        if (timer <= 0)
        {
            isTimerRunning = false; // 타이머 중지
            timerText.text = "0s"; // 0초 표시
            Result();
        }
    }

    private void Result()
    {
        Debug.Log("타이머가 종료되었습니다.");
        battleUI.ResultUI();
    }
}
