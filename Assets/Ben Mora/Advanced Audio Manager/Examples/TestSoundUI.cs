using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestSoundUI : MonoBehaviour
{
    [SerializeField] private InputField nameInputField;
    [SerializeField] private Toggle toggleLoop;
    [SerializeField] private Toggle toggle3D;
    [SerializeField] private Transform parent3D;
    [SerializeField] private InputField mixerGroupInputField;
    [SerializeField] private InputField loopIdInputField;

    // Called by UI Button "Play Sound"
    public void OnPlaySound()
    {
        if (toggleLoop.isOn)
        {
            if (toggle3D.isOn)
            {
                if (mixerGroupInputField.text.Equals(string.Empty))
                {
                    AudioManager.Instance.PlaySoundLoop(nameInputField.text, parent3D, loopIdInputField.text);
                }
                else
                {
                    AudioManager.Instance.PlaySoundLoop(nameInputField.text, parent3D, loopIdInputField.text, mixerGroupInputField.text);
                }
            }
            else
            {
                if (mixerGroupInputField.text.Equals(string.Empty))
                {
                    AudioManager.Instance.PlaySoundLoop(nameInputField.text, loopIdInputField.text);
                }
                else
                {
                    AudioManager.Instance.PlaySoundLoop(nameInputField.text, loopIdInputField.text, mixerGroupInputField.text);
                }
            }
        }
        else
        {
            if (toggle3D.isOn)
            {
                if (mixerGroupInputField.text.Equals(string.Empty))
                {
                    AudioManager.Instance.PlaySound(nameInputField.text, parent3D);
                }
                else
                {
                    AudioManager.Instance.PlaySound(nameInputField.text, parent3D, mixerGroupInputField.text);
                }
            }
            else
            {
                if (mixerGroupInputField.text.Equals(string.Empty))
                {
                    AudioManager.Instance.PlaySound(nameInputField.text);
                }
                else
                {
                    AudioManager.Instance.PlaySound(nameInputField.text, mixerGroupInputField.text);
                }
            }
        }
    }

    // Called by UI Button "Stop Loop"
    public void OnStopLoop()
    {
        AudioManager.Instance.StopLooping(loopIdInputField.text);
    }
}
