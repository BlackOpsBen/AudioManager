using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[ExecuteInEditMode]
public class DialogManager : MonoBehaviour
{
    [SerializeField] private List<DialogCharacter> characters = new List<DialogCharacter>();

    public void PlayDialog(string characterName, string category)
    {

    }

    public void PlayDialog(string characterName, string category, Transform parent)
    {

    }

    public void PlayDialog(int characterIndex, int categoryIndex)
    {
        AudioClip clip = characters[characterIndex].dialogCategories[categoryIndex].soundCue.GetRandomClip();
        

    }

    public void PlayDialog(int characterIndex, int categoryIndex, Transform parent)
    {

    }
}