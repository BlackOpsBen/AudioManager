using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    private SoundCue[] soundCues;
    private AudioClip[] audioClips;
    private AudioMixer[] audioMixers;
    private SourcePool sourcePool2D;
    private Dictionary<string, AudioSource> loopInstances = new Dictionary<string, AudioSource>();

    private SourcePool sourcePool3D;

    private void Awake()
    {
        SingletonPattern();
        LoadResources();
        sourcePool2D = new SourcePool(gameObject, true);
        sourcePool3D = new SourcePool(gameObject, false);
    }

    private void LoadResources()
    {
        soundCues = Resources.LoadAll<SoundCue>("");

        audioClips = Resources.LoadAll<AudioClip>("");

        audioMixers = Resources.LoadAll<AudioMixer>("");
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

    #region Play 2D Sounds

    #region Play 2D AudioClips

    /// <summary>
    /// Plays the named AudioClip from the Resources folder.
    /// </summary>
    public void PlayAudioClip2D(string name)
    {
        PlaySound(name, false, false, false, false);
    }

    /// <summary>
    /// Plays the named AudioClip from the Resources folder and in the specified AudioMixerGroup.
    /// </summary>
    public void PlayAudioClip2D(string name, string mixerGroupName)
    {
        PlaySound(name, false, false, true, false, mixerGroupName);
    }

    /// <summary>
    /// Plays and loops the named AudioClip from the Resources folder. Provide an arbitrary uniqueId and use that same string in StopSound2DLooping in order to stop that instance of the loop.
    /// </summary>
    public void PlayAudioClip2DLooping(string name, string uniqueId)
    {
        PlaySound(name, false, false, false, true, "", uniqueId);
    }

    /// <summary>
    /// Plays and loops the named AudioClip from the Resources folder and in the specified AudioMixerGroup. Provide an arbitrary uniqueId and use that same string in StopSound2DLooping in order to stop that instance of the loop.
    /// </summary>
    public void PlayAudioClip2DLooping(string name, string mixerGroupName, string uniqueId)
    {
        PlaySound(name, false, false, true, true, mixerGroupName, uniqueId);
    }

    #endregion

    #region Play 2D SoundCues

    /// <summary>
    /// Plays the named SoundCue from the Resources folder.
    /// </summary>
    public void PlaySoundCue2D(string name)
    {
        PlaySound(name, false, true, false, false);
    }

    /// <summary>
    /// Plays the named SoundCue from the Resources folder and in the specified AudioMixerGroup.
    /// </summary>
    public void PlaySoundCue2D(string name, string mixerGroupName)
    {
        PlaySound(name, false, true, true, false, mixerGroupName, "");
    }

    /// <summary>
    /// Plays and loops the named SoundCue from the Resources folder. Provide an arbitrary uniqueId and use that same string in StopSound2DLooping in order to stop that instance of the loop.
    /// </summary>
    public void PlaySoundCue2DLooping(string name, string uniqueId)
    {
        PlaySound(name, false, true, false, true, "", uniqueId);
    }

    /// <summary>
    /// Plays and loops the named SoundCue from the Resources folder and in the specified AudioMixerGroup. Provide an arbitrary uniqueId and use that same string in StopSound2DLooping in order to stop that instance of the loop.
    /// </summary>
    public void PlaySoundCue2DLooping(string name, string mixerGroupName, string uniqueId)
    {
        PlaySound(name, false, true, true, true, mixerGroupName, uniqueId);
    }
    #endregion

    #endregion

    #region Play 3D Sounds

    #region Play 3D AudioClips

    /// <summary>
    /// Plays the named AudioClip from the Resources folder as a 3D sound attached to the provided Transform.
    /// </summary>
    public void PlayAudioClip3D(string name, Transform parent)
    {
        PlaySound(name, true, false, false, false, "", "", parent);
    }

    /// <summary>
    /// Plays the named AudioClip from the Resources folder and in the specified AudioMixerGroup, as a 3D sound attached to the provided Transform.
    /// </summary>
    public void PlayAudioClip3D(string name, string mixerGroupName, Transform parent)
    {
        PlaySound(name, true, false, true, false, mixerGroupName, "", parent);
    }

    /// <summary>
    /// Plays and loops the named AudioClip from the Resources folder as a 3D sound attached to the provided Transform.
    /// </summary>
    public void PlayAudioClip3DLooping(string name, string uniqueId, Transform parent)
    {
        PlaySound(name, true, false, false, true, "", uniqueId, parent);
    }

    /// <summary>
    /// Plays and loops the named AudioClip from the Resources folder and in the specified AudioMixerGroup, as a 3D sound attached to the provided Transform.
    /// </summary>
    public void PlayAudioClip3DLooping(string name, string mixerGroupName, string uniqueId, Transform parent)
    {
        PlaySound(name, true, false, true, true, mixerGroupName, uniqueId, parent);
    }

    #endregion

    #region Play 3D SoundCues

    /// <summary>
    /// Plays the named SoundCue from the Resources folder as a 3D sound attached to the provided Transform.
    /// </summary>
    public void PlaySoundCue3D(string name, Transform parent)
    {
        PlaySound(name, true, true, false, false, "", "", parent);
    }

    /// <summary>
    /// Plays the named SoundCue from the Resources folder and in the specified AudioMixerGroup, as a 3D sound attached to the provided Transform.
    /// </summary>
    public void PlaySoundCue3D(string name, string mixerGroupName, Transform parent)
    {
        PlaySound(name, true, true, true, false, mixerGroupName, "", parent);
    }

    /// <summary>
    /// Plays and loops the named SoundCue from the Resources folder as a 3D sound attached to the provided Transform.
    /// </summary>
    public void PlaySoundCue3DLooping(string name, string uniqueId, Transform parent)
    {
        PlaySound(name, true, true, false, true, "", uniqueId, parent);
    }

    /// <summary>
    /// Plays and loops the named SoundCue from the Resources folder and in the specified AudioMixerGroup, as a 3D sound attached to the provided Transform.
    /// </summary>
    public void PlaySoundCue3DLooping(string name, string mixerGroupName, string uniqueId, Transform parent)
    {
        PlaySound(name, true, true, true, true, mixerGroupName, uniqueId, parent);
    }


    #endregion

    #endregion

    #region Helper Functions

    private void PlaySound(string name, bool is3D, bool isSoundCue, bool specificMixerGroup, bool isLooping, string mixerGroupName = "", string uniqueId = "", Transform parent = null)
    {
        AudioClip clip;
        SoundCue cue;
        AudioSource source;

        #region Setup Source (2D vs 3D)
        if (!is3D)
        {
            source = sourcePool2D.GetAudioSource();
        }
        else
        {
            source = sourcePool3D.GetAudioSource();
            source.transform.parent = parent;
            source.transform.localPosition = Vector3.zero;
        }
        #endregion

        #region Process and Play (AudioClip vs SoundCue)
        if (!isSoundCue)
        {
            clip = Array.Find(audioClips, sound => sound.name == name);
            source.volume = 1.0f;
            source.pitch = 1.0f;

            #region Set Clip AudioMixerGroup
            if (specificMixerGroup)
            {
                source.outputAudioMixerGroup = FindAudioMixerGroup(mixerGroupName);
            }
            else
            {
                source.outputAudioMixerGroup = null;
            }
            #endregion

            #region Play Clip (OneShot vs Loop)
            if (!isLooping)
            {
                source.PlayOneShot(clip);
            }
            else
            {
                PlayLoop(uniqueId, clip, source);
            }
            #endregion
        }
        else
        {
            cue = Array.Find(soundCues, sound => sound.name == name);
            if (cue == null)
            {
                Debug.LogWarning("No SoundCue found in Resources folder with the name, \"" + name + ".\"");
                return;
            }
            source.pitch = cue.GetPitch();
            source.volume = cue.GetVolume();

            #region Set Cue AudioMixerGroup
            if (specificMixerGroup)
            {
                AudioMixerGroup specifiedGroup = FindAudioMixerGroup(mixerGroupName);
                if (specifiedGroup == null)
                {
                    specifiedGroup = cue.GetAudioMixerGroup();
                }
                source.outputAudioMixerGroup = specifiedGroup;
            }
            else
            {
                source.outputAudioMixerGroup = cue.GetAudioMixerGroup();
            }
            #endregion

            #region Play Cue (OneShot vs Loop)
            if (!isLooping)
            {
                source.PlayOneShot(cue.GetRandomClip());
            }
            else
            {
                PlayLoop(uniqueId, cue.GetRandomClip(), source);
            }
            #endregion
        }
        #endregion
    }

    private void PlayLoop(string uniqueId, AudioClip clip, AudioSource source)
    {
        source.clip = clip;
        source.loop = true;
        source.Play();

        StopLooping(uniqueId);
        loopInstances.Add(uniqueId, source);
    }

    /// <summary>
    /// Stops the loop instance with the uniqueId.
    /// </summary>
    public void StopLooping(string uniqueId)
    {
        if (loopInstances.ContainsKey(uniqueId))
        {
            AudioSource source = loopInstances[uniqueId];
            loopInstances.Remove(uniqueId);
            source.Stop();
            source.loop = false;
            source.clip = null;
        }
    }

    /// <summary>
    /// Finds the named AudioMixerGroup from the AudioMixers from the Resources folder.
    /// </summary>
    private AudioMixerGroup FindAudioMixerGroup(string mixerGroupName)
    {
        AudioMixerGroup specifiedGroup = null;
        foreach (AudioMixer mixer in audioMixers)
        {
            AudioMixerGroup[] groups = mixer.FindMatchingGroups(mixerGroupName);

            if (groups.Length > 0)
            {
                specifiedGroup = groups[0];
                break;
            }
        }

        if (specifiedGroup == null)
        {
            Debug.LogWarning("No mixer group named \"" + mixerGroupName + "\" was found.");
        }

        return specifiedGroup;
    }

    public void RemoveDestroyed3DSound(AudioSource source)
    {
        sourcePool3D.RemoveAudioSource(source);
    }

    #endregion
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

        AudioSource newSource;
        if (is2D)
        {
            newSource = owner.AddComponent<AudioSource>();
            newSource.spatialBlend = 0.0f;
        }
        else
        {
            GameObject newGameObject = new GameObject("Sound3D", typeof(RemoveSourceOnDestroy));
            newSource = newGameObject.AddComponent<AudioSource>();
            newSource.spatialBlend = 1.0f;
        }
        sources.Add(newSource);
        return newSource;
    }

    public void RemoveAudioSource(AudioSource source)
    {
        sources.Remove(source);
    }
}

// TODO limit dialog