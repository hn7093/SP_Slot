using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance { get { return _instance; } private set { _instance = value; } }
    [SerializeField ] public UIMainMenu UIMainMenu;
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
        if(activate)
        {
            UIMainMenu.Enter();
        }
        else
        {
            UIMainMenu.Exit();
        }
    }
}
