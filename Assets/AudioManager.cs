using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    private SoundCue[] soundCues;

    private AudioClip[] audioClips;

    private SoundClip[] soundClips;

    private void Awake()
    {
        SingletonPattern();
        LoadResources();
        CreateAudioSources(ref soundCues);
        CreateAudioSources(audioClips, ref soundClips);
    }

    private void LoadResources()
    {
        soundCues = Resources.LoadAll<SoundCue>("");

        audioClips = Resources.LoadAll<AudioClip>("");

        Debug.Log(soundCues.Length + " Sound Cues found in Resources");

        foreach (SoundCue cue in soundCues)
        {
            Debug.Log(cue.name);
        }

        Debug.Log(audioClips.Length + " AudioClips found in Resources");

        foreach (AudioClip clip in audioClips)
        {
            Debug.Log(clip.name);
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

    private void CreateAudioSources(AudioClip[] audioClips, ref SoundClip[] soundClips)
    {
        soundClips = new SoundClip[audioClips.Length];

        for (int i = 0; i < audioClips.Length; i++)
        {
            soundClips[i] = new SoundClip(audioClips[i], gameObject.AddComponent<AudioSource>());
        }
    }

    public void PlaySoundCue(string name)
    {
        SoundCue cue = Array.Find(soundCues, soundCue => soundCue.name == name);
        if (cue != null)
        {
            cue.Play();
        }
        else
        {
            Debug.LogWarning("Sound Cue \"" + name + "\" not found in Resources folder. Check spelling of name.");
        }
    }

    public void PlaySoundClip(string name)
    {
        SoundClip clip = Array.Find(soundClips, sound => sound.name == name);
        clip.source.Play();
    }
}

public class SoundClip
{
    public SoundClip(AudioClip audioClip, AudioSource audioSource)
    {
        this.source = audioSource;

        this.source.clip = audioClip;

        this.name = audioClip.name;
    }

    public string name;

    public AudioSource source;
}