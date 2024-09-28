using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StarCatchGauge : MonoBehaviour
{
    public GameObject gauge;  // ������ UI ������Ʈ

    public RectTransform gaugeImageRect;  // �����̴� �̹����� RectTransform (�¿� �̵��ϴ� ������)
    public RectTransform rangeIndicatorRect;  // ���� ������ ǥ���ϴ� RectTransform
    public float speed = 300f;  // ������ �̹��� �̵� �ӵ�

    [Header("������ ����")]
    public float minX = -4.3f;  // ������ �̹��� �̵� ������ �ּ� X��
    public float maxX = 4.3f;   // ������ �̹��� �̵� ������ �ִ� X��

    [Header("���� ����")]
    public float chanceRange = 4.3f;  // ���� ������ �ִ� �Ÿ�

    [Header("���� ũ��")]
    public float chanceMin = 1.5f;  // ���� ���� �ּҰ� (�������� ������)
    public float chanceMax = 1.7f;  // ���� ���� �ִ밪 (�������� ������)

    [Header("�ð�����")]
    public float timeLimit = 5f;  // ��Ÿĳġ �ð� ����
    [SerializeField] private TextMeshProUGUI timerText;  // Ÿ�̸Ӹ� ǥ���� �ؽ�Ʈ

    [Header("�ִϸ��̼�")]
    private BlacksmithAnimation anim;  // ��ȭ �ִϸ��̼��� ���� ����

    [Header("����Ʈ")]
    [SerializeField] private GameObject chanceSuccess;  // ���� �� ��Ÿ���� ����Ʈ

    [Header("����")]
    [SerializeField] private EnforceManager enforceManager;  // ��ȭ �Ŵ��� ���� (��ȭ ����/���� ó��)

    private float successRangeMin;  // ���� ������ �ּ� X��
    private float successRangeMax;  // ���� ������ �ִ� X��
    private bool isMovingRight = true;  // ������ �̹����� ���������� �̵� ������ ����
    public bool isEnforcing = false;  // ��ȭ ������ ����
    private float currentTime;  // ���� �ð�
    private bool isTimeUp = true;  // �ð��� �ʰ��Ǿ����� ����

    public static StarCatchGauge instance;  // �̱��� ������ ���� �ν��Ͻ� ����

    private void Awake()
    {
        instance = this;  // �̱��� �ν��Ͻ� �Ҵ�
    }

    void Start()
    {
        anim = GetComponent<BlacksmithAnimation>();  // �ִϸ��̼� ������Ʈ ����
        SetRandomSuccessRange();  // ���� ���� �� ������ ���� ���� ����
    }

    void Update()
    {
        if (isEnforcing)  // ��ȭ ���̸� �������� ����
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))  // �����̽��ٸ� ������ ��ȭ ����
        {
            ResetTimer();  // Ÿ�̸� �ʱ�ȭ
            SetRandomSuccessRange();  // ���ο� ���� ���� ����
            gauge.SetActive(true);  // ������ Ȱ��ȭ

            Vector2 currentPosition = gaugeImageRect.anchoredPosition;  // ������ �̹����� ���� ��ġ
            currentPosition.x = minX;  // ������ ��ġ�� �ּҰ����� ����
            gaugeImageRect.anchoredPosition = currentPosition;  // ������ ��ġ ������Ʈ
        }

        if (isTimeUp)  // �ð��� �� ������ �������� ����
        {
            return;
        }

        UpdateTimer();  // Ÿ�̸� ������Ʈ

        if (Input.GetKey(KeyCode.Space))  // �����̽��ٸ� ������ ���� ��
        {
            MoveGaugeImage();  // ������ �̹����� �̵�
        }

        if (Input.GetKeyUp(KeyCode.Space))  // �����̽��ٸ� ���� ��
        {
            float currentX = gaugeImageRect.anchoredPosition.x;  // ���� �������� X ��ġ

            if (currentX >= successRangeMin && currentX <= successRangeMax)  // ���� ���� ���� ���� ���
            {
                chanceSuccess.SetActive(false);  // ���� ����Ʈ ��Ȱ��ȭ

                Vector2 effectPosition = chanceSuccess.GetComponent<RectTransform>().anchoredPosition;
                effectPosition.x = currentX;  // ���� ����Ʈ ��ġ�� ���� ������ ��ġ�� ����
                chanceSuccess.GetComponent<RectTransform>().anchoredPosition = effectPosition;

                chanceSuccess.SetActive(true);  // ���� ����Ʈ Ȱ��ȭ
            }

            anim.EnforceAnim();  // ��ȭ �ִϸ��̼� ���
            isEnforcing = true;  // ��ȭ �� ���·� ����
            isTimeUp = true;  // �ð��� �ʰ��Ǿ����� ǥ��
            Invoke("HideGauge", 0.5f);  // 0.5�� �Ŀ� ������ ����
        }

        if (currentTime <= 0)  // �ð��� �� �Ǿ��� ��
        {
            if (!isTimeUp)  // ���� �ð��� �� ���� �ʾҴٸ�
            {
                Debug.Log("Time's up! Fail...");  // �ð� �ʰ��� ���� �α� ���
                anim.EnforceAnim();  // ��ȭ �ִϸ��̼� ���
                gauge.SetActive(false);  // ������ ����
                isEnforcing = true;  // ��ȭ �� ���·� ����
                isTimeUp = true;  // �ð��� �ʰ��Ǿ����� ǥ��
            }
        }
    }

    void HideGauge()  // �������� ����� �Լ�
    {
        gauge.SetActive(false);  // ������ UI ��Ȱ��ȭ
    }

    public void ExecuteEnforce()  // ��ȭ ���� �Լ�
    {
        float currentX = gaugeImageRect.anchoredPosition.x;  // ���� ������ X ��ġ

        if (currentX >= successRangeMin && currentX <= successRangeMax)  // ���� ���� ���� ���� ��
        {
            Debug.Log("Success!");  // ���� �α� ���
            enforceManager.Enforce(true);  // ��ȭ ���� ó��
        }
        else
        {
            Debug.Log("Fail...");  // ���� �α� ���
            enforceManager.Enforce(false);  // ��ȭ ���� ó��
        }
    }

    void MoveGaugeImage()  // ������ �̹����� �¿�� �̵���Ű�� �Լ�
    {
        Vector2 currentPosition = gaugeImageRect.anchoredPosition;  // ���� ������ ��ġ

        if (isMovingRight)  // ���������� �̵� ���� ��
        {
            currentPosition.x += speed * Time.deltaTime;  // ���������� �̵�

            if (currentPosition.x >= maxX)  // �ִ� ������ ������ ������ �ݴ��
            {
                currentPosition.x = maxX;
                isMovingRight = false;  // �������� �̵��ϵ��� ����
            }
        }
        else  // �������� �̵� ���� ��
        {
            currentPosition.x -= speed * Time.deltaTime;  // �������� �̵�

            if (currentPosition.x <= minX)  // �ּ� ������ ������ ������ �ٽ� ����������
            {
                currentPosition.x = minX;
                isMovingRight = true;  // ���������� �̵��ϵ��� ����
            }
        }

        gaugeImageRect.anchoredPosition = currentPosition;  // ���ŵ� ��ġ ����
    }

    void SetRandomSuccessRange()  // ���� ���� ������ �����ϴ� �Լ�
    {
        float rangeWidth = Random.Range(chanceMin, chanceMax);  // ���� ������ �ʺ� ����
        successRangeMin = Random.Range(minX, maxX - rangeWidth);  // ���� ���� �ּ� X�� ����
        successRangeMax = successRangeMin + rangeWidth;  // ���� ���� �ִ� X�� ����

        if (successRangeMax > maxX)  // ���� ������ �ִ밪�� ���� �ʵ��� ����
        {
            successRangeMax = maxX;
            successRangeMin = successRangeMax - rangeWidth;
        }

        UpdateRangeIndicator();  // ���� ������ �ð������� ������Ʈ
    }

    void UpdateRangeIndicator()  // ���� ������ �ð������� ������Ʈ�ϴ� �Լ�
    {
        Vector2 rangePosition = rangeIndicatorRect.anchoredPosition;
        rangePosition.x = (successRangeMin + successRangeMax) / 2f;  // ���� ������ �߰� ��ġ�� ����
        rangeIndicatorRect.anchoredPosition = rangePosition;

        float rangeWidth = successRangeMax - successRangeMin;
        rangeIndicatorRect.sizeDelta = new Vector2(rangeWidth, rangeIndicatorRect.sizeDelta.y);  // ���� ũ�� ������Ʈ
    }

    void UpdateTimer()  // ���� �ð��� ������Ʈ�ϴ� �Լ�
    {
        currentTime -= Time.deltaTime;  // ���� �ð� ����
        timerText.text = currentTime.ToString("F0");  // Ÿ�̸� UI ������Ʈ

        if (currentTime < 0)  // �ð��� 0���� �۾����� �ʵ���
        {
            currentTime = 0;
        }
    }

    void ResetTimer()  // Ÿ�̸Ӹ� �ʱ�ȭ�ϴ� �Լ�
    {
        currentTime = timeLimit;  // ���� �ð� �ʱ�ȭ
        isTimeUp = false;  // �ð��� �ʰ����� �ʾ����� ǥ��
    }

    private void OnDrawGizmos()  // ���� ������ �ð������� ǥ���ϴ� �Լ� (������)
    {
        Gizmos.color = Color.yellow;  // Gizmo ���� ����

        if (successRangeMin < successRangeMax)  // ���� ������ ������ ��쿡�� ǥ��
        {
            Vector3 minPoint = new Vector3(successRangeMin, 0, 0);  // ���� ���� �ּ� X�� ��ġ
            Vector3 maxPoint = new Vector3(successRangeMax, 0, 0);  // ���� ���� �ִ� X�� ��ġ

            // ���� ������ �����¿� ���� �׸��� (�ð��� �����)
            Gizmos.DrawLine(new Vector3(minPoint.x, -1, minPoint.z), new Vector3(maxPoint.x, -1, minPoint.z));  // �ϴ� ��
            Gizmos.DrawLine(new Vector3(minPoint.x, 1, minPoint.z), new Vector3(maxPoint.x, 1, minPoint.z));  // ��� ��
            Gizmos.DrawLine(new Vector3(minPoint.x, -1, minPoint.z), new Vector3(minPoint.x, 1, minPoint.z));  // ���� ��
            Gizmos.DrawLine(new Vector3(maxPoint.x, -1, minPoint.z), new Vector3(maxPoint.x, 1, minPoint.z));  // ������ ��
        }
    }
}