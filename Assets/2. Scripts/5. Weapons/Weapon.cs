using System;
using UnityEngine;

[Serializable]
public class Weapon : MonoBehaviour, Upgradeable
{
    public float Damage;
    public int UpgradeCost;
    public string Name;
    public int Level;
    public Sprite Sprite;

    public string GetName()
    {
        return Name;
    }

    public int GetLevel()
    {
        return Level;
    }

    public Sprite GetSprite()
    {
        return Sprite;
    }

    public void SetAttack(float attack)
    {
        Damage = attack;
    }

    public void SetLevel(int level)
    {
        Level = level;
    }
}