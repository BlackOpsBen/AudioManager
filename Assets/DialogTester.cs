using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogTester : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private TMP_InputField characterNameInputField;
    [SerializeField] private TMP_InputField soundCueInputField;
    [SerializeField] private TMP_InputField interruptModeField;

    public void OnPlayDialog()
    {
        AudioManager.Instance.PlayDialog(characterNameInputField.text, soundCueInputField.text, true, transform, int.Parse(interruptModeField.text));
    }

    private void Update()
    {
        AudioManager.Instance.PlayDialog("enemy", "EnemyHit");
    }
}
