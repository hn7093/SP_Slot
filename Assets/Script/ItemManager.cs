using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager
{
    public Dictionary<int, Item> ItemInfo = new Dictionary<int, Item>();
    public Dictionary<int, ItemSlot> savedItems = new Dictionary<int, ItemSlot>();
    //public Dictionary<int, Sprite> sprites = new Dictionary<int, Sprite>();
    public ItemManager()
    {
        // 아이템 정보 불러오기
        List<Item> dataList = LoadJsonData<Item>("ItemList");
        for (int i = 0; i < dataList.Count; i++)
        {
            dataList[i].sprite = Resources.Load<Sprite>(dataList[i].spritePath);
            ItemInfo[dataList[i].key] = dataList[i];
        }
        
        // 저장정보 불러오기
        List<ItemSlot> savedList = LoadJsonData<ItemSlot>("SavedItem");
        for (int i = 0; i < savedList.Count; i++)
        {
            savedItems[savedList[i].index] = savedList[i];
        }
    }

    // 리소스에서 JSON 불러와 리스트로 반환
    public static List<T> LoadJsonData<T>(string resourcePath)
    {
        TextAsset textAsset = Resources.Load<TextAsset>(resourcePath);
        if (textAsset == null)
        {
            Debug.LogError($"Failed to load resource: {resourcePath}");
            return null;
        }

        string loadedText = textAsset.text;
        return JsonUtility.FromJson<Extension.Wrapper<T>>(loadedText)?.Items;
    }
}
