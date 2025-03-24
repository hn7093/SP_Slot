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
        var obj = Instantiate(UsePopUp, uiParent);
        if (obj.TryGetComponent<UIUsePopup>(out UIUsePopup popup))
        {
            nowPopup = popup;
        }
    }

    public void SetPopup(int key, UnityAction callback = null)
    {
        if (nowPopup != null)
        {
            nowPopup.Setup(key, callback);
        }
    }
}
