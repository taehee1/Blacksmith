using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    //���� ����
    private Player player;  // Player ��ũ��Ʈ ���� (�÷��̾� ���� Ȯ�ο�)

    [Header("��ġ")]  // �ν����Ϳ��� ��ġ ���� �������� �׷�ȭ�ؼ� ǥ��
    public float comboDelay;  // �޺� ���� ������ ������ �ð�

    private int currentCombo;  // ���� �޺� ���� (1, 2, 3�� ���� �� ��� �ܰ�����)
    private bool canNext = true;  // ���� �޺��� �Է��� �� �ִ��� ����
    private bool canAttack = true;  // ������ ������ �������� ����
    private float comboTimer;  // �޺� �Է� ���� �ð� Ÿ�̸�

    private Animator anim;  // Animator ������Ʈ (�ִϸ��̼� ó��)

    private void Awake()
    {
        // ���� ���� �ʱ�ȭ
        player = GetComponent<Player>();  // Player ��ũ��Ʈ ��������
        anim = GetComponent<Animator>();  // Animator ������Ʈ ��������
    }

    private void Update()
    {
        // �÷��̾ ������ �� ���� ���¶�� ������ �������� ����
        if (!player.canMove)
        {
            return;
        }

        Attack();      // ���� ó��
        ComboDelay();  // �޺� ������ ó��
    }

    // ���� �Է� �� �޺� ó��
    private void Attack()
    {
        // Z Ű�� ������ ��, ���� �޺��� �����ϰ�, ���� ������ ���¶��
        if (Input.GetKeyDown(KeyCode.Z) && canNext && canAttack)
        {
            canNext = false;  // ���� �޺� �Է��� ��� ���� (�� ���� �ϳ��� ���ݸ� ó��)

            // �޺��� ���� �ٸ� ���� �ִϸ��̼� ����
            if (currentCombo == 0)
            {
                anim.SetTrigger("Attack1");  // ù ��° ����
            }
            else if (currentCombo == 1)
            {
                anim.SetTrigger("Attack2");  // �� ��° ����
            }
            else if (currentCombo == 2)
            {
                anim.SetTrigger("Attack3");  // �� ��° ����
                canAttack = false;  // �� ��° ���� ���Ŀ��� ��� ���� �Ұ�
            }

            comboTimer = 0;  // �޺� Ÿ�̸� �ʱ�ȭ

            currentCombo++;  // �޺� �ܰ� ����

            // �޺��� 3�� �̻� �Ǹ� �޺� �ʱ�ȭ
            if (currentCombo > 2)
            {
                ResetCombo();  // �޺� �ʱ�ȭ �Լ� ȣ��
            }
        }
    }

    // �޺� �����̸� üũ�ϴ� �Լ�
    private void ComboDelay()
    {
        comboTimer += Time.deltaTime;  // �޺� Ÿ�̸� ����

        // �޺� Ÿ�̸Ӱ� ������ �����̸� �ʰ��ϸ� �ٽ� ���� ���� ���·�
        if (comboTimer > comboDelay)
        {
            canAttack = true;  // ���� ���� ���·� ����
        }
    }

    // �޺��� �ʱ�ȭ�ϴ� �Լ� (���� ������ ó�� �ܰ��)
    public void ResetCombo()
    {
        currentCombo = 0;  // �޺� �ܰ踦 �ʱ�ȭ
    }

    // ���� ������ �����ϴٰ� �����ϴ� �Լ� (�ִϸ��̼� �̺�Ʈ�� ������ �� ����)
    public void CanNext()
    {
        canNext = true;  // ���� �޺� �Է��� ���
    }
}