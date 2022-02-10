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

    public void PlaySpecifiedClip()
    {
        AudioManager.Instance.PlaySound2D(inputFieldClip.text, inputFieldMixerGroup.text);
    }

    public void PlaySpecifiedCue()
    {
        if (loopCue.isOn)
        {
            AudioManager.Instance.PlaySoundCue2D(inputFieldCue.text);
        }
        else
        {
            AudioManager.Instance.PlaySoundCue2D(inputFieldCue.text);
        }
    }
}
