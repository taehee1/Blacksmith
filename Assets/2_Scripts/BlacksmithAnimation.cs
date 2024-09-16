using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlacksmithAnimation : MonoBehaviour
{
    public static BlacksmithAnimation instance;

    private StarCatchGauge starCatchGauge;

    private Animator animator;
    private AudioSource audioSource;

    private void Awake()
    {
        instance = this;

        starCatchGauge = GetComponent<StarCatchGauge>();

        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public void EnforceAnim()
    {
        animator.SetTrigger("Enforce");
    }

    public void OnAnimationComplete()
    {
        Debug.Log("�ִϸ��̼� �Ϸ��");
        // �ִϸ��̼��� ���� �Ŀ� Enforce() ȣ��
        starCatchGauge.ExecuteEnforce();
    }

    public void PlayHammerSound()
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }
}
