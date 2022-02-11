using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test3DSounds : MonoBehaviour
{
    [SerializeField] private string soundCueName;
    [SerializeField] private string audioClipName;
    [SerializeField] private string mixerGroupName;
    [SerializeField] private Transform parent;

    public void PlayAudioClip3D()
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

    public void PlaySoundCue3D()
    {
        
    }
}
