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
        Debug.Log("애니메이션 완료됨");
        // 애니메이션이 끝난 후에 Enforce() 호출
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
