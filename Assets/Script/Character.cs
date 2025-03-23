using System;
using System.ComponentModel;
using UnityEngine;
public enum Job
{
    [Description("전사")]
    Warrior,
    [Description("마법사")]
    Magician,
}
[Serializable]
public class CharacterData
{
    public int key;
    public Job job;
    public string characterName;
    public int level;
    public int exp;
    public string descript;
    public int gold;
    public int attack;
    public int defense;
    public int health;
    public int critical;
}
[Serializable]
public class Character
{
    public CharacterData characterData{get; private set;}
    public Character(CharacterData data)
    {
        characterData = new CharacterData();
        characterData.key = data.key;
        characterData.job = data.job;
        characterData.characterName = data.characterName;
        characterData.level = data.level;
        characterData.exp = data.exp;
        characterData.descript = data.descript;
        characterData.gold = data.gold;
        characterData.attack = data.attack;
        characterData.defense = data.defense;
        characterData.health = data.health;
        characterData.critical = data.critical;
    }
}