using Michsky.UI.MTP;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnforceManager : MonoBehaviour
{
    #region �̱���
    public static EnforceManager instance;

    private void Init()
    {
        instance = this;
    }
    #endregion

    public GameObject successResult;
    public GameObject failResult;

    public Toggle safeguardToggle;

    private void Awake()
    {
        Init();
    }

    public void Enforce(bool isSuccess)
    {
        float starCatchChance = 1;

        if (isSuccess) starCatchChance = 1.05f;
        else starCatchChance = 1f;

        Debug.Log("��ȭȮ��:" + starCatchChance * WeaponManager.Instance.nextWeaponChance);

        float enforceChance = Random.Range(0f, 100f);

        if (enforceChance < WeaponManager.Instance.nextWeaponChance * starCatchChance)
        {
            //Success();
            successResult.SetActive(true);
            Debug.Log("��ȭ����");
        }
        else
        {
            //Fail();
            failResult.SetActive(true);
            Debug.Log("��ȭ����");
        }
    }

    public void Success()
    {
        WeaponManager.Instance.weaponIndex++;
        WeaponManager.Instance.ChangeWeapon();
        StarCatchGauge.instance.isEnforcing = false;
    }

    public void Fail()
    {
        if (safeguardToggle.isOn && GameManager.Instance.safeguardCount > 0)
        {
            GameManager.Instance.safeguardCount--;
        }
        else
        {
            WeaponManager.Instance.weaponIndex--;
            WeaponManager.Instance.ChangeWeapon();
        }
        StarCatchGauge.instance.isEnforcing = false;
    }
}
