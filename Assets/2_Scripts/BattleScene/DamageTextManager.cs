using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTextManager : MonoBehaviour
{
    public GameObject damageTextPrefab;     // ������ �ؽ�Ʈ ������
    public Transform damageTextParent;      // �ؽ�Ʈ�� ������ �θ� (Canvas)
    public float randomOffsetRange = 0.5f;  // ���� ��ġ ������ ����

    public void ShowDamage(int damage)
    {
        // ���� ������ ����
        Vector3 randomOffset = new Vector3(
            Random.Range(-randomOffsetRange, randomOffsetRange),
            Random.Range(-randomOffsetRange, randomOffsetRange), // Y���� ����
            0
        );

        // �ؽ�Ʈ ���� ��ġ�� damageTextParent�� ��ġ�� ���� �������� �߰�
        Vector3 spawnPosition = damageTextParent.position + randomOffset;

        // ������ �ؽ�Ʈ ����
        GameObject damageTextInstance = Instantiate(damageTextPrefab, spawnPosition, Quaternion.identity, damageTextParent);
        damageTextInstance.GetComponent<DamagePopup>().Setup(damage, this);
    }

    // �ִϸ��̼ǿ��� ȣ���� �Լ� (�ؽ�Ʈ ����)
    public void DestroyDamageText(GameObject damageText)
    {
        Destroy(damageText);
    }
}
