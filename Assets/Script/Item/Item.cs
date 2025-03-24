using System;
using UnityEngine;
[Serializable]
public class Item
{
    public int key;
    public string name;
    public int maxStack;
    public string spritePath;
    public int attack;
    public int health;
    public int defense;
    public int critical;
    public int price;
    public string descript;
    public Sprite sprite;

    public bool IsResource()
    {
        // 1000 아래는 기타자원
        return key < 1000;
    }
    public bool IsEquip()
    {
        // 장비품
        return key >= 1000 && key < 10000;
    }
    public bool IsConsumable()
    {
        // 소모품
        return key >= 10000;
    }
}
