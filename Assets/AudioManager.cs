using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    /// <summary>
    /// Use for low priority dialog that will never happen if ANY character is already talking.
    /// </summary>
    public const int INTERRUPT_OVERLAP_NONE = 0;
    /// <summary>
    /// Use for low priority dialog that will not play if the character is currently speaking, but will still play concurrently with other speaking characters.
    /// </summary>
    public const int INTERRUPT_NONE = 1;
    /// <summary>
    /// Use for medium priority dialog that will stop any previous dialog from the character, but all other characters will continue speaking.
    /// </summary>
    public const int INTERRUPT_SELF = 2;
    /// <summary>
    /// Use for high priority dialog which will cause all currently speaking characters to stop and then the new dialog will play.
    /// </summary>
    public const int INTERRUPT_ALL = 3;
    

    private SoundCue[] soundCues;
    private AudioClip[] audioClips;
    private AudioMixer[] audioMixers;
    private SourcePool sourcePool2D;
    private SourcePool sourcePool3D;
    private Dictionary<string, AudioSource> loopInstances = new Dictionary<string, AudioSource>();
    private Dictionary<string, AudioSource> charactersSpeaking = new Dictionary<string, AudioSource>();

    private void Awake()
    {
        SingletonPattern();
        LoadResources();
        sourcePool2D = new SourcePool(gameObject, true);
        sourcePool3D = new SourcePool(gameObject, false);

        DontDestroyOnLoad(gameObject);
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

    #region Play Dialog

    /// <summary>
    /// Attempt to play dialog from a specific character. Plays as a 2D sound unless is3D is set to true, in which case it is attached to the provided parent. The INTERRUPT_MODE determines whether the character can interrupt itself and/or other currently speaking characters. Uses the AudioMixerGroup of the specified SoundCue.
    /// </summary>
    public void PlayDialog(string characterName, string soundCueName, bool is3D = false, Transform parent = null, int INTERRUPT_MODE = INTERRUPT_NONE)
    {
        if (charactersSpeaking.ContainsKey(characterName) && charactersSpeaking[characterName].isPlaying)
        {
            switch (INTERRUPT_MODE)
            {
                case INTERRUPT_OVERLAP_NONE:
                    CantSpeak(characterName);
                    break;
                case INTERRUPT_NONE:
                    CantSpeak(characterName);
                    break;
                case INTERRUPT_SELF:
                    InterruptSelf(characterName, soundCueName, is3D, parent);
                    break;
                case INTERRUPT_ALL:
                    InterruptAll(characterName, soundCueName, is3D, parent);
                    break;
                default:
                    CantSpeak(characterName);
                    break;
            }

        }
        else
        {
            if (INTERRUPT_MODE == INTERRUPT_OVERLAP_NONE && GetAnyCharacterSpeaking())
            {
                CantSpeak(characterName);
            }
            else
            {
                charactersSpeaking.Remove(characterName);
                PlayAndAddCharacterDialog(characterName, soundCueName, is3D, parent);
            }
        }
    }

    /// <summary>
    /// Returns true if there is any character still speaking.
    /// </summary>
    private bool GetAnyCharacterSpeaking()
    {
        foreach (AudioSource character in charactersSpeaking.Values)
        {
            if (character.isPlaying)
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Simply logs that the character can't speak yet because of the INTERRUPT_MODE used. Does not play the new dialog.
    /// </summary>
    private void CantSpeak(string characterName)
    {
        Debug.Log(characterName + "is already speaking. Did not interrupt.");
    }

    /// <summary>
    /// Stops the currently playing dialog by the specified character before playing the new dialog.
    /// </summary>
    private void InterruptSelf(string characterName, string soundCueName, bool is3D, Transform parent)
    {
        Debug.Log(characterName + "is already speaking. Interrupting previous dialog.");
        charactersSpeaking[characterName].Stop();
        charactersSpeaking.Remove(characterName);

        charactersSpeaking.Remove(characterName);

        PlayAndAddCharacterDialog(characterName, soundCueName, is3D, parent);
    }

    /// <summary>
    /// Stops all currently speaking characters and then plays the new dialog.
    /// </summary>
    private void InterruptAll(string characterName, string soundCueName, bool is3D, Transform parent)
    {
        Debug.Log(characterName + "Interrupting ALL characters!");
        foreach (AudioSource charSource in charactersSpeaking.Values)
        {
            charSource.Stop();
        }
        charactersSpeaking.Clear();

        PlayAndAddCharacterDialog(characterName, soundCueName, is3D, parent);
    }

    /// <summary>
    /// Actually plays the dialog and saves a reference to the AudioSource in the Dictionary so it can be interrupted later if needed.
    /// </summary>
    private void PlayAndAddCharacterDialog(string characterName, string soundCueName, bool is3D, Transform parent)
    {
        AudioSource source = PlaySound(soundCueName, is3D, true, false, false, "", "", parent);
        charactersSpeaking.Add(characterName, source);
    }

    #endregion

    #region Helper Functions

    /// <summary>
    /// Processes an AudioSource from a pool to play the sound.
    /// </summary>
    private AudioSource PlaySound(string name, bool is3D, bool isSoundCue, bool specificMixerGroup, bool isLooping, string mixerGroupName = "", string uniqueId = "", Transform parent = null)
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
                return source;
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

        return source;
    }

    /// <summary>
    /// Plays the sound as a loop and saves a reference to the AudioSource in loopInstances.
    /// </summary>
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

    /// <summary>
    /// The RemoveSourceOnDestroy component of any 3D sounds played will automatically call this when their parent object is destroyed. This removes them from the list of AudioSources.
    /// </summary>
    public void RemoveDestroyed3DSound(AudioSource source)
    {
        sourcePool3D.RemoveAudioSource(source);
    }

    #endregion
}

/// <summary>
/// A pool of AudioSources.
/// </summary>
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

// TODO prevent lower priority dialog from starting after a higher priority dialog started.