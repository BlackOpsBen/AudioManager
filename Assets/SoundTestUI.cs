using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SoundTestUI : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputFieldCue;
    [SerializeField] private TMP_InputField inputFieldClip;

    public void PlaySpecifiedCue()
    {
        AudioManager.Instance.PlaySoundCue(inputFieldCue.text);
    }

    public void PlaySpecifiedClip()
    {
        AudioManager.Instance.PlaySoundClip(inputFieldClip.text);
    }
}
