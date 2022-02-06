using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    private SoundCue[] soundCues;

    private void Awake()
    {
        SingletonPattern();
        LoadResources();
        CreateAudioSources(ref soundCues);
    }

    private void LoadResources()
    {
        soundCues = Resources.LoadAll<SoundCue>("");

        Debug.Log(soundCues.Length + " Sound Cues found in Resources");

        foreach (SoundCue cue in soundCues)
        {
            Debug.Log(cue.name);
        }
    }

    private void SingletonPattern()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void CreateAudioSources(ref SoundCue[] cues)
    {
        foreach (SoundCue cue in cues)
        {
            cue.sources = new AudioSource[cue.clipOptions.Length];

            for (int i = 0; i < cue.clipOptions.Length; i++)
            {
                cue.sources[i] = gameObject.AddComponent<AudioSource>();
                cue.sources[i].clip = cue.clipOptions[i];
            }
        }
    }

    public void PlaySoundCue(string name)
    {
        SoundCue cue = Array.Find(soundCues, soundCue => soundCue.name == name);
        if (cue != null)
        {
            cue.sources[0].Play();
        }
        else
        {
            Debug.LogWarning("Sound Cue \"" + name + "\" found in Resources folder. Check spelling of name.");
        }
    }
}
