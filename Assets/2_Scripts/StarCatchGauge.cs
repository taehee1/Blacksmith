using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StarCatchGauge : MonoBehaviour
{
    public GameObject gauge;

    public RectTransform gaugeImageRect;  // 움직이는 이미지의 RectTransform
    public RectTransform rangeIndicatorRect;  // 성공 범위를 표시하는 RectTransform
    public float speed = 300f;  // 이미지 이동 속도

    [Header("게이지 범위")]
    [SerializeField] private float minX = -4.3f;  // 이미지 이동 범위의 최소 X값
    [SerializeField] private float maxX = 4.3f;   // 이미지 이동 범위의 최대 X값

    [Header("찬스 범위")]
    public float chanceRange = 4.3f;

    [Header("찬스 크기")]
    public float chanceMin = 1.5f;
    public float chanceMax = 1.7f;

    [Header("시간제한")]
    public float timeLimit = 5f;  // 스타캐치 시간 제한
    public TextMeshProUGUI timerText;  // 시간 제한을 표시할 UI 텍스트

    [Header("애니메이션")]
    public Animation stopEffect;
    private Animator animator;

    [Header("참조")]
    public EnforceManager enforceManager;

    private float successRangeMin;
    private float successRangeMax;
    private bool isMovingRight = true;  // 이미지가 오른쪽으로 이동 중인지 여부
    private float currentTime;  // 남은 시간
    private bool isTimeUp = true;

    void Start()
    {
        animator = GetComponent<Animator>();

        SetRandomSuccessRange();  // 시작 시 랜덤 성공 범위 설정
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            KeyDown();
        }

        if (isTimeUp)
        {
            return;
        }

        UpdateTimer();

        if (Input.GetKey(KeyCode.Space))
        {
            StayKey();
        }

        // 스페이스바 입력 감지
        if (Input.GetKeyUp(KeyCode.Space))
        {
            KeyUp();
        }

        if (currentTime <= 0)
        {
            if (!isTimeUp)  // 타이머가 종료된 상태가 아니면 처리
            {
                Debug.Log("Time's up! Fail...");
                enforceManager.Enforce(false);
                isTimeUp = true;  // 타이머 종료 상태로 변경
            }
        }
    }

    public void KeyUp()
    {
        float currentX = gaugeImageRect.anchoredPosition.x;

        // 현재 위치가 성공 범위 내에 있는지 확인
        if (currentX >= successRangeMin && currentX <= successRangeMax)
        {
            Debug.Log("Success!");
            enforceManager.Enforce(true);
            // 성공 피드백
        }
        else
        {
            Debug.Log("Fail...");
            enforceManager.Enforce(false);
            // 실패 피드백
        }

        gauge.SetActive(false);
        isTimeUp = true;  // 타이머 종료 상태로 변경
    }

    public void StayKey()
    {
        MoveGaugeImage();  // 게이지 이미지를 왔다 갔다 이동
    }

    public void KeyDown()
    {
        ResetTimer();  // 타이머 초기화

        SetRandomSuccessRange();

        gauge.SetActive(true);

        Vector2 currentPosition = gaugeImageRect.anchoredPosition;

        currentPosition.x = minX;

        gaugeImageRect.anchoredPosition = currentPosition;
    }

    // 게이지 이미지를 좌우로 이동시키는 함수
    void MoveGaugeImage()
    {
        Vector2 currentPosition = gaugeImageRect.anchoredPosition;

        // 이미지가 오른쪽으로 이동 중일 때
        if (isMovingRight)
        {
            currentPosition.x += speed * Time.deltaTime;

            // 최대 범위를 넘으면 방향을 반대로
            if (currentPosition.x >= maxX)
            {
                currentPosition.x = maxX;
                isMovingRight = false;
            }
        }
        // 이미지가 왼쪽으로 이동 중일 때
        else
        {
            currentPosition.x -= speed * Time.deltaTime;

            // 최소 범위를 넘으면 방향을 다시 오른쪽으로
            if (currentPosition.x <= minX)
            {
                currentPosition.x = minX;
                isMovingRight = true;
            }
        }

        gaugeImageRect.anchoredPosition = currentPosition;
    }

    // 랜덤 성공 범위를 설정하는 함수
    void SetRandomSuccessRange()
    {
        // 성공 범위를 minX ~ maxX 범위 내에서 랜덤으로 설정
        float rangeWidth = Random.Range(chanceMin, chanceMax);  // 성공 범위의 너비를 설정
        successRangeMin = Random.Range(minX, maxX - rangeWidth);  // 성공 범위의 최소 X값 설정
        successRangeMax = successRangeMin + rangeWidth;  // 성공 범위의 최대 X값 설정

        // successRangeMax가 maxX를 초과하지 않도록 조정
        if (successRangeMax > maxX)
        {
            successRangeMax = maxX;
            successRangeMin = successRangeMax - rangeWidth;  // successRangeMin 재설정
        }

        // 성공 범위를 시각적으로 업데이트
        UpdateRangeIndicator();
    }

    // 성공 범위를 시각적으로 업데이트하는 함수
    void UpdateRangeIndicator()
    {
        Vector2 rangePosition = rangeIndicatorRect.anchoredPosition;
        rangePosition.x = (successRangeMin + successRangeMax) / 2f;  // 성공 범위의 중간 위치로 설정

        rangeIndicatorRect.anchoredPosition = rangePosition;

        // 성공 범위 크기를 설정
        float rangeWidth = successRangeMax - successRangeMin;
        rangeIndicatorRect.sizeDelta = new Vector2(rangeWidth, rangeIndicatorRect.sizeDelta.y);
    }

    void UpdateTimer()
    {
        currentTime -= Time.deltaTime;
        timerText.text = currentTime.ToString("F0");  // 소수점 두 자리까지 표시

        if (currentTime < 0)
        {
            currentTime = 0;
        }
    }

    // 타이머를 초기화하는 함수
    void ResetTimer()
    {
        currentTime = timeLimit;  // 시간 제한을 초기화
        isTimeUp = false;
    }

    private void OnDrawGizmos()
    {
        // Gizmo 색상 설정
        Gizmos.color = Color.yellow;

        // 성공 범위가 설정되어 있을 때만 표시
        if (successRangeMin < successRangeMax)
        {
            // 성공 범위의 최소 X와 최대 X를 사용하여 범위를 그리기
            Vector3 minPoint = new Vector3(successRangeMin, 0, 0);
            Vector3 maxPoint = new Vector3(successRangeMax, 0, 0);

            // 성공 범위의 상단과 하단 선을 그리기
            Gizmos.DrawLine(new Vector3(minPoint.x, -1, minPoint.z), new Vector3(maxPoint.x, -1, minPoint.z)); // 하단 선
            Gizmos.DrawLine(new Vector3(minPoint.x, 1, minPoint.z), new Vector3(maxPoint.x, 1, minPoint.z));   // 상단 선

            // 성공 범위의 왼쪽과 오른쪽 선을 그리기
            Gizmos.DrawLine(new Vector3(minPoint.x, -1, minPoint.z), new Vector3(minPoint.x, 1, minPoint.z));   // 왼쪽 선
            Gizmos.DrawLine(new Vector3(maxPoint.x, -1, minPoint.z), new Vector3(maxPoint.x, 1, minPoint.z));   // 오른쪽 선
        }
    }
}
