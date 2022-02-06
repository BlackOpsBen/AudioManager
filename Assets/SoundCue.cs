using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Sound Cue", menuName = "Audio/Sound Cue")]
public class SoundCue : ScriptableObject
{
    public AudioClip[] clipOptions;

    [HideInInspector]
    public AudioSource[] sources;

    public bool modulate = true;
    public float pitchMin = 0.95f;
    public float pitchMax = 1.05f;
    public float volumeMin = 0.95f;
    public float volumeMax = 1.05f;

    public void Play()
    {
        if (sources.Length > 0)
        {
            int rand = UnityEngine.Random.Range(0, sources.Length);

            if (modulate)
            {
                float randPitch = UnityEngine.Random.Range(pitchMin, pitchMax);
                sources[rand].pitch = randPitch;

                float randVolume = UnityEngine.Random.Range(volumeMin, volumeMax);
                sources[rand].pitch = randVolume;
            }

            sources[rand].Play();
        }
        else
        {
            if (clipOptions.Length == 0)
            {
                Debug.LogError("SoundCue \" " + name + "\" contains no AudioClips!");
            }
            else
            {
                Debug.LogError("SoundCue \" " + name + "\" contains no AudioSources! The AudioManager may have failed to add a source.");
            }
        }
    }
}
