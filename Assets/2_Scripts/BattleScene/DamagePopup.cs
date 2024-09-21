using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamagePopup : MonoBehaviour
{
    private DamageTextManager textManager;
    public float moveSpeed = 2f; // 텍스트가 올라가는 속도
    public float lifetime = 1.5f; // 텍스트가 사라지기까지의 시간

    public void Setup(int damage, DamageTextManager manager)
    {
        textManager = manager;

        // 텍스트 설정 (TextMeshPro 사용)
        GetComponent<TMP_Text>().text = damage.ToString();

        // 코루틴 시작
        StartCoroutine(MoveAndDestroy());
    }

    private IEnumerator MoveAndDestroy()
    {
        Vector3 startPosition = transform.position;
        Vector3 targetPosition = startPosition + new Vector3(0, 1.5f, 0); // 1.5f 만큼 위로 이동

        float elapsedTime = 0f;

        while (elapsedTime < lifetime)
        {
            // 시간에 따라 위치를 보간하여 부드럽게 이동
            transform.position = Vector3.Lerp(startPosition, targetPosition, (elapsedTime / lifetime));
            elapsedTime += Time.deltaTime;
            yield return null; // 다음 프레임까지 대기
        }
    }

    private void AnimDone()
    {
        // 텍스트 삭제
        textManager.DestroyDamageText(gameObject);
    }
}