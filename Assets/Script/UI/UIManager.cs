using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance { get { return _instance; } private set { _instance = value; } }
    [SerializeField] UIInventory UIInventory { get; }
    [SerializeField] UIStatus UIStatus { get; }
    [SerializeField] UIMainMenu UIMainMenu { get; }

    private IUIPage currentPage;
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
    public void ChangePage(IUIPage newPage)
    {
        if(currentPage != newPage)
        {
            currentPage?.Exit();
            currentPage = newPage;
            currentPage?.Enter();
        }
    }
}
