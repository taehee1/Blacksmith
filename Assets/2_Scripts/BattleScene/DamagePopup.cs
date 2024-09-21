using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamagePopup : MonoBehaviour
{
    private DamageTextManager textManager;
    public float moveSpeed = 2f; // �ؽ�Ʈ�� �ö󰡴� �ӵ�
    public float lifetime = 1.5f; // �ؽ�Ʈ�� ������������ �ð�

    public void Setup(int damage, DamageTextManager manager)
    {
        textManager = manager;

        // �ؽ�Ʈ ���� (TextMeshPro ���)
        GetComponent<TMP_Text>().text = damage.ToString();

        // �ڷ�ƾ ����
        StartCoroutine(MoveAndDestroy());
    }

    private IEnumerator MoveAndDestroy()
    {
        Vector3 startPosition = transform.position;
        Vector3 targetPosition = startPosition + new Vector3(0, 1.5f, 0); // 1.5f ��ŭ ���� �̵�

        float elapsedTime = 0f;

        while (elapsedTime < lifetime)
        {
            // �ð��� ���� ��ġ�� �����Ͽ� �ε巴�� �̵�
            transform.position = Vector3.Lerp(startPosition, targetPosition, (elapsedTime / lifetime));
            elapsedTime += Time.deltaTime;
            yield return null; // ���� �����ӱ��� ���
        }
    }

    private void AnimDone()
    {
        // �ؽ�Ʈ ����
        textManager.DestroyDamageText(gameObject);
    }
}