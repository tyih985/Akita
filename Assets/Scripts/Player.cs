using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player
{
    public Character character;
    public List<Character> characterList = new();
    public float posX = 0;
    public float posY = 0;
    public float lastSaveX = 0;
    public float lastSaveY = 0;
    public float moveSpeed = 0;
    public float runSpeed = 0;
    public int coins = 0;

    void SwitchCharacter(Characters character)
    {
        this.character = characterList[(int)character];
    }
}

[System.Serializable]
public class Character
{
    public string name = "";
    public Characters character;
    public RuntimeAnimatorController animator;
    public int hp = 100;
    public int maxhp = 100;
    public int sp = 100;
    public int maxsp = 100;
    public int strength = 10;
    public int might = 10;
    public int defence = 10;
    public int resistance = 10;
    public int resilience = 10;
    public int agility = 10;
}

[System.Serializable]
public enum Characters
{
    Jin,
    Byron,
    Erik,
    Ina
}