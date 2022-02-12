using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Test3DSounds : MonoBehaviour
{
    [SerializeField] private string soundCueName;
    [SerializeField] private string audioClipName;
    [SerializeField] private string mixerGroupName;
    [SerializeField] private Transform parent;
    [SerializeField] private Toggle loopToggle;
    [SerializeField] private TMP_InputField inputFieldUniqueId;

    public void PlayAudioClip3D()
    {
        if (loopToggle.isOn)
        {
            if (mixerGroupName.Equals(string.Empty))
            {
                AudioManager.Instance.PlayAudioClip3DLooping(audioClipName, inputFieldUniqueId.text, parent);
            }
            else
            {
                AudioManager.Instance.PlayAudioClip3DLooping(audioClipName, mixerGroupName, inputFieldUniqueId.text, parent);
            }
        }
        else
        {
            if (mixerGroupName.Equals(string.Empty))
            {
                AudioManager.Instance.PlayAudioClip3D(audioClipName, parent);
            }
            else
            {
                AudioManager.Instance.PlayAudioClip3D(audioClipName, mixerGroupName, parent);
            }
        }
    }

    public void PlaySoundCue3D()
    {
        if (loopToggle.isOn)
        {
            if (mixerGroupName.Equals(string.Empty))
            {
                AudioManager.Instance.PlaySoundCue3DLooping(audioClipName, inputFieldUniqueId.text, parent);
            }
            else
            {
                AudioManager.Instance.PlaySoundCue3DLooping(audioClipName, mixerGroupName, inputFieldUniqueId.text, parent);
            }
        }
        else
        {
            if (mixerGroupName.Equals(string.Empty))
            {
                AudioManager.Instance.PlaySoundCue3D(audioClipName, parent);
            }
            else
            {
                AudioManager.Instance.PlaySoundCue3D(audioClipName, mixerGroupName, parent);
            }
        }
    }
}
