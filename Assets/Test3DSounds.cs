using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test3DSounds : MonoBehaviour
{
    [SerializeField] private string soundCueName;
    [SerializeField] private string audioClipName;
    [SerializeField] private Transform parent;

    public void PlayAudioClip3D()
    {
        AudioManager.Instance.PlayAudioClip3D(audioClipName, parent);
    }

    public void PlaySoundCue3D()
    {
        
    }
}
