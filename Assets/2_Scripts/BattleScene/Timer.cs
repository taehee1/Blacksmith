using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;

    [SerializeField] private float timer = 60;

    private void Start()
    {
        timer = 60;
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        timerText.text = $"{timer:N0}s";

        TimeOver();
    }

    private void TimeOver()
    {
        if (timer <= 0)
        {
            Debug.Log("타임오버");
        }
    }
}
