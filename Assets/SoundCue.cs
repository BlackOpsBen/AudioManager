using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "New Sound Cue", menuName = "Audio/Sound Cue")]
public class SoundCue : ScriptableObject
{
    public AudioClip[] clipOptions;

    public AudioMixerGroup audioMixerGroup;

    public bool modulate = true;
    public float pitchMin = 0.95f;
    public float pitchMax = 1.05f;
    public float volumeMin = 0.95f;
    public float volumeMax = 1.05f;

    public AudioClip GetRandomClip()
    {
        int rand = UnityEngine.Random.Range(0, clipOptions.Length);

        return clipOptions[rand];
    }

    public float GetPitch()
    {
        if (modulate)
        {
            return UnityEngine.Random.Range(pitchMin, pitchMax);
        }
        else
        {
            return 1.0f;
        }
    }

    public float GetVolume()
    {
        if (modulate)
        {
            return UnityEngine.Random.Range(volumeMin, volumeMax);
        }
        else
        {
            return 1.0f;
        }
    }
}
