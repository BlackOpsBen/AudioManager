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

    public void PlaySpecifiedClip()
    {
        if (loopClip.isOn)
        {
            AudioManager.Instance.PlaySound2D(inputFieldClip.text, true);
        }
        else
        {
            AudioManager.Instance.PlaySound2D(inputFieldClip.text);
        }
    }

    public void PlaySpecifiedCue()
    {
        if (loopCue.isOn)
        {
            AudioManager.Instance.PlaySoundCue2D(inputFieldCue.text, true);
        }
        else
        {
            AudioManager.Instance.PlaySoundCue2D(inputFieldCue.text);
        }
    }
}
