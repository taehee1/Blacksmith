using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    [SerializeField] private PlayerAttack playerAttack;
    private int damage;
    private int hitCount;

    private void Start()
    {
        damage = playerAttack.damage;
        hitCount = playerAttack.hitCount;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Dummy")
        {
            for (int i = 0; i < hitCount; i++)
            {
                collision.GetComponent<Dummy>().TakeDamage(damage);
            }
        }
    }
}
