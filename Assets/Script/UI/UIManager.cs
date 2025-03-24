using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance { get { return _instance; } private set { _instance = value; } }
    public UIMainMenu UIMainMenu;
    private UIUsePopup nowPopup;
    [SerializeField] private Transform uiParent;
    [SerializeField] private GameObject UsePopUp;
    void Awake()
    {
        if (_instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void ActivateUIMainMenu(bool activate)
    {
        if (activate)
        {
            UIMainMenu.Enter();
        }
        else
        {
            UIMainMenu.Exit();
        }
    }
    public void OpenPopup()
    {
        // 팝업창 열기
        uiParent.gameObject.SetActive(true);
        var obj = Instantiate(UsePopUp, uiParent);
        if (obj.TryGetComponent<UIUsePopup>(out UIUsePopup popup))
        {
            nowPopup = popup;
        }
    }
    public void ClosePopup()
    {
        if (nowPopup != null)
        {
            uiParent.gameObject.SetActive(false);
            nowPopup = null;
        }
    }

    public void SetPopup(Item item, bool IsEquip, UnityAction callback = null)
    {
        if (nowPopup != null)
        {
            nowPopup.Setup(item, IsEquip, callback, ClosePopup);
        }
    }
}
