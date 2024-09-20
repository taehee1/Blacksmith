using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject hitScan;

    public int damage = 10;
    public int hitCount = 3;

    public void HitScanOn()
    {
        hitScan.SetActive(true);
        Invoke("HitScanOff", 0.05f);
    }

    private void HitScanOff()
    {
        hitScan.SetActive(false);
    }
}
