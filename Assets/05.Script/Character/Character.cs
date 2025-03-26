using System;
using System.Collections.Generic;
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
    // 기본 스텟
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

    // 장비 스텟
    public int EqAtk;
    public int EqDefense;
    public int EqHealth;
    public int EqCritical;
}
[Serializable]
public class Character
{
    public CharacterData CharacterData { get; private set; }
    public Inventory Inventory { get; private set; }
    public static int maxSize = 20;
    Dictionary<int, Item> ItemInfo;
    public Character(CharacterData data)
    {
        // 데이터 초기화
        CharacterData = new CharacterData();
        CharacterData.key = data.key;
        CharacterData.job = data.job;
        CharacterData.characterName = data.characterName;
        CharacterData.level = data.level;
        CharacterData.exp = data.exp;
        CharacterData.descript = data.descript;
        CharacterData.gold = data.gold;
        CharacterData.attack = data.attack;
        CharacterData.defense = data.defense;
        CharacterData.health = data.health;
        CharacterData.critical = data.critical;

        // 인벤토리 초기화
        Inventory = new Inventory(maxSize, this);
        ItemInfo = GameManager.Instance.ItemManager.ItemInfo;
        Dictionary<int, ItemSlot> savedItems = GameManager.Instance.ItemManager.savedItems;
        for (int i = 0; i < maxSize; i++)
        {
            // 해당 칸에 저장된 정보가 있으면 불러오기
            if (savedItems.ContainsKey(i))
            {
                // 저장된 정보 불러오기
                ItemSlot savedItem = savedItems[i];
                if (savedItem != null)
                {
                    SetItem(savedItem);
                }
            }
        }
    }
    public void SetItem(ItemSlot savedItem)
    {
        Inventory.SetItemSlot(savedItem.index, savedItem);
    }
    public void AddItem(int key, int count)
    {
        Item item = ItemInfo[key];
        AddItem(item, count);
    }
    private void AddItem(Item item, int count)
    {
        int remain = count;
        while (remain > 0)
        {
            // 넣을 수 있는 공간 찾기
            int slotIndex = Inventory.GetEmptyIndex(item.key, remain);
            if (slotIndex != -1)
            {
                // 공간에 갯수만큼 추가
                remain -= Inventory.AddItem(slotIndex, item, remain);
            }
            else
            {
                // 공간이 없으면 중단
                return;
            }
        }
    }
    public void Consume(Item item, int index)
    {
        if (item.health != 0) CharacterData.health += item.health;
        if (item.attack != 0) CharacterData.attack += item.attack;
        if (item.defense != 0) CharacterData.defense += item.defense;
        if (item.critical != 0) CharacterData.critical += item.critical;

        // 개수 감소
        Inventory.UseItem(index, 1);
    }
    public int AutoEquip(int index)
    {
        // 스텟 적용
        if (Inventory.slotList[index].equip)
        {
            UnEquip(index);
        }
        else
        {
            Equip(index);
        }
        // 장착/해제 적용, 바뀐 슬롯 인덱스 반환
        int prev = Inventory.AutoEquip(index);
        return prev;
    }
    public void Equip(int index)
    {
        int key = Inventory.slotList[index].item.key;
        Item item = GameManager.Instance.ItemManager.ItemInfo[key];
        // 장착
        if (item.health != 0) CharacterData.EqHealth += item.health;
        if (item.attack != 0) CharacterData.EqAtk += item.attack;
        if (item.defense != 0) CharacterData.EqDefense += item.defense;
        if (item.critical != 0) CharacterData.EqCritical += item.critical;
    }
    public void UnEquip(int index)
    {
        int key = Inventory.slotList[index].item.key;
        Item item = GameManager.Instance.ItemManager.ItemInfo[key];
        // 해제
        if (item.health != 0) CharacterData.EqHealth -= item.health;
        if (item.attack != 0) CharacterData.EqAtk -= item.attack;
        if (item.defense != 0) CharacterData.EqDefense -= item.defense;
        if (item.critical != 0) CharacterData.EqCritical -= item.critical;
    }
}