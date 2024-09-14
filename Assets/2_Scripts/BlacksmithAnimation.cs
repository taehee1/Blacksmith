using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlacksmithAnimation : MonoBehaviour
{
    public static BlacksmithAnimation instance;

    private Animator animator;

    private void Awake()
    {
        instance = this;

        animator = GetComponent<Animator>();
    }

    public void EnforceAnim()
    {
        animator.SetTrigger("Enforce");
    }
}
