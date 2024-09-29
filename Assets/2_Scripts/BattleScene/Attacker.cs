using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    // �÷��̾��� ���� ������ ��� �ִ� PlayerAttack ��ũ��Ʈ
    [SerializeField] private PlayerAttack playerAttack;

    // ī�޶� ��鸲�� �����ϴ� CameraShake ��ũ��Ʈ
    private CameraShake cameraShake;

    // ���� �������� ��Ʈ Ƚ��
    private int damage;    // ���� �� ���ϴ� ������
    private int hitCount;  // �� ���� �������� �� �� Ÿ���� ������

    private void Start()
    {
        // CameraShake ������Ʈ�� ������
        cameraShake = GetComponent<CameraShake>();

        // PlayerAttack���κ��� �������� ��Ʈ ī��Ʈ�� ������
        damage = playerAttack.damage;
        hitCount = playerAttack.hitCount;
    }

    // ���� ������ ���� ������ �� ����Ǵ� �Լ�
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �浹�� ������Ʈ�� �±װ� "Dummy"�� ��� (Ÿ�� ������ ��)
        if (collision.tag == "Dummy")
        {
            // ī�޶� ��鸲 ȿ�� ����
            cameraShake.ShakeCamera();

            // hitCount Ƚ����ŭ �������� ����
            for (int i = 0; i < hitCount; i++)
            {
                // ����(Dummy) ������Ʈ�� �������� ���ϴ� �Լ� ȣ��
                collision.GetComponent<Dummy>().TakeDamage(damage);
            }
        }
    }
}
