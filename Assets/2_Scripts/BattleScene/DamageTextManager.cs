using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTextManager : MonoBehaviour
{
    public GameObject damageTextPrefab; // ������ �ؽ�Ʈ ������
    public Transform damageTextParent;  // �ؽ�Ʈ�� ������ �θ�(��: Canvas)
    public float verticalOffset = 0.5f; // �ؽ�Ʈ ����
    public int maxDamageTexts = 8;      // �ؽ�Ʈ �ִ� ����
    public Vector2 slightOffset = new Vector2(0.1f, 0.1f); // X, Y offset�� �۰� �༭ ��ü�� �߰�
    private List<GameObject> activeDamageTexts = new List<GameObject>();

    public void ShowDamage(int damage, Vector3 position)
    {
        // �⺻ ���� ��ġ (�Ʒú� ��)
        Vector3 spawnPosition = position + new Vector3(0, 2f, 0);

        // �ؽ�Ʈ�� 8�� �̻� �׿��� ��
        if (activeDamageTexts.Count >= maxDamageTexts)
        {
            // ���� ������ �ؽ�Ʈ ����
            GameObject oldText = activeDamageTexts[0];
            activeDamageTexts.RemoveAt(0);
            Destroy(oldText);
        }

        // ���ο� �ؽ�Ʈ�� ���� ��ġ ���
        if (activeDamageTexts.Count > 0)
        {
            // ���� �ؽ�Ʈ ���� �� �ؽ�Ʈ ����
            GameObject lastText = activeDamageTexts[activeDamageTexts.Count - 1];
            spawnPosition = lastText.transform.position + new Vector3(0, verticalOffset, 0);
        }

        // ��ü�� �߰�
        spawnPosition += new Vector3(Random.Range(-slightOffset.x, slightOffset.x), Random.Range(-slightOffset.y, slightOffset.y), 0);

        // ������ �ؽ�Ʈ ����
        GameObject damageTextInstance = Instantiate(damageTextPrefab, spawnPosition, Quaternion.identity, damageTextParent);
        damageTextInstance.GetComponent<DamagePopup>().Setup(damage);

        // ����Ʈ�� �� �ؽ�Ʈ �߰�
        activeDamageTexts.Add(damageTextInstance);

        // ���� �ð��� ������ ����Ʈ���� ����
        StartCoroutine(RemoveTextAfterTime(damageTextInstance, 1.5f)); // 1.5�� �� ����
    }

    private IEnumerator RemoveTextAfterTime(GameObject damageText, float time)
    {
        yield return new WaitForSeconds(time);

        // �ؽ�Ʈ ���� �� ����Ʈ���� ����
        if (activeDamageTexts.Contains(damageText))
        {
            activeDamageTexts.Remove(damageText);
            Destroy(damageText);
        }
    }
}
