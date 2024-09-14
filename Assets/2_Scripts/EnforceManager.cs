using System.Collections;
using System.Collections.Generic;
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
            Success();
            Debug.Log("��ȭ����");
        }
        else
        {
            Fail();
            Debug.Log("��ȭ����");
        }
    }

    private void Success()
    {
        weaponManager.weaponIndex++;
        weaponManager.ChangeWeapon();
    }

    private void Fail()
    {
        weaponManager.weaponIndex--;
        weaponManager.ChangeWeapon();
    }
}
