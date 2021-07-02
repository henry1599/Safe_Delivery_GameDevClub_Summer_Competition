using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineShake : MonoBehaviour
{
    public static CinemachineShake instance;
    private CinemachineVirtualCamera cinemachineVirtualCamera;
    private float shakeTime;

    private void Awake()
    {
        instance = this;
        cinemachineVirtualCamera = transform.GetComponent<CinemachineVirtualCamera>();
    }

    public void Shake(float intensity, float time)
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = cinemachineVirtualCamera.GetComponent<CinemachineBasicMultiChannelPerlin>();
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
        cinemachineBasicMultiChannelPerlin.m_FrequencyGain = time;
        shakeTime = time;
    }

    private void Update()
    {
        if (shakeTime > 0)
        {
            shakeTime -= Time.deltaTime;
            if (shakeTime <= 0)
            {
                CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = cinemachineVirtualCamera.GetComponent<CinemachineBasicMultiChannelPerlin>();
                cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0;
            }
        }
    }
}
