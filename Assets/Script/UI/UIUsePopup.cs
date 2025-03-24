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
    UnityAction closeAction;

    void Awake()
    {
        useButton.onClick.AddListener(Use);
        closeButton.onClick.AddListener(Close);
    }
    public void Setup(Item item, bool IsEquip, UnityAction callback = null, UnityAction closeCallback = null)
    {
        // key로 아이템 정보 세팅
        itemImage.sprite = item.sprite;
        string stat = "";
        string descript = item.descript;
        // 아이템 종류별로 버튼 세팅
        if(ItemLogic.IsConsumable(item.key))
        {
            useButton.gameObject.SetActive(true);
            useText.text = "사용하기";
            
        }
        else if(ItemLogic.IsEquip(item.key))
        {
            useButton.gameObject.SetActive(true);
            useText.text = IsEquip ? "해제하기" :"장착하기";
        }
        else if(ItemLogic.IsResource(item.key))
        {
            useButton.gameObject.SetActive(false);
            useText.text = "";
        }
        statText.text = stat;
        descriptText.text = descript;
        // 이벤트 등록
        useAction += callback;
        closeAction += closeCallback;
    }
    public void Use()
    {
        useAction?.Invoke();
        closeAction?.Invoke();
        Destroy(gameObject);
    }
    public void Close()
    {
        closeAction?.Invoke();
        Destroy(gameObject);
    }
}
