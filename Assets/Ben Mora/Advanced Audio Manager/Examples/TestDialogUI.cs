using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestDialogUI : MonoBehaviour
{
    [SerializeField] private Transform parent3D;
    [SerializeField] private CharacterInfo[] characterInfo;

    // This dialog will overlap with other characters, but won't speak if this character is already speaking.
    public void OnPlaySelected(int characterIndex)
    {
        AudioManager.Instance.PlayDialog(characterInfo[characterIndex].name, characterInfo[characterIndex].selectedDialog, INTERRUPT_MODE: AudioManager.INTERRUPT_OVERLAP);
    }

    // This dialog will only ever happen if nobody is already speaking.
    public void OnPlayGetKill(int characterIndex)
    {
        AudioManager.Instance.PlayDialog(characterInfo[characterIndex].name, characterInfo[characterIndex].getKillDialog, INTERRUPT_MODE: AudioManager.INTERRUPT_NONE);
    }

    // If this character is already speaking it will stop speaking and then play this dialog. Other characters will continue speaking.
    public void OnPlayDie(int characterIndex)
    {
        AudioManager.Instance.PlayDialog(characterInfo[characterIndex].name, characterInfo[characterIndex].dieDialog, INTERRUPT_MODE: AudioManager.INTERRUPT_SELF);
    }

    // This dialog will stop all characters speaking and then play the dialog, not allowing any characters to speak until it's done. A new INTERRUPT_ALL dialog will replace a previous one.
    public void OnPlayVictory()
    {
        AudioManager.Instance.PlayDialog("Commander", "Victory", INTERRUPT_MODE: AudioManager.INTERRUPT_ALL);
    }    

    [System.Serializable]
    private struct CharacterInfo
    {
        public string name;
        public string selectedDialog;
        public string getKillDialog;
        public string dieDialog;
    }
}
