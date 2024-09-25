using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject hitScan;

    public int damage;
    public int hitCount = 3;

    private void Start()
    {
        SetUpState();
    }

    public void HitScanOn()
    {
        GetComponent<AudioSource>().Play();
        hitScan.SetActive(true);
        Invoke("HitScanOff", 0.05f);
    }

    private void HitScanOff()
    {
        hitScan.SetActive(false);
    }

    private void SetUpState()
    {
        if (WeaponManager.Instance != null)
        {
            damage = WeaponManager.Instance.weaponDamage;
        }
    }
}
