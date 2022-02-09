using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    private SoundCue[] soundCues;
    private AudioClip[] audioClips;
    private SourcePool sourcePool2D;

    private void Awake()
    {
        SingletonPattern();
        LoadResources();
        sourcePool2D = new SourcePool(gameObject, true);
    }

    private void LoadResources()
    {
        soundCues = Resources.LoadAll<SoundCue>("");

        audioClips = Resources.LoadAll<AudioClip>("");

        Debug.Log(soundCues.Length + " SoundCues found");
        Debug.Log(audioClips.Length + " AudioClips found");
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

    public void PlaySound2D(string name, bool looping = false)
    {
        AudioClip clip = Array.Find(audioClips, sound => sound.name == name);
        AudioSource source2D = sourcePool2D.GetAudioSource();
        source2D.outputAudioMixerGroup = null;
        source2D.loop = looping;
        source2D.PlayOneShot(clip);
    }

    public void PlaySoundCue2D(string name, bool looping = false)
    {
        SoundCue cue = Array.Find(soundCues, sound => sound.name == name);
        AudioSource source2D = sourcePool2D.GetAudioSource();
        source2D.outputAudioMixerGroup = cue.audioMixerGroup;
        source2D.loop = looping;
        source2D.pitch = cue.GetPitch();
        source2D.volume = cue.GetVolume();
        source2D.PlayOneShot(cue.GetRandomClip());
    }
}

public class SourcePool
{
    private List<AudioSource> sources = new List<AudioSource>();

    private int index = 0;

    private GameObject owner;

    private bool is2D = false;

    public SourcePool(GameObject owner, bool is2D)
    {
        this.owner = owner;
        this.is2D = is2D;
    }

    public AudioSource GetAudioSource()
    {
        for (int i = index; i < sources.Count; i++)
        {
            if (!sources[i].isPlaying)
            {
                index = i + 1;
                if (index >= sources.Count)
                {
                    index = 0;
                }
                return sources[i];
            }
        }

        AudioSource newSource = owner.AddComponent<AudioSource>();
        if (is2D)
        {
            newSource.spatialBlend = 0.0f;
        }
        else
        {
            newSource.spatialBlend = 1.0f;
        }
        sources.Add(newSource);
        return newSource;
    }
}