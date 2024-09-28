using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StarCatchGauge : MonoBehaviour
{
    public GameObject gauge;  // 게이지 UI 오브젝트

    public RectTransform gaugeImageRect;  // 움직이는 이미지의 RectTransform (좌우 이동하는 게이지)
    public RectTransform rangeIndicatorRect;  // 성공 범위를 표시하는 RectTransform
    public float speed = 300f;  // 게이지 이미지 이동 속도

    [Header("게이지 범위")]
    public float minX = -4.3f;  // 게이지 이미지 이동 범위의 최소 X값
    public float maxX = 4.3f;   // 게이지 이미지 이동 범위의 최대 X값

    [Header("찬스 범위")]
    public float chanceRange = 4.3f;  // 성공 범위의 최대 거리

    [Header("찬스 크기")]
    public float chanceMin = 1.5f;  // 성공 범위 최소값 (랜덤으로 설정됨)
    public float chanceMax = 1.7f;  // 성공 범위 최대값 (랜덤으로 설정됨)

    [Header("시간제한")]
    public float timeLimit = 5f;  // 스타캐치 시간 제한
    [SerializeField] private TextMeshProUGUI timerText;  // 타이머를 표시할 텍스트

    [Header("애니메이션")]
    private BlacksmithAnimation anim;  // 강화 애니메이션을 위한 참조

    [Header("이펙트")]
    [SerializeField] private GameObject chanceSuccess;  // 성공 시 나타나는 이펙트

    [Header("참조")]
    [SerializeField] private EnforceManager enforceManager;  // 강화 매니저 참조 (강화 성공/실패 처리)

    private float successRangeMin;  // 성공 범위의 최소 X값
    private float successRangeMax;  // 성공 범위의 최대 X값
    private bool isMovingRight = true;  // 게이지 이미지가 오른쪽으로 이동 중인지 여부
    public bool isEnforcing = false;  // 강화 중인지 여부
    private float currentTime;  // 남은 시간
    private bool isTimeUp = true;  // 시간이 초과되었는지 여부

    public static StarCatchGauge instance;  // 싱글톤 패턴을 위한 인스턴스 변수

    private void Awake()
    {
        instance = this;  // 싱글톤 인스턴스 할당
    }

    void Start()
    {
        anim = GetComponent<BlacksmithAnimation>();  // 애니메이션 컴포넌트 참조
        SetRandomSuccessRange();  // 게임 시작 시 랜덤한 성공 범위 설정
    }

    void Update()
    {
        if (isEnforcing)  // 강화 중이면 동작하지 않음
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))  // 스페이스바를 누르면 강화 시작
        {
            ResetTimer();  // 타이머 초기화
            SetRandomSuccessRange();  // 새로운 성공 범위 설정
            gauge.SetActive(true);  // 게이지 활성화

            Vector2 currentPosition = gaugeImageRect.anchoredPosition;  // 게이지 이미지의 현재 위치
            currentPosition.x = minX;  // 게이지 위치를 최소값으로 설정
            gaugeImageRect.anchoredPosition = currentPosition;  // 게이지 위치 업데이트
        }

        if (isTimeUp)  // 시간이 다 됐으면 동작하지 않음
        {
            return;
        }

        UpdateTimer();  // 타이머 업데이트

        if (Input.GetKey(KeyCode.Space))  // 스페이스바를 누르고 있을 때
        {
            MoveGaugeImage();  // 게이지 이미지를 이동
        }

        if (Input.GetKeyUp(KeyCode.Space))  // 스페이스바를 뗐을 때
        {
            float currentX = gaugeImageRect.anchoredPosition.x;  // 현재 게이지의 X 위치

            if (currentX >= successRangeMin && currentX <= successRangeMax)  // 성공 범위 내에 있을 경우
            {
                chanceSuccess.SetActive(false);  // 성공 이펙트 비활성화

                Vector2 effectPosition = chanceSuccess.GetComponent<RectTransform>().anchoredPosition;
                effectPosition.x = currentX;  // 성공 이펙트 위치를 현재 게이지 위치로 설정
                chanceSuccess.GetComponent<RectTransform>().anchoredPosition = effectPosition;

                chanceSuccess.SetActive(true);  // 성공 이펙트 활성화
            }

            anim.EnforceAnim();  // 강화 애니메이션 재생
            isEnforcing = true;  // 강화 중 상태로 설정
            isTimeUp = true;  // 시간이 초과되었음을 표시
            Invoke("HideGauge", 0.5f);  // 0.5초 후에 게이지 숨김
        }

        if (currentTime <= 0)  // 시간이 다 되었을 때
        {
            if (!isTimeUp)  // 아직 시간이 다 되지 않았다면
            {
                Debug.Log("Time's up! Fail...");  // 시간 초과로 실패 로그 출력
                anim.EnforceAnim();  // 강화 애니메이션 재생
                gauge.SetActive(false);  // 게이지 숨김
                isEnforcing = true;  // 강화 중 상태로 변경
                isTimeUp = true;  // 시간이 초과되었음을 표시
            }
        }
    }

    void HideGauge()  // 게이지를 숨기는 함수
    {
        gauge.SetActive(false);  // 게이지 UI 비활성화
    }

    public void ExecuteEnforce()  // 강화 실행 함수
    {
        float currentX = gaugeImageRect.anchoredPosition.x;  // 현재 게이지 X 위치

        if (currentX >= successRangeMin && currentX <= successRangeMax)  // 성공 범위 내에 있을 때
        {
            Debug.Log("Success!");  // 성공 로그 출력
            enforceManager.Enforce(true);  // 강화 성공 처리
        }
        else
        {
            Debug.Log("Fail...");  // 실패 로그 출력
            enforceManager.Enforce(false);  // 강화 실패 처리
        }
    }

    void MoveGaugeImage()  // 게이지 이미지를 좌우로 이동시키는 함수
    {
        Vector2 currentPosition = gaugeImageRect.anchoredPosition;  // 현재 게이지 위치

        if (isMovingRight)  // 오른쪽으로 이동 중일 때
        {
            currentPosition.x += speed * Time.deltaTime;  // 오른쪽으로 이동

            if (currentPosition.x >= maxX)  // 최대 범위를 넘으면 방향을 반대로
            {
                currentPosition.x = maxX;
                isMovingRight = false;  // 왼쪽으로 이동하도록 설정
            }
        }
        else  // 왼쪽으로 이동 중일 때
        {
            currentPosition.x -= speed * Time.deltaTime;  // 왼쪽으로 이동

            if (currentPosition.x <= minX)  // 최소 범위를 넘으면 방향을 다시 오른쪽으로
            {
                currentPosition.x = minX;
                isMovingRight = true;  // 오른쪽으로 이동하도록 설정
            }
        }

        gaugeImageRect.anchoredPosition = currentPosition;  // 갱신된 위치 적용
    }

    void SetRandomSuccessRange()  // 랜덤 성공 범위를 설정하는 함수
    {
        float rangeWidth = Random.Range(chanceMin, chanceMax);  // 성공 범위의 너비 설정
        successRangeMin = Random.Range(minX, maxX - rangeWidth);  // 성공 범위 최소 X값 설정
        successRangeMax = successRangeMin + rangeWidth;  // 성공 범위 최대 X값 설정

        if (successRangeMax > maxX)  // 성공 범위가 최대값을 넘지 않도록 조정
        {
            successRangeMax = maxX;
            successRangeMin = successRangeMax - rangeWidth;
        }

        UpdateRangeIndicator();  // 성공 범위를 시각적으로 업데이트
    }

    void UpdateRangeIndicator()  // 성공 범위를 시각적으로 업데이트하는 함수
    {
        Vector2 rangePosition = rangeIndicatorRect.anchoredPosition;
        rangePosition.x = (successRangeMin + successRangeMax) / 2f;  // 성공 범위의 중간 위치로 설정
        rangeIndicatorRect.anchoredPosition = rangePosition;

        float rangeWidth = successRangeMax - successRangeMin;
        rangeIndicatorRect.sizeDelta = new Vector2(rangeWidth, rangeIndicatorRect.sizeDelta.y);  // 범위 크기 업데이트
    }

    void UpdateTimer()  // 남은 시간을 업데이트하는 함수
    {
        currentTime -= Time.deltaTime;  // 남은 시간 감소
        timerText.text = currentTime.ToString("F0");  // 타이머 UI 업데이트

        if (currentTime < 0)  // 시간이 0보다 작아지지 않도록
        {
            currentTime = 0;
        }
    }

    void ResetTimer()  // 타이머를 초기화하는 함수
    {
        currentTime = timeLimit;  // 남은 시간 초기화
        isTimeUp = false;  // 시간이 초과되지 않았음을 표시
    }

    private void OnDrawGizmos()  // 성공 범위를 시각적으로 표시하는 함수 (디버깅용)
    {
        Gizmos.color = Color.yellow;  // Gizmo 색상 설정

        if (successRangeMin < successRangeMax)  // 성공 범위가 설정된 경우에만 표시
        {
            Vector3 minPoint = new Vector3(successRangeMin, 0, 0);  // 성공 범위 최소 X값 위치
            Vector3 maxPoint = new Vector3(successRangeMax, 0, 0);  // 성공 범위 최대 X값 위치

            // 성공 범위의 상하좌우 선을 그리기 (시각적 디버깅)
            Gizmos.DrawLine(new Vector3(minPoint.x, -1, minPoint.z), new Vector3(maxPoint.x, -1, minPoint.z));  // 하단 선
            Gizmos.DrawLine(new Vector3(minPoint.x, 1, minPoint.z), new Vector3(maxPoint.x, 1, minPoint.z));  // 상단 선
            Gizmos.DrawLine(new Vector3(minPoint.x, -1, minPoint.z), new Vector3(minPoint.x, 1, minPoint.z));  // 왼쪽 선
            Gizmos.DrawLine(new Vector3(maxPoint.x, -1, minPoint.z), new Vector3(maxPoint.x, 1, minPoint.z));  // 오른쪽 선
        }
    }
}