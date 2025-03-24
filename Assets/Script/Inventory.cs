using UnityEngine;
using System.Collections.Generic;
public class Inventory
{
    public Dictionary<int, ItemSlot> slotList;
    private int mainHandIndex; // 1000
    private int mainKey;
    private int subHandIndex; // 2000
    private int subKey;
    public Inventory(int size)
    {
        slotList = new Dictionary<int, ItemSlot>(size);
        mainHandIndex = -1;
        subHandIndex = -1;
    }
    public int AutoEquip(int newIndex)
    {
        int newKey = slotList[newIndex].key;
        //int mainKey = 
        // 빈 공간에 장착 - 메인
        if (mainHandIndex == -1 && ItemLogic.IsEquip(newKey) && newKey / 1000 == 1)
        {
            Debug.Log("메인 빈곳에 새로 추가");
            slotList[newIndex].equip = true;
            mainHandIndex = newIndex;
            mainKey = newKey;
            return newIndex;
        }
        else if (subHandIndex == -1 && ItemLogic.IsEquip(newKey) && newKey / 1000 == 2)
        {
            // 빈 공간에 장착 - 서브
            Debug.Log("서브 새로 장착");
            slotList[newIndex].equip = true;
            subHandIndex = newIndex;
            subKey = newKey;
            return newIndex;
        }
        // 부위 판별 - 메인
        else if (ItemLogic.IsSamePart(mainKey, newKey))
        {
            if (mainHandIndex == newIndex)
            {
                // 같은 슬롯 = 해제
                Debug.Log("메인 해제");
                slotList[mainHandIndex].equip = false;
                mainHandIndex = -1;
                mainKey = 0;
                return newIndex;
            }
            else
            {
                // 다른 슬롯= 교체, 
                // 기본 해제, 새로 장착
                Debug.Log("메인 교환");
                int prev = mainHandIndex;
                slotList[mainHandIndex].equip = false;
                slotList[newIndex].equip = true;
                mainHandIndex = newIndex;
                mainKey = newKey;
                return prev;
            }
        }
        // 부위 판별 - 서브
        else if (ItemLogic.IsSamePart(subKey, newKey))
        {
            if (subHandIndex == newIndex)
            {
                // 같은 슬롯 = 해제
                Debug.Log("서브 해제");
                slotList[subHandIndex].equip = false;
                subHandIndex = -1;
                subKey = 0; 
                return newIndex;
            }
            else
            {
                // 다른 슬롯= 교체, 
                // 기본 해제, 새로 장착
                Debug.Log("서브 교환");
                int prev = mainHandIndex;
                slotList[subHandIndex].equip = false;
                slotList[newIndex].equip = true;
                subHandIndex = newIndex;
                subKey = newKey;
                return prev;
            }
        }
        return -1;
    }
}
