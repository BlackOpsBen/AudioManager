using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Sound Cue", menuName = "Audio/Sound Cue")]
public class SoundCue : ScriptableObject
{
    public AudioClip[] clipOptions;

    public AudioSource[] sources;
}
