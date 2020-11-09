using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SettingsData
{
    List<KeyCode> keybindings = new List<KeyCode>() { KeyCode.Mouse0, KeyCode.Mouse1, KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D };
    public List<KeyCode> Keybindings
    {
        get { return keybindings; }
        set { keybindings = value; }
    }

    KeyCode selectCharacter = KeyCode.Mouse0;
    public KeyCode SelectCharacter
    {
        get { return selectCharacter; }
        set { selectCharacter = value; }
    }

    KeyCode selectTile = KeyCode.Mouse1;
    public KeyCode SelectTile
    {
        get { return selectTile; }
        set { selectTile = value; }
    }

    KeyCode moveCameraUp = KeyCode.W;
    public KeyCode MoveCameraUp
    {
        get { return moveCameraUp; }
        set { moveCameraUp = value; }
    }

    KeyCode moveCameraDown = KeyCode.S;
    public KeyCode MoveCameraDown
    {
        get { return moveCameraDown; }
        set { moveCameraDown = value; }
    }

    KeyCode moveCameraLeft = KeyCode.A;
    public KeyCode MoveCameraLeft
    {
        get { return moveCameraLeft; }
        set { moveCameraLeft = value; }
    }

    KeyCode moveCameraRight = KeyCode.D;
    public KeyCode MoveCameraRight
    {
        get { return moveCameraRight; }
        set { moveCameraRight = value; }
    }

    private int volume = 100;

    public int Volume
    {
        get { return volume; }
        set { volume = value; }
    }

}
