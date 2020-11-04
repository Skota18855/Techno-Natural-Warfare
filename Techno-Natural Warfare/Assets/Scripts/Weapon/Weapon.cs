using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon
{
    private int damage;

    public int Damage
    {
        get { return damage; }
        set { damage = value; }
    }
    private int attackRange;

    public int AttackRange
    {
        get { return attackRange; }
        set { attackRange = value; }
    }
    private int accuracyModifer;

    public int AccuracyModifier
    {
        get { return accuracyModifer; }
        set { accuracyModifer = value; }
    }
    private WeaponType type;

    public WeaponType Type
    {
        get { return type; }
        set { type = value; }
    }

    public Weapon(int damage, int attackRange, int accuracyModifier, WeaponType type)
    {
        Damage = damage;
        AttackRange = attackRange;
        AccuracyModifier = accuracyModifier;
        Type = type;
    }
}

public enum WeaponType
{
    Shotgun,
    Rifle,
    Pistol,
    Melee
}
