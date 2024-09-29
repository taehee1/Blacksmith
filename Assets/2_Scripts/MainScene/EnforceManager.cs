using Michsky.UI.MTP;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnforceManager : MonoBehaviour
{
    #region �̱���
    public static EnforceManager instance;  // �̱��� ������ ���� �ν��Ͻ� ����

    private void Init()
    {
        instance = this;  // �ν��Ͻ� �Ҵ�
    }
    #endregion

    public GameObject successResult;  // ��ȭ ���� �� ǥ�õǴ� ��� UI ������Ʈ
    public GameObject failResult;  // ��ȭ ���� �� ǥ�õǴ� ��� UI ������Ʈ

    public Toggle safeguardToggle;  // ����������(��ȣ��) Ȱ��ȭ ���θ� ��Ÿ���� ��� UI

    private void Awake()
    {
        Init();  // �ν��Ͻ� �ʱ�ȭ
    }

    // ��ȭ ���� �Լ�, ���� ���θ� �޾� ó��
    public void Enforce(bool isSuccess)
    {
        float starCatchChance = 1;  // ��Ÿĳġ ���� �� ��ȭ Ȯ�� ������

        if (isSuccess) starCatchChance = 1.05f;  // ��Ÿĳġ ���� �� ��ȭ Ȯ�� 5% ����
        else starCatchChance = 1f;  // ���� �� �⺻ Ȯ�� ����

        Debug.Log("��ȭȮ��:" + starCatchChance * WeaponManager.Instance.nextWeaponChance);  // ���� ��ȭ Ȯ�� ���

        float enforceChance = Random.Range(0f, 100f);  // 0���� 100 ������ ���� �� ����

        // ��ȭ Ȯ���� ���Ͽ� ����/���� ���� ����
        if (enforceChance < WeaponManager.Instance.nextWeaponChance * starCatchChance)
        {
            // ��ȭ ���� ó��
            successResult.SetActive(true);  // ���� UI Ȱ��ȭ
            Debug.Log("��ȭ����");  // ���� �α� ���
        }
        else
        {
            // ��ȭ ���� ó��
            failResult.SetActive(true);  // ���� UI Ȱ��ȭ
            Debug.Log("��ȭ����");  // ���� �α� ���
        }
    }

    // ��ȭ ���� �� ó���ϴ� �Լ�
    public void Success()
    {
        WeaponManager.Instance.weaponIndex++;  // ���� �ε����� �ϳ� ���� (���� �ܰ��)
        WeaponManager.Instance.ChangeWeapon();  // ���� ���� ó��
        StarCatchGauge.instance.isEnforcing = false;  // ��ȭ �� ���� ����
    }

    // ��ȭ ���� �� ó���ϴ� �Լ�
    public void Fail()
    {
        // ���������尡 ���� �ְ�, ���������� ī��Ʈ�� ���� ���� ��
        if (safeguardToggle.isOn && GameManager.Instance.safeguardCount > 0)
        {
            GameManager.Instance.safeguardCount--;  // ���������� ī��Ʈ�� �ϳ� ����
        }
        else
        {
            WeaponManager.Instance.weaponIndex--;  // ���� �ε����� �ϳ� ���� (���� �ܰ��)
            WeaponManager.Instance.ChangeWeapon();  // ���� ���� ó��
        }
        StarCatchGauge.instance.isEnforcing = false;  // ��ȭ �� ���� ����
    }
}
