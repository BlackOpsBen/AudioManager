using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    private Transform poolParent;

    private SoundCue[] soundCues;
    private AudioClip[] audioClips;
    //private SoundClip[] soundClips;

    //private AudioSource source2D;
    private SourcePool sourcePool2D;

    private void Awake()
    {
        SingletonPattern();
        LoadResources();
        //Create2DAudioSources(ref soundCues);
        //Create2DAudioSources(audioClips, ref soundClips);
        poolParent = new GameObject("[SOUNDS]").transform;

        //source2D = gameObject.AddComponent<AudioSource>();
        //source2D.spatialBlend = 0.0f;

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

    public void PlaySound2D(string name)
    {
        AudioClip clip = Array.Find(audioClips, sound => sound.name == name);
        AudioSource source2D = sourcePool2D.GetAudioSource();
        source2D.outputAudioMixerGroup = null;
        source2D.PlayOneShot(clip);
    }

    public void PlaySoundCue2D(string name)
    {
        SoundCue cue = Array.Find(soundCues, sound => sound.name == name);
        AudioSource source2D = sourcePool2D.GetAudioSource();
        source2D.outputAudioMixerGroup = cue.audioMixerGroup;
        source2D.pitch = cue.GetPitch();
        source2D.volume = cue.GetVolume();
        source2D.PlayOneShot(cue.GetRandomClip());
    }

    /*private void Create2DAudioSources(ref SoundCue[] cues)
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
    }*/

    /*private void Create2DAudioSources(AudioClip[] audioClips, ref SoundClip[] soundClips)
    {
        soundClips = new SoundClip[audioClips.Length];

        for (int i = 0; i < audioClips.Length; i++)
        {
            soundClips[i] = new SoundClip(audioClips[i], gameObject.AddComponent<AudioSource>());
        }
    }*/

    /*public void PlaySoundCue2D(string name)
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
    }*/

    /*public void PlaySoundClip2D(string name)
    {
        SoundClip clip = Array.Find(soundClips, sound => sound.name == name);
        clip.source.Play();
    }*/
}

/*public class SoundClip
{
    public SoundClip(AudioClip audioClip, AudioSource audioSource)
    {
        this.source = audioSource;

        this.source.clip = audioClip;

        this.name = audioClip.name;
    }

    public string name;

    public AudioSource source;
}*/

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

public class SoundPool
{
    private List<AudioSource> sources;

    private int index;

    public void PlaySoundAtPosition(AudioClip clip, Vector3 position)
    {
        bool noneAvailable = true;

        for (int i = 0; i < sources.Count; i++)
        {
            if (!sources[i].isPlaying)
            {
                noneAvailable = false;
                break;
            }
        }

        if (noneAvailable)
        {
            SpawnNewSound(clip, position);
        }
    }

    private void SpawnNewSound(AudioClip clip, Vector3 position)
    {
        GameObject newObject = new GameObject("Sound");
        newObject.transform.position = position;
        AudioSource newSource = newObject.AddComponent<AudioSource>();
        newSource.clip = clip;

        sources.Add(newSource);
    }
}