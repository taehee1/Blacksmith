using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : MonoBehaviour
{
    public DamageTextManager damageTextManager; // DamageTextManager ����

    public void TakeDamage(int damage)
    {
        // ������ �ؽ�Ʈ�� �Ʒú��� ��ġ ���� ����
        damageTextManager.ShowDamage(damage, transform.position);

        // �Ʒú��� ü�� ���� ���� ���� �߰�
    }
}
