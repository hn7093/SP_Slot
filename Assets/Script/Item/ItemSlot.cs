using System;
[Serializable]
public class ItemSlot
{
    public int index;
    public int count;
    public int key;
    public Item item;
    public bool equip;
    public ItemSlot(int index)
    {
        this.index = index;
        this.count = 0;
    }
    public ItemSlot(int index, Item item, int count, bool equip = false)
    {
        this.index = index;
        this.count = count;
        this.item = item;
        this.key = item.key;
        this.equip = equip;
    }
}