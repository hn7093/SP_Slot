using UnityEngine;
using System.Collections.Generic;
public class Inventory
{
    public Dictionary<int, ItemSlot> slotList;
    private Character owner;

    // key: 부위, value: 슬롯 인덱스
    private Dictionary<int, int> equippedItems = new Dictionary<int, int>();
    public Inventory(int size, Character owner)
    {
        slotList = new Dictionary<int, ItemSlot>(size);
        this.owner = owner;
    }
    public void AddItem(int index, ItemSlot itemSlot)
    {
        slotList[index] = itemSlot;
    }
    public int AutoEquip(int newIndex)
    {
        // 슬롯의 아이템 키 값
        int newKey = slotList[newIndex].key;

        if (!ItemLogic.IsEquip(newKey)) return -1; // 장비가 아닐 경우
        // 부위 코드 추출
        int part = newKey / 1000;

        // 해당 부위에 장비가 있는지 확인
        if (equippedItems.TryGetValue(part, out int equippedIndex))
        {
            if (equippedIndex == newIndex)
            {
                // 같은 슬롯이면 해제
                slotList[equippedIndex].equip = false;
                equippedItems.Remove(part);
                return newIndex;
            }
            else
            {
                // 다른 슬롯의 장비와 교체
                slotList[equippedIndex].equip = false;
                owner.UnEquip(equippedIndex);
                slotList[newIndex].equip = true;
                equippedItems[part] = newIndex;
                return equippedIndex;
            }
        }
        else
        {
            // 해당 부위가 비어있다면 새로 장착
            slotList[newIndex].equip = true;
            equippedItems[part] = newIndex;
            return newIndex;
        }
    }
}
