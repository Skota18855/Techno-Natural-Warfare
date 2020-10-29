using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SettingsData
{
    KeyCode select = KeyCode.Mouse0;
    public KeyCode Select
    {
        get { return select; }
        set { select = value; }
    }

    KeyCode rotateClockwise = KeyCode.E;
    public KeyCode RotateClockWise
    {
        get { return rotateClockwise; }
        set { rotateClockwise = value; }
    }

    KeyCode rotateCounterClockwise = KeyCode.Q;
    public KeyCode RotateCounterClockWise
    {
        get { return rotateCounterClockwise; }
        set { rotateCounterClockwise = value; }
    }

    private KeyCode useSpecial = KeyCode.Space;

    public KeyCode UseSpecial
    {
        get { return useSpecial; }
        set { useSpecial = value; }
    }

    private int volume = 100;

    public int Volume
    {
        get { return volume; }
        set { volume = value; }
    }
}
