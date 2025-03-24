using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIUsePopup : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI statText;
    [SerializeField] private TextMeshProUGUI descriptText;
    [SerializeField] private TextMeshProUGUI useText;

    [SerializeField] private Button useButton;
    [SerializeField] private Button closeButton;
    UnityAction useAction;

    void Awake()
    {
        useButton.onClick.AddListener(Use);
        closeButton.onClick.AddListener(Close);
    }
    public void Setup(int key, UnityAction callback = null)
    {
        // key로 아이템 정보 세팅
        Item item = GameManager.Instance.ItemManager.ItemInfo[key];
        itemImage.sprite = item.sprite;
        string stat = "";
        string descript = item.descript;
        // 아이템 종류별로 버튼 세팅
        if(item.IsConsumable())
        {
            useButton.gameObject.SetActive(true);
            useText.text = "사용하기";
            
        }
        else if(item.IsEquip())
        {
            useButton.gameObject.SetActive(true);
            useText.text = "장착하기";
        }
        else if(item.IsResource())
        {
            useButton.gameObject.SetActive(false);
            useText.text = "";
        }
        statText.text = stat;
        descriptText.text = descript;
        // 이벤트 등록
        useAction += callback;
    }
    public void Use()
    {
        useAction?.Invoke();
        Destroy(gameObject);
    }
    public void Close()
    {
        useAction = null;
        Destroy(gameObject);
    }
}
