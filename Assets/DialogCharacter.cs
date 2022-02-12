using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "New Dialog Character", menuName = "Audio/Dialog Character")]
public class DialogCharacter : ScriptableObject
{
    public AudioMixerGroup mixerGroup;
    public List<DialogCategory> dialogCategories = new List<DialogCategory>();
}

[System.Serializable]
public class DialogCategory
{
    public DialogCategory(string name)
    {
        this.name = name;
    }

    public string name;
    public SoundCue soundCue;
}