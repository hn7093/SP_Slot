using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ItemLogic
{
    public static bool IsResource(int key)
    {
        // 1000 아래는 기타자원
        return key < 1000;
    }
    public static bool IsEquip(int key)
    {
        // 장비품
        return key >= 1000 && key < 10000;
    }
    public static bool IsConsumable(int key)
    {
        // 소모품
        return key >= 10000;
    }
    public static bool IsSamePart(int firKey, int secKey)
    {
        if(IsEquip(firKey) && IsEquip(secKey))
        {
            int typeFir = firKey/1000;
            int typeSec = secKey/1000;
            return typeFir == typeSec;
        }
        else
            return false;
    }
}
