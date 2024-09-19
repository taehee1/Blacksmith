using Michsky.UI.MTP;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnforceManager : MonoBehaviour
{
    #region �̱���
    public static EnforceManager instance;

    private void Init()
    {
        instance = this;
    }
    #endregion

    public WeaponManager weaponManager;
    public GameObject successResult;
    public GameObject failResult;

    private void Awake()
    {
        Init();
    }

    public void Enforce(bool isSuccess)
    {
        float starCatchChance = 1;

        if (isSuccess) starCatchChance = 1.05f;
        else starCatchChance = 1f;

        Debug.Log("��ȭȮ��:" + starCatchChance * weaponManager.nextWeaponChance);

        float enforceChance = Random.Range(0f, 100f);

        if (enforceChance < weaponManager.nextWeaponChance * starCatchChance)
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
        weaponManager.weaponIndex++;
        weaponManager.ChangeWeapon();
        StarCatchGauge.instance.isEnforcing = false;
    }

    public void Fail()
    {
        weaponManager.weaponIndex--;
        weaponManager.ChangeWeapon();
        StarCatchGauge.instance.isEnforcing = false;
    }
}
