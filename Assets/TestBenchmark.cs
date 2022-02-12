using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBenchmark : MonoBehaviour
{
    private int currentFrame = 0;

    private void Start()
    {
        AudioManager.Instance.PlayAudioClip2DLooping("repair", "scLoop3D1");
        AudioManager.Instance.PlayAudioClip2DLooping("sword", "scLoop3D2");
    }

    private void Update()
    {
        if (currentFrame%10 == 0)
        {
            AudioManager.Instance.PlayAudioClip2D("sword", "SFX");
        }
        currentFrame++;
        if (currentFrame == 10000)
        {
            Debug.LogError("Current frame: " + currentFrame + ", Total time: " + Time.time + ".");
        }
    }
}
