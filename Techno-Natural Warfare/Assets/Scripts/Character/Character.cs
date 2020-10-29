using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character
{
    private string name;

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    private int speed;

    public int Speed
    {
        get { return speed; }
        set { speed = value; }
    }

    private int range;

    public int Range
    {
        get { return range; }
        set { range = value; }
    }

    private int accuracy;

    public int Accuracy
    {
        get { return accuracy; }
        set { accuracy = value; }
    }

    private Weapon equippedWeapon = null;

    public Weapon EquippedWeapon
    {
        get { return equippedWeapon; }
        set { equippedWeapon = value; }
    }

    private Dictionary<PartType, BodyPart> bodyParts;

    public Dictionary<PartType, BodyPart> BodyParts
    {
        get { return bodyParts; }
        set { bodyParts = value; }
    }

    public void ReplaceBodyPart(PartType partToReplace, BodyPart partToReplaceWith)
    {
        bodyParts[partToReplace] = partToReplaceWith;
    }

    public void Attack(Character target)
    {

    }

    public Character(string name, int speed, int range, int accuracy)
    {
        Name = name;
        Speed = speed;
        Range = range;
        Accuracy = accuracy;
    }
}
