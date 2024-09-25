using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public CinemachineImpulseSource impulseSource;

    public void ShakeCamera()
    {
        // Impulse를 발생시킵니다.
        if (impulseSource != null)
        {
            impulseSource.GenerateImpulse();
        }
    }
}
