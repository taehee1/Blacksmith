using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public float comboDalay;

    private int currentCombo;
    private bool canNext = true;
    private bool canAttack = true;
    private float comboTimer;

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        Attack();

        comboTimer += Time.deltaTime;
        if (comboTimer > comboDalay)
        {
            canAttack = true;
        }
    }

    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Z) && canNext && canAttack)
        {
            canNext = false;

            if (currentCombo == 0)
            {
                anim.SetTrigger("Attack1");
            }
            else if (currentCombo == 1)
            {
                anim.SetTrigger("Attack2");
            }
            else if (currentCombo == 2)
            {
                anim.SetTrigger("Attack3");
                canAttack = false;
            }

            comboTimer = 0;

            currentCombo++;

            if (currentCombo > 2)
            {
                ResetCombo();
            }
        }
    }

    public void ResetCombo()
    {
        currentCombo = 0;
    }

    public void CanNext()
    {
        canNext = true;
    }
}