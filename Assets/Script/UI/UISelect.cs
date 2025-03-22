using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISelect : UIPage
{
    [SerializeField] Button statButton;
    [SerializeField] Button inventoryButton;
    private Animator animator;
    public override void Enter()
    {
        //animator.Play("UISelect In");
        SetActivate(true);
    }

    public override void Exit()
    {
        //animator.Play("UISelect Out");
        SetActivate(false);
    }
    void Awake()
    {
        animator = GetComponent<Animator>();
        statButton.onClick.AddListener(OpenStat);
        inventoryButton.onClick.AddListener(OpenInventory);
    }
    public void OpenStat()
    {
        UIManager.Instance.UIMainMenu.EnterPage(UIManager.Instance.UIMainMenu.UIStatus);
    }
    public void OpenInventory()
    {
        UIManager.Instance.UIMainMenu.EnterPage(UIManager.Instance.UIMainMenu.UIInventory);
    }
}