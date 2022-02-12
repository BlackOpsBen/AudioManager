using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ProcessSource : MonoBehaviour
{
    AudioClip[] audioClips;
    SoundCue[] soundCues;
    AudioMixer[] audioMixers;
    SourcePool sourcePool2D;
    SourcePool sourcePool3D;
    Dictionary<string, AudioSource> loopInstances = new Dictionary<string, AudioSource>();

    private void PlaySound(string name, bool is3D, bool isSoundCue, bool specificMixerGroup, bool isLooping, Transform parent = null, string mixerGroupName = "", string uniqueId = "")
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


    // TEMP!!!!!!!!!!!!!
    private AudioMixerGroup FindAudioMixerGroup(string mixerGroupName)
    {
        AudioMixerGroup group = audioMixers[0].outputAudioMixerGroup;
        return group;
    }

    private void StopLooping(string uniqueId)
    {

    }
}
