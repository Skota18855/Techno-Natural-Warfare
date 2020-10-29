using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Achievement
{
    private bool isCompleted = false;
    private string name;
    private string hintMessage;

    public bool IsCompleted
    {
        get { return isCompleted; }
        set { isCompleted = value; }

    }
    public string Name { get => name; set => name = value; }
    public string HintMessage { get => hintMessage; set => hintMessage = value; }

    public Achievement(string name, string hintMessage)
    {
        Name = name;
        HintMessage = hintMessage;
    }
}
