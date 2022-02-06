using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SoundTestUI : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;

    public void PlaySpecifiedSound()
    {
        AudioManager.Instance.PlaySoundCue(inputField.text);
    }
}
