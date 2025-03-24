using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIInventory : UIPage
{
    [SerializeField] Button closeButton;
    [SerializeField] Transform ContentParent;

    [SerializeField] GameObject SlotPrefab;
    [SerializeField] TextMeshProUGUI slotMaxText;
    [SerializeField] TextMeshProUGUI slotNowText;

    public static int maxSize = 20;
    private List<UISlot> ItemList = new List<UISlot>(maxSize); // 아이템 슬롯 UI
    private Character player;
    private int tempIndex;
    private int tempKey;
    Animator animator;

    void Start()
    {
        ItemList = new List<UISlot>(maxSize);
        player = GameManager.Instance.Player;

        for (int i = 0; i < maxSize; i++)
        {
            var slotui = Instantiate(SlotPrefab, ContentParent);
            ItemList.Add(slotui.GetComponent<UISlot>());
            // 해당 칸에 저장된 정보가 있으면 불러오기
            if (player.Inventory.slotList.ContainsKey(i))
            {
                // 저장된 정보 불러오기
                ItemSlot savedItem = player.Inventory.slotList[i];
                if (savedItem != null)
                {
                    Item item = GameManager.Instance.ItemManager.ItemInfo[savedItem.key];
                    ItemList[i].SetItem(i, item, savedItem.count, savedItem.equip);
                    ItemList[i].SetCallback(Show);
                }
            }
        }
    }
    // 팝업 생성 호출
    private void Show(int index, int key)
    {
        // 0 은 설정 안됨
        if (key != 0)
        {
            Item item = GameManager.Instance.ItemManager.ItemInfo[key];
            tempIndex = index;
            tempKey = item.key;
            bool IsEquip = player.Inventory.slotList[tempIndex].equip;
            UIManager.Instance.OpenPopup();
            UIManager.Instance.SetPopup(item, IsEquip, UseItem);
        }
    }
    private void UseItem()
    {
        Item item = GameManager.Instance.ItemManager.ItemInfo[tempKey];
        // 종류별 사용
        if (ItemLogic.IsConsumable(item.key))
        {
            GameManager.Instance.Player.Consume(item, tempIndex);
            // UI 갱신
            ItemList[tempIndex].RefreshUI(player.Inventory.slotList[tempIndex]);
        }
        else if (ItemLogic.IsEquip(item.key))
        {
            int prev = GameManager.Instance.Player.AutoEquip(item, tempIndex);
            // UI 갱신
            ItemList[tempIndex].RefreshUI(player.Inventory.slotList[tempIndex]);
            // 교환 대비 이전 슬롯도 갱신
            if(tempIndex != prev)
                ItemList[prev].RefreshUI(player.Inventory.slotList[prev]);
        }
    }
    public override void Enter()
    {
        //animator.Play("UIInventory In");
        SetActivate(true);
        slotMaxText.text = maxSize.ToString();
        slotNowText.text = GameManager.Instance.ItemManager.savedItems.Count.ToString();
    }

    public override void Exit()
    {
        //animator.Play("UIInventory Out");
        SetActivate(false);
    }
    void Awake()
    {
        animator = GetComponent<Animator>();
        closeButton.onClick.AddListener(Close);
    }
    public void Close()
    {
        UIManager.Instance.UIMainMenu.BackPage();
    }
    // 아이템 추가 - 키, 수
    public bool AddItem(int key, int amount = 1)
    {
        if (amount < 0)
        {
            Debug.LogError("오류 : 갯수가 0이하");
            return false;
        }
        if (key < 0)
        {
            Debug.LogError("오류 : 인덱스 0보다 작음");
            return false;
        }
        Item itemInfo = null;//DataManager.Instance.ItemDataLoader.GetByKey(key);
        if (itemInfo != null)
        {
            // 인벤토리에서 검색
            int remain = amount;
            for (int i = 0; i < ItemList.Count; i++)
            {
                if (ItemList[i] != null && ItemList[i].count > 0 && ItemList[i].key == key)
                {
                    // 아직 maxStack만큼 안 찼으면
                    if (ItemList[i].count < itemInfo.maxStack)
                    {
                        Debug.Log($"아이템 {itemInfo.name}이(가) 인벤토리 슬롯 {i}에 겹쳐 추가");
                        ItemList[i].count += amount;
                        return true;
                    }
                }
            }


            // 기존 아이템을 찾지 못했거나 모두 가득 찼으면 빈슬롯 찾기
            bool addedToEmptySlot = false;
            // 인벤부터 찾기
            for (int i = 0; i < ItemList.Count; i++)
            {
                if (ItemList[i] != null)
                {
                    //ItemList[i] = new ItemSlot(item, amount);
                    Debug.Log($"아이템 {itemInfo.name}이(가) 인벤토리 슬롯 {i}에 추가됨");
                    addedToEmptySlot = true;
                    return true;
                }
            }

            // 빈슬롯 없이 모두 가득 찼으면
            if (!addedToEmptySlot)
            {
                Debug.LogError("오류 : 인벤토리가 가득 참");
                return false;
            }
            return false; ;
        }
        else
        {
            Debug.LogError($"해당 정보 없음 키 : {key}");
            return false;
        }
    }
}
