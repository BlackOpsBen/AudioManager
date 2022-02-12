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
        AudioClip clip = Array.Find(audioClips, sound => sound.name == name);
        AudioSource source2D = sourcePool2D.GetAudioSource();
        source2D.outputAudioMixerGroup = null;
        source2D.volume = 1.0f;
        source2D.pitch = 1.0f;
        source2D.PlayOneShot(clip);
    }

    /// <summary>
    /// Plays the named AudioClip from the Resources folder and in the specified AudioMixerGroup.
    /// </summary>
    public void PlayAudioClip2D(string name, string mixerGroupName)
    {
        AudioClip clip = Array.Find(audioClips, sound => sound.name == name);
        AudioSource source2D = sourcePool2D.GetAudioSource();
        source2D.outputAudioMixerGroup = GetAudioMixerGroup(mixerGroupName);
        source2D.volume = 1.0f;
        source2D.pitch = 1.0f;
        source2D.PlayOneShot(clip);
    }

    /// <summary>
    /// Plays and loops the named AudioClip from the Resources folder. Provide an arbitrary uniqueId and use that same string in StopSound2DLooping in order to stop that instance of the loop.
    /// </summary>
    public void PlayAudioClip2DLooping(string name, string uniqueId)
    {
        AudioClip clip = Array.Find(audioClips, sound => sound.name == name);
        AudioSource source2D = sourcePool2D.GetAudioSource();
        source2D.outputAudioMixerGroup = null;
        source2D.volume = 1.0f;
        source2D.pitch = 1.0f;
        source2D.clip = clip;
        source2D.loop = true;
        source2D.Play();

        Stop2DLooping(uniqueId);
        loopInstances.Add(uniqueId, source2D);
    }

    /// <summary>
    /// Plays and loops the named AudioClip from the Resources folder and in the specified AudioMixerGroup. Provide an arbitrary uniqueId and use that same string in StopSound2DLooping in order to stop that instance of the loop.
    /// </summary>
    public void PlayAudioClip2DLooping(string name, string mixerGroupName, string uniqueId)
    {
        AudioClip clip = Array.Find(audioClips, sound => sound.name == name);
        AudioSource source2D = sourcePool2D.GetAudioSource();

        source2D.outputAudioMixerGroup = GetAudioMixerGroup(mixerGroupName);
        source2D.volume = 1.0f;
        source2D.pitch = 1.0f;
        source2D.clip = clip;
        source2D.loop = true;
        source2D.Play();

        Stop2DLooping(uniqueId);
        loopInstances.Add(uniqueId, source2D);
    }

    #endregion

    #region Play 2D SoundCues
    /// <summary>
    /// Plays the named SoundCue from the Resources folder.
    /// </summary>
    public void PlaySoundCue2D(string name)
    {
        SoundCue cue = Array.Find(soundCues, sound => sound.name == name);
        if (cue == null)
        {
            Debug.LogWarning("No SoundCue found in Resources folder with the name, \"" + name + ".\"");
            return;
        }
        AudioSource source2D = sourcePool2D.GetAudioSource();
        source2D.outputAudioMixerGroup = cue.audioMixerGroup;
        source2D.pitch = cue.GetPitch();
        source2D.volume = cue.GetVolume();
        source2D.PlayOneShot(cue.GetRandomClip());
    }

    /// <summary>
    /// Plays the named SoundCue from the Resources folder and in the specified AudioMixerGroup.
    /// </summary>
    public void PlaySoundCue2D(string name, string mixerGroupName)
    {
        SoundCue cue = Array.Find(soundCues, sound => sound.name == name);
        if (cue == null)
        {
            Debug.LogWarning("No SoundCue found in Resources folder with the name, \"" + name + ".\"");
            return;
        }
        AudioSource source2D = sourcePool2D.GetAudioSource();
        source2D.pitch = cue.GetPitch();
        source2D.volume = cue.GetVolume();

        AudioMixerGroup specifiedGroup = GetAudioMixerGroup(mixerGroupName);
        if (specifiedGroup == null)
        {
            specifiedGroup = cue.audioMixerGroup;
        }

        source2D.outputAudioMixerGroup = specifiedGroup;

        source2D.PlayOneShot(cue.GetRandomClip());
    }

    /// <summary>
    /// Plays and loops the named SoundCue from the Resources folder. Provide an arbitrary uniqueId and use that same string in StopSound2DLooping in order to stop that instance of the loop.
    /// </summary>
    public void PlaySoundCue2DLooping(string name, string uniqueId)
    {
        SoundCue cue = Array.Find(soundCues, sound => sound.name == name);
        if (cue == null)
        {
            Debug.LogWarning("No SoundCue found in Resources folder with the name, \"" + name + "\"");
            return;
        }
        AudioSource source2D = sourcePool2D.GetAudioSource();
        source2D.outputAudioMixerGroup = cue.audioMixerGroup;
        source2D.pitch = cue.GetPitch();
        source2D.volume = cue.GetVolume();
        source2D.clip = cue.GetRandomClip();
        source2D.loop = true;
        source2D.Play();

        Stop2DLooping(uniqueId);
        loopInstances.Add(uniqueId, source2D);
    }

    /// <summary>
    /// Plays and loops the named SoundCue from the Resources folder and in the specified AudioMixerGroup. Provide an arbitrary uniqueId and use that same string in StopSound2DLooping in order to stop that instance of the loop.
    /// </summary>
    public void PlaySoundCue2DLooping(string name, string mixerGroupName, string uniqueId)
    {
        SoundCue cue = Array.Find(soundCues, sound => sound.name == name);
        if (cue == null)
        {
            Debug.LogWarning("No SoundCue found in Resources folder with the name, \"" + name + "\"");
            return;
        }
        AudioSource source2D = sourcePool2D.GetAudioSource();
        source2D.pitch = cue.GetPitch();
        source2D.volume = cue.GetVolume();
        source2D.clip = cue.GetRandomClip();
        source2D.loop = true;

        AudioMixerGroup specifiedGroup = GetAudioMixerGroup(mixerGroupName);
        if (specifiedGroup == null)
        {
            specifiedGroup = cue.audioMixerGroup;
        }

        source2D.outputAudioMixerGroup = specifiedGroup;
        source2D.Play();

        Stop2DLooping(uniqueId);
        loopInstances.Add(uniqueId, source2D);
    }
    #endregion

    /// <summary>
    /// Stops the loop instance with the uniqueId.
    /// </summary>
    public void Stop2DLooping(string uniqueId)
    {
        if (loopInstances.ContainsKey(uniqueId))
        {
            AudioSource source2D = loopInstances[uniqueId];
            loopInstances.Remove(uniqueId);
            source2D.Stop();
            source2D.loop = false;
            source2D.clip = null;
        }
    }

    #endregion

    #region Play 3D Sounds

    /// <summary>
    /// Plays the named AudioClip from the Resources folder as a 3D sound attached to the provided Transform.
    /// </summary>
    public void PlayAudioClip3D(string name, Transform parent)
    {
        AudioClip clip = Array.Find(audioClips, sound => sound.name == name);
        AudioSource source3D = sourcePool3D.GetAudioSource();
        source3D.transform.parent = parent;
        source3D.transform.localPosition = Vector3.zero;
        source3D.outputAudioMixerGroup = null;
        source3D.volume = 1.0f;
        source3D.pitch = 1.0f;
        source3D.PlayOneShot(clip);
    }

    /// <summary>
    /// Plays the named AudioClip from the Resources folder and in the specified AudioMixerGroup, as a 3D sound attached to the provided Transform.
    /// </summary>
    public void PlayAudioClip3D(string name, string mixerGroupName, Transform parent)
    {
        AudioClip clip = Array.Find(audioClips, sound => sound.name == name);
        AudioSource source3D = sourcePool3D.GetAudioSource();
        source3D.transform.parent = parent;
        source3D.transform.localPosition = Vector3.zero;
        source3D.outputAudioMixerGroup = GetAudioMixerGroup(mixerGroupName);
        source3D.volume = 1.0f;
        source3D.pitch = 1.0f;
        source3D.PlayOneShot(clip);
    }

    public void PlayAudioClip3DLooping(string name, string uniqueId, Transform parent)
    {
        AudioClip clip = Array.Find(audioClips, sound => sound.name == name);
        AudioSource source3D = sourcePool3D.GetAudioSource();
        source3D.transform.parent = parent;
        source3D.transform.localPosition = Vector3.zero;
        source3D.outputAudioMixerGroup = null;
        source3D.volume = 1.0f;
        source3D.pitch = 1.0f;
        source3D.clip = clip;
        source3D.loop = true;
        source3D.Play();

        Stop2DLooping(uniqueId);
        loopInstances.Add(uniqueId, source3D);
    }

    #endregion

    /// <summary>
    /// Gets the named AudioMixerGroup from the AudioMixers from the Resources folder.
    /// </summary>
    private AudioMixerGroup GetAudioMixerGroup(string mixerGroupName)
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
            GameObject newGameObject = new GameObject("Sound3D");
            newSource = newGameObject.AddComponent<AudioSource>();
            newSource.spatialBlend = 1.0f;
        }
        sources.Add(newSource);
        return newSource;
    }
}

/* TODO Prevent null ref if Sound3D parent was destroyed. Subrscibe to parent OnDestroy to unparent itself?
 * Can't attach to a transform because it would prevent being able to stop a loop attached,
 * unless the transform has OnDestroy and stops it's loop and de-childs the sound3D gameObject before killing itself.
 * StopLoop should unparent the gameobject in case the parent gets destroyed.
*/

// TODO limit dialog

// TODO add option to sequentially play all sounds in a group.