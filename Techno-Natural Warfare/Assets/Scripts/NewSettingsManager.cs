using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NewSettingsManager : MonoBehaviour
{
    [SerializeField] List<Button> buttons = null;
    [SerializeField] Slider volume = null;
    [SerializeField] TextMeshProUGUI valueText = null;
    bool isKeybinding = false;
    List<TextMeshProUGUI> keybindTexts = new List<TextMeshProUGUI>();

    private void Start()
    {
        buttons[0].GetComponentInChildren<TextMeshProUGUI>().text = Game.Instance.Data.Settings.Select.ToString();
        buttons[1].GetComponentInChildren<TextMeshProUGUI>().text = Game.Instance.Data.Settings.RotateClockWise.ToString();
        buttons[2].GetComponentInChildren<TextMeshProUGUI>().text = Game.Instance.Data.Settings.RotateCounterClockWise.ToString();
        for (int i = 0; i < buttons.Count; i++)
        {
            keybindTexts.Add(buttons[i].GetComponentInChildren<TextMeshProUGUI>());
        }
        volume.value = Game.Instance.Data.Settings.Volume;
        valueText.text = volume.value.ToString();
    }

    public void OnVolumeChange()
    {
        valueText.text = volume.value.ToString();
        AudioListener.volume = volume.value;
        //TODO
        //Add a brief clip of music to play to verify music is to the players liking.
    }
    public void OnKeybindClick(int buttonIndex)
    {
        //if (!isKeybinding)
        {
            isKeybinding = true;
            keybindTexts[buttonIndex].text = "<New Binding>";
            StartCoroutine("KeybindChange", keybindTexts[buttonIndex]);
        }
    }


    IEnumerator KeybindChange(Text text)
    {
        while (!Input.anyKeyDown)
        {
            yield return null;
        }

        foreach (KeyCode vKey in Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKey(vKey))
            {
                text.text = vKey.ToString();
            }
        }
        StopCoroutine("AwaitButtonPress");
        isKeybinding = false;
        yield return null;
    }

    public void CloseSetting()
    {
        Game.Instance.Data.Settings.Volume = (int)volume.value;
        Game.Instance.Data.Settings.Select = (KeyCode)Enum.Parse(typeof(KeyCode), buttons[0].GetComponentInChildren<Text>().text, true);
        Game.Instance.Data.Settings.RotateClockWise = (KeyCode)Enum.Parse(typeof(KeyCode), buttons[1].GetComponentInChildren<Text>().text, true);
        Game.Instance.Data.Settings.RotateCounterClockWise = (KeyCode)Enum.Parse(typeof(KeyCode), buttons[2].GetComponentInChildren<Text>().text, true);
        Game.Instance.SceneManagerObject.LoadSceneAsyncByName("Main Menu");
    }

    public void ResetToDefault()
    {
        Game.Instance.Data.Settings.Volume = 100;
        volume.value = 100;
        valueText.text = "100";
        Game.Instance.Data.Settings.Select = KeyCode.Mouse0;
        keybindTexts[0].text = KeyCode.Mouse0.ToString();

        Game.Instance.Data.Settings.RotateClockWise = KeyCode.E;
        keybindTexts[1].text = KeyCode.E.ToString();

        Game.Instance.Data.Settings.RotateCounterClockWise = KeyCode.Q;
        keybindTexts[2].text = KeyCode.Q.ToString();
    }
}
