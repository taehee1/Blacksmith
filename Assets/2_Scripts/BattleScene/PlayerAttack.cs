using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    // �÷��̾��� ��Ʈ ��ĵ ������Ʈ (���� ������ ����ϴ� ������Ʈ)
    public GameObject hitScan;

    // ���� �������� ��Ʈ Ƚ��
    public int damage;  // �÷��̾ ���ϴ� ������
    public int hitCount = 3;  // �� ���� �������� �ִ� ��Ʈ ��

    private void Start()
    {
        // ���� ���� ����
        SetUpState();
    }

    // ��Ʈ ��ĵ�� Ȱ��ȭ�ϴ� �Լ� (���� �ִϸ��̼� �� ȣ��)
    public void HitScanOn()
    {
        // ����� �ҽ��� ���� ���� ���� ���
        GetComponent<AudioSource>().Play();

        // ��Ʈ ��ĵ ������Ʈ Ȱ��ȭ
        hitScan.SetActive(true);

        // 0.05�� �Ŀ� ��Ʈ ��ĵ ������Ʈ�� ��Ȱ��ȭ
        Invoke("HitScanOff", 0.05f);
    }

    // ��Ʈ ��ĵ ������Ʈ�� ��Ȱ��ȭ�ϴ� �Լ�
    private void HitScanOff()
    {
        hitScan.SetActive(false);  // ��Ʈ ��ĵ ������Ʈ ��Ȱ��ȭ
    }

    // ������ ���¸� �ʱ�ȭ�ϴ� �Լ� (���� �������� ����)
    private void SetUpState()
    {
        // WeaponManager�� ������ ��� ���� �������� ������
        if (WeaponManager.Instance != null)
        {
            damage = WeaponManager.Instance.weaponDamage;  // ���� ������ ������ ����
        }
    }
}
