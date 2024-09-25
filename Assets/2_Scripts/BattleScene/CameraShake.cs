using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public CinemachineImpulseSource impulseSource;

    public void ShakeCamera()
    {
        // Impulse�� �߻���ŵ�ϴ�.
        if (impulseSource != null)
        {
            impulseSource.GenerateImpulse();
        }
    }
}
