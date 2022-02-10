using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SoundTestUI : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputFieldCue;
    [SerializeField] private TMP_InputField inputFieldClip;
    [SerializeField] private Toggle loopCue;
    [SerializeField] private Toggle loopClip;
    [SerializeField] private TMP_InputField inputFieldMixerGroup;
    [SerializeField] private TMP_InputField inputFieldUniqueId;

    public void PlaySpecifiedClip()
    {
        if (loopClip.isOn)
        {
            if (inputFieldMixerGroup.text.Equals(string.Empty))
            {
                Debug.Log("No AudioMixerGroup specified.");
                AudioManager.Instance.PlayAudioClip2DLooping(inputFieldClip.text, inputFieldUniqueId.text);
            }
            else
            {
                AudioManager.Instance.PlayAudioClip2DLooping(inputFieldClip.text, inputFieldMixerGroup.text, inputFieldUniqueId.text);
            }
        }
        else
        {
            if (inputFieldMixerGroup.text.Equals(string.Empty))
            {
                Debug.Log("No AudioMixerGroup specified.");
                AudioManager.Instance.PlayAudioClip2D(inputFieldClip.text);
            }
            else
            {
                AudioManager.Instance.PlayAudioClip2D(inputFieldClip.text, inputFieldMixerGroup.text);
            }
        }
    }

    public void PlaySpecifiedCue()
    {
        if (loopCue.isOn)
        {
            if (inputFieldMixerGroup.text.Equals(string.Empty))
            {
                Debug.Log("No AudioMixerGroup specified.");
                AudioManager.Instance.PlaySoundCue2DLooping(inputFieldCue.text, inputFieldUniqueId.text);
            }
            else
            {
                AudioManager.Instance.PlaySoundCue2DLooping(inputFieldCue.text, inputFieldMixerGroup.text, inputFieldUniqueId.text);
            }
        }
        else
        {
            if (inputFieldMixerGroup.text.Equals(string.Empty))
            {
                Debug.Log("No AudioMixerGroup specified.");
                AudioManager.Instance.PlaySoundCue2D(inputFieldCue.text);
            }
            else
            {
                AudioManager.Instance.PlaySoundCue2D(inputFieldCue.text, inputFieldMixerGroup.text);
            }
        }
    }
}
