using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [Header("����")]
    public BattleUI battleUI;

    [Header("������Ʈ")]
    public TextMeshProUGUI timerText;

    [Header("��ġ")]
    [SerializeField] private float timer = 60;

    private bool isTimerRunning = false; // Ÿ�̸� ���¸� �����ϴ� ����

    private void Start()
    {
        timer = 60;
    }

    private void Update()
    {
        if (isTimerRunning) // Ÿ�̸Ӱ� ���� ���� ���� ����
        {
            timer -= Time.deltaTime;

            timerText.text = $"{Mathf.Max(timer, 0):N0}s"; // 0 ���Ϸ� �������� �ʵ��� ����

            TimeOver();
        }
    }

    public void StartTimer() // Ÿ�̸� ���� �޼���
    {
        isTimerRunning = true;
    }

    private void TimeOver()
    {
        if (timer <= 0)
        {
            isTimerRunning = false; // Ÿ�̸� ����
            timerText.text = "0s"; // 0�� ǥ��
            Result();
        }
    }

    private void Result()
    {
        Debug.Log("Ÿ�̸Ӱ� ����Ǿ����ϴ�.");
        battleUI.ResultUI();
    }
}
