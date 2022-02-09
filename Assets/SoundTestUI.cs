using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SoundTestUI : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputFieldCue;
    [SerializeField] private TMP_InputField inputFieldClip;

    public void PlaySpecifiedClip()
    {
        AudioManager.Instance.PlaySound2D(inputFieldClip.text);
    }

    public void PlaySpecifiedCue()
    {
        AudioManager.Instance.PlaySoundCue2D(inputFieldCue.text);
    }
}
