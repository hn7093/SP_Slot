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
        // 초기화
        ItemList = new List<UISlot>(maxSize);
        player = GameManager.Instance.Player;
        Dictionary<int, Item> ItemInfo = GameManager.Instance.ItemManager.ItemInfo;
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
                    // 정보 세팅
                    Item item = savedItem.item;
                    ItemList[i].SetItem(i, item, savedItem.count, savedItem.equip);
                    // 클륵 이벤트 등록
                    ItemList[i].SetCallback(Show);
                }
            }
            else
            {
                // 빈 슬롯에는 인덱스와 클릭 이벤트만 등록
                ItemList[i].SetIndex(i);
                ItemList[i].SetCallback(Show);
            }

        }
        slotMaxText.text = maxSize.ToString();
        slotNowText.text = player.Inventory.slotList.Count.ToString();
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
            // 현재 수
            int remain = player.Inventory.slotList[tempIndex].count;
            GameManager.Instance.Player.Consume(item, tempIndex);
            // UI 갱신, 남은갯수에 따라 초기화
            if (remain > 1)
                ItemList[tempIndex].RefreshUI(player.Inventory.slotList[tempIndex]);
            else
                ItemList[tempIndex].SetEmptyUI(tempIndex);
        }
        else if (ItemLogic.IsEquip(item.key))
        {
            int prev = GameManager.Instance.Player.AutoEquip(tempIndex);
            // UI 갱신
            ItemList[tempIndex].RefreshUI(player.Inventory.slotList[tempIndex]);
            // 교환 대비 이전 슬롯도 갱신
            if (tempIndex != prev && prev != -1)
                ItemList[prev].RefreshUI(player.Inventory.slotList[prev]);
        }
    }
    public override void Enter()
    {
        //animator.Play("UIInventory In");
        SetActivate(true);


        // 정보 불러오기 - 초기화 후
        if (player != null)
        {
            for (int i = 0; i < maxSize; i++)
            {
                if (player.Inventory.slotList.ContainsKey(i))
                    ItemList[i].RefreshUI(player.Inventory.slotList[i]);
            }
            slotMaxText.text = maxSize.ToString();
            slotNowText.text = player.Inventory.slotList.Count.ToString();
        }

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
}
