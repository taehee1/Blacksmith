using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTextManager : MonoBehaviour
{
    public GameObject damageTextPrefab; // 데미지 텍스트 프리팹
    public Transform damageTextParent;  // 텍스트가 생성될 부모(예: Canvas)
    public float verticalOffset = 0.5f; // 텍스트 간격
    public int maxDamageTexts = 8;      // 텍스트 최대 개수
    public Vector2 slightOffset = new Vector2(0.1f, 0.1f); // X, Y offset을 작게 줘서 입체감 추가
    private List<GameObject> activeDamageTexts = new List<GameObject>();

    public void ShowDamage(int damage, Vector3 position)
    {
        // 기본 생성 위치 (훈련봇 위)
        Vector3 spawnPosition = position + new Vector3(0, 2f, 0);

        // 텍스트가 8개 이상 쌓였을 때
        if (activeDamageTexts.Count >= maxDamageTexts)
        {
            // 가장 오래된 텍스트 삭제
            GameObject oldText = activeDamageTexts[0];
            activeDamageTexts.RemoveAt(0);
            Destroy(oldText);
        }

        // 새로운 텍스트의 생성 위치 계산
        if (activeDamageTexts.Count > 0)
        {
            // 기존 텍스트 위에 새 텍스트 생성
            GameObject lastText = activeDamageTexts[activeDamageTexts.Count - 1];
            spawnPosition = lastText.transform.position + new Vector3(0, verticalOffset, 0);
        }

        // 입체감 추가
        spawnPosition += new Vector3(Random.Range(-slightOffset.x, slightOffset.x), Random.Range(-slightOffset.y, slightOffset.y), 0);

        // 데미지 텍스트 생성
        GameObject damageTextInstance = Instantiate(damageTextPrefab, spawnPosition, Quaternion.identity, damageTextParent);
        damageTextInstance.GetComponent<DamagePopup>().Setup(damage);

        // 리스트에 새 텍스트 추가
        activeDamageTexts.Add(damageTextInstance);

        // 일정 시간이 지나면 리스트에서 제거
        StartCoroutine(RemoveTextAfterTime(damageTextInstance, 1.5f)); // 1.5초 후 제거
    }

    private IEnumerator RemoveTextAfterTime(GameObject damageText, float time)
    {
        yield return new WaitForSeconds(time);

        // 텍스트 제거 및 리스트에서 제거
        if (activeDamageTexts.Contains(damageText))
        {
            activeDamageTexts.Remove(damageText);
            Destroy(damageText);
        }
    }
}
