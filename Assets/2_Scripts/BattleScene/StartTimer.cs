using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StartTimer : MonoBehaviour
{
    [Header("����")]
    public Timer timerScript;
    public Player player;

    [Header("������Ʈ")]
    public TextMeshProUGUI startTimerText; // ���� �̸��� ��Ȯ�ϰ� ����

    [Header("��ġ")]
    [SerializeField] private float timer = 3f;

    private void Start()
    {
        StartCoroutine(TimerCoroutine());
    }

    IEnumerator TimerCoroutine()
    {
        while (timer > 0) // Ÿ�̸Ӱ� 0���� ū ���� �ݺ�
        {
            timer -= Time.deltaTime; // Ÿ�̸� ����
            startTimerText.text = Mathf.Ceil(timer).ToString(); // UI ������Ʈ
            yield return null;
        }

        startTimerText.text = "0";
        player.canMove = true;
        startTimerText.gameObject.SetActive(false);
        timerScript.StartTimer();
    }
}
