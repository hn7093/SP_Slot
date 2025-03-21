using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Job
{
    Warrior,
    Magician,
}
public class Character
{
    public Job job;
    public string characterName;
    public int level;
    public int exp;
    public int reqExp;
    public int descript;
    public int gold;
}
