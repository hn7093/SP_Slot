using System.Collections.Generic;
using System;
using UnityEngine;
public class Inventory
{
    public Dictionary<int, ItemSlot> slotList;

    // key: 부위, value: 슬롯 인덱스
    private Dictionary<int, int> equippedItems = new Dictionary<int, int>();
    private Character owner;
    private int size;
    public Inventory(int size, Character owner)
    {
        slotList = new Dictionary<int, ItemSlot>(size);
        this.size = size;
        this.owner = owner;
    }
    public void SetItemSlot(int index, ItemSlot itemSlot)
    {
        slotList[index] = itemSlot;
        //slotList[index].item.maxStack = maxStack;
    }
    public int GetEmptyIndex(int key, int count)
    {
        // 특정 key를 count 만큼 넣을 수 있는 칸 제일 앞의 칸
        int index = -1;
        for (int i = 0; i < size; i++)
        {
            // 내용 있는 칸인지 검사
            if (slotList.TryGetValue(i, out ItemSlot slot))
            {

                // 다른 아이템이면 넘김
                if (slotList[i].item.key != key)
                {
                    continue;
                }

                // 공간이 없으면 넘김
                if ((slotList[i].item.maxStack - slotList[i].count) == 0)
                {
                    continue;
                }

                // 남은 갯수 파악 - 충분한 공간이 있으면 반환
                else if ((slotList[i].item.maxStack - slotList[i].count) >= count)
                {
                    Debug.Log("공간 있음");
                    return i;
                }
                else
                {
                    // 공간이 없지만 같은 키, 임시 순서 저장
                    Debug.Log("임시");
                    return i;
                }
            }
            else
            {
                // 빈칸 반환,  임시 키가 있으면 반환
                Debug.Log("인덱스 반환 :" + index);
                return index == -1 ? i : index;
            }
        }
        // 넣을 수 없는 공간 없음
        Debug.Log("공간 없음");
        return -1;
    }

    public int AddItem(int index, Item item, int count)
    {
        // 내용 있는 칸
        if (slotList.TryGetValue(index, out ItemSlot slot))
        {
            // 남은 공간과 인자로 넣을 갯수 구하기
            int remain = item.maxStack - slotList[index].count;
            int finalTarget;
            if (remain >= count)
            {
                // 충분한 공간
                finalTarget = count;
            }
            else
            {
                // 공간이 부족하면 빈 공간만
                finalTarget = remain;
            }
            Debug.Log("추가 : " + index + "에 " + finalTarget);
            slotList[index].count += finalTarget;
            return finalTarget;
        }
        else
        {
            // 빈칸, 갯수와 최대 갯수로 넣을 수 결정
            int target = item.maxStack - count > 0 ? count : count - item.maxStack;
            slotList[index] = new ItemSlot(index, item, target);
            return target;
        }
    }
    public void UseItem(int index, int count)
    {
        slotList[index].count -= count;
        if (slotList[index].count == 0)
        {
            slotList.Remove(index);
        }
    }
    public int AutoEquip(int newIndex)
    {
        // 슬롯의 아이템 키 값
        int newKey = slotList[newIndex].item.key;

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
