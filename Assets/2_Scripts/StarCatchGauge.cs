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
    public float minX = -4.3f;  // �̹��� �̵� ������ �ּ� X��
    public float maxX = 4.3f;   // �̹��� �̵� ������ �ִ� X��

    [Header("���� ����")]
    public float chanceRange = 4.3f;

    [Header("���� ũ��")]
    public float chanceMin = 1.5f;
    public float chanceMax = 1.7f;

    [Header("�ð�����")]
    public float timeLimit = 5f;  // ��Ÿĳġ �ð� ����
    [SerializeField] private TextMeshProUGUI timerText;

    [Header("�ִϸ��̼�")]
    private BlacksmithAnimation anim;

    [Header("����Ʈ")]
    [SerializeField] private GameObject chanceSuccess;

    [Header("����")]
    [SerializeField] private EnforceManager enforceManager;

    private float successRangeMin;
    private float successRangeMax;
    private bool isMovingRight = true;  // �̹����� ���������� �̵� ������ ����
    private bool isEnforcing = false; //��ȭ������ ����
    private float currentTime;  // ���� �ð�
    private bool isTimeUp = true; //�ð��� �ʰ��Ǿ����� ����

    void Start()
    {
        anim = GetComponent<BlacksmithAnimation>();

        SetRandomSuccessRange();  // ���� �� ���� ���� ���� ����
    }

    void Update()
    {
        if (isEnforcing)
        {
            return;
        }

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

            if (currentX >= successRangeMin && currentX <= successRangeMax)
            {
                chanceSuccess.SetActive(false);

                Vector2 effectPosition = chanceSuccess.GetComponent<RectTransform>().anchoredPosition;
                effectPosition.x = currentX;

                chanceSuccess.GetComponent<RectTransform>().anchoredPosition = effectPosition;

                chanceSuccess.SetActive(true);
            }

            anim.EnforceAnim();

            isEnforcing = true; // ��ȭ�� ���·� ����
            isTimeUp = true;  // Ÿ�̸� ���� ���·� ����

            Invoke("HideGauge", 0.5f);
        }

        // ��ȭ �ð� �ʰ���
        if (currentTime <= 0)
        {
            if (!isTimeUp)
            {
                Debug.Log("Time's up! Fail...");
                anim.EnforceAnim();

                gauge.SetActive(false);
                isEnforcing = true; // ��ȭ�� ���·� ����
                isTimeUp = true;  // Ÿ�̸� ���� ���·� ����
            }
        }
    }

    void HideGauge()
    {
        gauge.SetActive(false);
    }

    public void ExecuteEnforce()
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

        isEnforcing = false; // ��ȭ�� ���� ���
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
        float rangeWidth = Random.Range(chanceMin, chanceMax);  // ���� ������ �ʺ� ����
        successRangeMin = Random.Range(minX, maxX - rangeWidth);  // ���� ������ �ּ� X�� ����
        successRangeMax = successRangeMin + rangeWidth;  // ���� ������ �ִ� X�� ����

        // successRangeMax�� maxX�� �ʰ����� �ʵ��� ����
        if (successRangeMax > maxX)
        {
            successRangeMax = maxX;
            successRangeMin = successRangeMax - rangeWidth;  // successRangeMin �缳��
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

    private void OnDrawGizmos()
    {
        // Gizmo ���� ����
        Gizmos.color = Color.yellow;

        // ���� ������ �����Ǿ� ���� ���� ǥ��
        if (successRangeMin < successRangeMax)
        {
            // ���� ������ �ּ� X�� �ִ� X�� ����Ͽ� ������ �׸���
            Vector3 minPoint = new Vector3(successRangeMin, 0, 0);
            Vector3 maxPoint = new Vector3(successRangeMax, 0, 0);

            // ���� ������ ��ܰ� �ϴ� ���� �׸���
            Gizmos.DrawLine(new Vector3(minPoint.x, -1, minPoint.z), new Vector3(maxPoint.x, -1, minPoint.z)); // �ϴ� ��
            Gizmos.DrawLine(new Vector3(minPoint.x, 1, minPoint.z), new Vector3(maxPoint.x, 1, minPoint.z));   // ��� ��

            // ���� ������ ���ʰ� ������ ���� �׸���
            Gizmos.DrawLine(new Vector3(minPoint.x, -1, minPoint.z), new Vector3(minPoint.x, 1, minPoint.z));   // ���� ��
            Gizmos.DrawLine(new Vector3(maxPoint.x, -1, minPoint.z), new Vector3(maxPoint.x, 1, minPoint.z));   // ������ ��
        }
    }
}
