using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBenchmark : MonoBehaviour
{
    private int currentFrame = 0;

    private void Start()
    {
        AudioManager.Instance.PlayAudioClip3DLooping("sword", "acLoop3D", transform);
        AudioManager.Instance.PlaySoundCue3DLooping("Hit", "scLoop3D", transform);
    }

    private void Update()
    {
        if (currentFrame%10 == 0)
        {
            AudioManager.Instance.PlaySoundCue3D("sword", "SFX", transform);
        }
        currentFrame++;
        if (currentFrame == 1000)
        {
            Debug.LogError("Current frame: " + currentFrame + ", Total time: " + Time.time + ".");
        }
    }
}
