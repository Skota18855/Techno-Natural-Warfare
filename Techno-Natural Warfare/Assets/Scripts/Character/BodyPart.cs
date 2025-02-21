﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPart
{
    private bool isNatural = true;

    public bool IsNatural
    {
        get { return isNatural; }
        set { isNatural = value; }
    }

    private int speedModifier = 0;

    public int SpeedModifier
    {
        get { return speedModifier; }
        set { speedModifier = value; }
    }

    private int rangeModifier = 0;

    public int RangeModifier
    {
        get { return rangeModifier; }
        set { rangeModifier = value; }
    }

    private int accuracyModifier = 0;

    public int AccuracyModifier
    {
        get { return accuracyModifier; }
        set { accuracyModifier = value; }
    }

    private PartType partType;

    public PartType PartType
    {
        get { return partType; }
        set { partType = value; }
    }

    private WeaponType weaponType;

    public WeaponType WeaponType
    {
        get { return weaponType; }
        set { weaponType = value; }
    }

    public BodyPart(PartType partType, WeaponType weaponType)
    {
        PartType = partType;
        WeaponType = weaponType;
    }
}

public enum PartType
{
    RightArm,
    LeftArm,
    RightLeg,
    LeftLeg,
    Torso,
    LeftEye,
    RightEye,
    Heart,
    Lungs,
    Brain
}
