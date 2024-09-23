using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dummy : MonoBehaviour
{
    public DamageTextManager damageTextManager; // DamageTextManager ����
    public TextMeshProUGUI dpsText; // DPS UI �ؽ�Ʈ
    public TextMeshProUGUI totalDamageText; // �� ���ط� UI �ؽ�Ʈ

    public float totalDamage = 0;
    private float damagePerSecond = 0;
    private float damageTimer = 0;

    private void Start()
    {
        // 1�ʸ��� DPS ������Ʈ �ڷ�ƾ ����
        StartCoroutine(UpdateDPS());
    }

    public void TakeDamage(int damage)
    {
        totalDamage += damage; // �� ���ط� ����

        GameManager.Instance.coin += damage;

        // ������ �ؽ�Ʈ�� �Ʒú��� ��ġ ���� ����
        damageTextManager.ShowDamage(damage);

        // Ÿ�̸� ������Ʈ
        if (damageTimer == 0)
        {
            damageTimer = Time.time; // ù ���� �� ���� �ð����� �ʱ�ȭ
        }
    }


    private IEnumerator UpdateDPS()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f); // 1�� ���

            if (damageTimer > 0)
            {
                float elapsedTime = Time.time - damageTimer; // ��� �ð� ���
                if (elapsedTime > 0)
                {
                    damagePerSecond = totalDamage / elapsedTime; // DPS ���
                }
            }

            // UI ������Ʈ
            totalDamageText.text = "�հ�: " + totalDamage;
            dpsText.text = "�ʴ� ���ط�: " + damagePerSecond.ToString("F2");
        }
    }

    /*(1�ʵ����� ���ط� ���)
    public DamageTextManager damageTextManager; // DamageTextManager ����
    public TextMeshProUGUI dpsText; // DPS UI �ؽ�Ʈ
    public TextMeshProUGUI totalDamageText; // �� ���ط� UI �ؽ�Ʈ

    private float totalDamage = 0;
    private float currentDamageThisSecond = 0; // ���� 1�� ������ ���ط�

    private void Start()
    {
        // 1�ʸ��� DPS ������Ʈ �ڷ�ƾ ����
        StartCoroutine(UpdateDPS());
    }

    public void TakeDamage(int damage)
    {
        totalDamage += damage; // �� ���ط� ����
        currentDamageThisSecond += damage; // ���� 1�� ������ ���ط� ����

        // ������ �ؽ�Ʈ�� �Ʒú��� ��ġ�� ����
        damageTextManager.ShowDamage(damage);
    }


    private IEnumerator UpdateDPS()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f); // 1�� ���

            // ���� 1�� ������ DPS ���
            dpsText.text = "�ʴ� ���ط�: " + currentDamageThisSecond.ToString("F2");

            // ���� 1�� ������ ���ط� �ʱ�ȭ
            currentDamageThisSecond = 0;

            // UI ������Ʈ
            totalDamageText.text = "�հ�: " + totalDamage;
        }
    }
    */
}