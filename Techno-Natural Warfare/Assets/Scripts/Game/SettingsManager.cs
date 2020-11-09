using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] List<Button> buttons = null;
    [SerializeField] Slider volume = null;
    [SerializeField] TextMeshProUGUI valueText = null;
    bool isKeybinding = false;
    int keybindingIndex = -1;
    List<TextMeshProUGUI> keybindTexts = new List<TextMeshProUGUI>();

    List<KeyCode> tempStorage = null;

    private void Start()
    {
        tempStorage = Game.Instance.Data.Settings.Keybindings;
        for (int i = 0; i < buttons.Count && i < Game.Instance.Data.Settings.Keybindings.Count; i++)
        {
            buttons[i].GetComponentInChildren<TextMeshProUGUI>().text = Game.Instance.Data.Settings.Keybindings[i].ToString();
            keybindTexts.Add(buttons[i].GetComponentInChildren<TextMeshProUGUI>());
        }
        //buttons[0].GetComponentInChildren<TextMeshProUGUI>().text = Game.Instance.Data.Settings.SelectCharacter.ToString();
        //buttons[1].GetComponentInChildren<TextMeshProUGUI>().text = Game.Instance.Data.Settings.SelectTile.ToString();
        //buttons[2].GetComponentInChildren<TextMeshProUGUI>().text = Game.Instance.Data.Settings.MoveCameraUp.ToString();
        //buttons[3].GetComponentInChildren<TextMeshProUGUI>().text = Game.Instance.Data.Settings.MoveCameraDown.ToString();
        //buttons[4].GetComponentInChildren<TextMeshProUGUI>().text = Game.Instance.Data.Settings.MoveCameraLeft.ToString();
        //buttons[5].GetComponentInChildren<TextMeshProUGUI>().text = Game.Instance.Data.Settings.MoveCameraRight.ToString();

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
        if (!isKeybinding)
        {
            isKeybinding = true;
            keybindingIndex = buttonIndex;
            keybindTexts[buttonIndex].text = "<New Binding>";
            //StartCoroutine("KeybindChange", keybindTexts[buttonIndex]);
        }
    }

    private void LateUpdate()
    {
        if (Input.anyKeyDown && isKeybinding)
        {
            foreach (KeyCode vKey in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKey(vKey))
                {
                    if (!tempStorage.Contains(vKey))
                    {
                        keybindTexts[keybindingIndex].text = vKey.ToString();
                        isKeybinding = false;
                        tempStorage[keybindingIndex] = vKey;
                        keybindingIndex = -1;
                    }
                }
            }
        }
    }

    //IEnumerator KeybindChange(TextMeshProUGUI text)
    //{
    //    while (!Input.anyKeyDown)
    //    {
    //        yield return null;
    //    }

    //    foreach (KeyCode vKey in Enum.GetValues(typeof(KeyCode)))
    //    {
    //        if (Input.GetKey(vKey))
    //        {
    //            text.text = vKey.ToString();
    //        }
    //    }
    //    StopCoroutine("AwaitButtonPress");
    //    isKeybinding = false;
    //    yield return null;
    //}

    public void CloseSetting()
    {
        Game.Instance.Data.Settings.Volume = (int)volume.value;
        Game.Instance.Data.Settings.SelectCharacter = (KeyCode)Enum.Parse(typeof(KeyCode), buttons[0].GetComponentInChildren<TextMeshProUGUI>().text, true);
        Game.Instance.Data.Settings.SelectTile = (KeyCode)Enum.Parse(typeof(KeyCode), buttons[1].GetComponentInChildren<TextMeshProUGUI>().text, true);
        Game.Instance.Data.Settings.MoveCameraUp = (KeyCode)Enum.Parse(typeof(KeyCode), buttons[2].GetComponentInChildren<TextMeshProUGUI>().text, true);
        Game.Instance.Data.Settings.MoveCameraDown = (KeyCode)Enum.Parse(typeof(KeyCode), buttons[3].GetComponentInChildren<TextMeshProUGUI>().text, true);
        Game.Instance.Data.Settings.MoveCameraLeft = (KeyCode)Enum.Parse(typeof(KeyCode), buttons[4].GetComponentInChildren<TextMeshProUGUI>().text, true);
        Game.Instance.Data.Settings.MoveCameraRight = (KeyCode)Enum.Parse(typeof(KeyCode), buttons[5].GetComponentInChildren<TextMeshProUGUI>().text, true);

        Game.Instance.Data.Settings.Keybindings = new List<KeyCode>()
        {
            Game.Instance.Data.Settings.SelectCharacter,
            Game.Instance.Data.Settings.SelectTile,
            Game.Instance.Data.Settings.MoveCameraUp,
            Game.Instance.Data.Settings.MoveCameraDown,
            Game.Instance.Data.Settings.MoveCameraLeft,
            Game.Instance.Data.Settings.MoveCameraRight
        };
        Game.Instance.SceneManagerObject.LoadSceneAsyncByName("Main Menu");
    }

    public void ResetToDefault()
    {
        Game.Instance.Data.Settings.Volume = 100;
        volume.value = 100;
        valueText.text = "100";

        Game.Instance.Data.Settings.SelectCharacter = KeyCode.Mouse0;
        keybindTexts[0].text = KeyCode.Mouse0.ToString();

        Game.Instance.Data.Settings.SelectTile = KeyCode.Mouse1;
        keybindTexts[1].text = KeyCode.Mouse1.ToString();

        Game.Instance.Data.Settings.MoveCameraUp = KeyCode.W;
        keybindTexts[2].text = KeyCode.W.ToString();

        Game.Instance.Data.Settings.MoveCameraDown = KeyCode.S;
        keybindTexts[3].text = KeyCode.S.ToString();

        Game.Instance.Data.Settings.MoveCameraLeft = KeyCode.A;
        keybindTexts[4].text = KeyCode.A.ToString();

        Game.Instance.Data.Settings.MoveCameraRight = KeyCode.D;
        keybindTexts[5].text = KeyCode.D.ToString();

        Game.Instance.Data.Settings.Keybindings = new List<KeyCode>()
        {
            Game.Instance.Data.Settings.SelectCharacter,
            Game.Instance.Data.Settings.SelectTile,
            Game.Instance.Data.Settings.MoveCameraUp,
            Game.Instance.Data.Settings.MoveCameraDown,
            Game.Instance.Data.Settings.MoveCameraLeft,
            Game.Instance.Data.Settings.MoveCameraRight
        };
    }
}
