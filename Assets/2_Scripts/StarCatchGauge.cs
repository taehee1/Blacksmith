using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StarCatchGauge : MonoBehaviour
{
    public GameObject gauge;

    public RectTransform gaugeImageRect;  // �����̴� �̹����� RectTransform
    public RectTransform rangeIndicatorRect;  // ���� ������ ǥ���ϴ� RectTransform
    public float speed = 300f;  // �̹��� �̵� �ӵ�

    [Header("������ ����")]
    [SerializeField] private float minX = -4.3f;  // �̹��� �̵� ������ �ּ� X��
    [SerializeField] private float maxX = 4.3f;   // �̹��� �̵� ������ �ִ� X��

    [Header("���� ����")]
    public float chanceRange = 4.3f;

    [Header("���� ũ��")]
    public float chanceMin = 1.5f;
    public float chanceMax = 1.7f;

    [Header("�ð�����")]
    public float timeLimit = 5f;  // ��Ÿĳġ �ð� ����
    public TextMeshProUGUI timerText;  // �ð� ������ ǥ���� UI �ؽ�Ʈ

    [Header("����")]
    public EnforceManager enforceManager;

    private float successRangeMin;
    private float successRangeMax;
    private bool isMovingRight = true;  // �̹����� ���������� �̵� ������ ����
    private float currentTime;  // ���� �ð�
    private bool isTimeUp = true;

    void Start()
    {
        SetRandomSuccessRange();  // ���� �� ���� ���� ���� ����
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ResetTimer();  // Ÿ�̸� �ʱ�ȭ

            SetRandomSuccessRange();

            gauge.SetActive(true);

            Vector2 currentPosition = gaugeImageRect.anchoredPosition;

            currentPosition.x = minX;

            gaugeImageRect.anchoredPosition = currentPosition;
        }

        if (isTimeUp)
        {
            //gauge.SetActive(false);
            return;
        }

        UpdateTimer();

        if (Input.GetKey(KeyCode.Space))
        {
            MoveGaugeImage();  // ������ �̹����� �Դ� ���� �̵�
        }

        // �����̽��� �Է� ����
        if (Input.GetKeyUp(KeyCode.Space))
        {
            float currentX = gaugeImageRect.anchoredPosition.x;

            // ���� ��ġ�� ���� ���� ���� �ִ��� Ȯ��
            if (currentX >= successRangeMin && currentX <= successRangeMax)
            {
                Debug.Log("Success!");
                enforceManager.Enforce(true);
                // ���� �ǵ��
            }
            else
            {
                Debug.Log("Fail...");
                enforceManager.Enforce(false);
                // ���� �ǵ��
            }

            gauge.SetActive(false);
            isTimeUp = true;  // Ÿ�̸� ���� ���·� ����
        }

        if (currentTime <= 0)
        {
            if (!isTimeUp)  // Ÿ�̸Ӱ� ����� ���°� �ƴϸ� ó��
            {
                Debug.Log("Time's up! Fail...");
                enforceManager.Enforce(false);
                isTimeUp = true;  // Ÿ�̸� ���� ���·� ����
            }
        }
    }

    // ������ �̹����� �¿�� �̵���Ű�� �Լ�
    void MoveGaugeImage()
    {
        Vector2 currentPosition = gaugeImageRect.anchoredPosition;

        // �̹����� ���������� �̵� ���� ��
        if (isMovingRight)
        {
            currentPosition.x += speed * Time.deltaTime;

            // �ִ� ������ ������ ������ �ݴ��
            if (currentPosition.x >= maxX)
            {
                currentPosition.x = maxX;
                isMovingRight = false;
            }
        }
        // �̹����� �������� �̵� ���� ��
        else
        {
            currentPosition.x -= speed * Time.deltaTime;

            // �ּ� ������ ������ ������ �ٽ� ����������
            if (currentPosition.x <= minX)
            {
                currentPosition.x = minX;
                isMovingRight = true;
            }
        }

        gaugeImageRect.anchoredPosition = currentPosition;
    }

    // ���� ���� ������ �����ϴ� �Լ�
    void SetRandomSuccessRange()
    {
        // ���� ������ minX ~ maxX ���� ������ �������� ����
        successRangeMin = Random.Range(minX, maxX - chanceRange);
        successRangeMax = successRangeMin + Random.Range(chanceMin, chanceMax);  // ���� ���� ũ��� 50 ~ 100 ���̷� ����

        if (successRangeMax > maxX)
        {
            successRangeMax = maxX;
        }

        // ���� ������ �ð������� ������Ʈ
        UpdateRangeIndicator();
    }

    // ���� ������ �ð������� ������Ʈ�ϴ� �Լ�
    void UpdateRangeIndicator()
    {
        Vector2 rangePosition = rangeIndicatorRect.anchoredPosition;
        rangePosition.x = (successRangeMin + successRangeMax) / 2f;  // ���� ������ �߰� ��ġ�� ����

        rangeIndicatorRect.anchoredPosition = rangePosition;

        // ���� ���� ũ�⸦ ����
        float rangeWidth = successRangeMax - successRangeMin;
        rangeIndicatorRect.sizeDelta = new Vector2(rangeWidth, rangeIndicatorRect.sizeDelta.y);
    }

    void UpdateTimer()
    {
        currentTime -= Time.deltaTime;
        timerText.text = currentTime.ToString("F0");  // �Ҽ��� �� �ڸ����� ǥ��

        if (currentTime < 0)
        {
            currentTime = 0;
        }
    }

    // Ÿ�̸Ӹ� �ʱ�ȭ�ϴ� �Լ�
    void ResetTimer()
    {
        currentTime = timeLimit;  // �ð� ������ �ʱ�ȭ
        isTimeUp = false;
    }
}
