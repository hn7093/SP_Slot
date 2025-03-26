using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISelect : UIPage
{
    [SerializeField] Button statButton;
    [SerializeField] Button inventoryButton;
    [SerializeField] Button appleButton;
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
        appleButton.onClick.AddListener(AddApple);
    }
    public void OpenStat()
    {
        UIManager.Instance.UIMainMenu.EnterPage(UIManager.Instance.UIMainMenu.UIStatus);
    }
    public void OpenInventory()
    {
        UIManager.Instance.UIMainMenu.EnterPage(UIManager.Instance.UIMainMenu.UIInventory);
    }
    public void AddApple()
    {
        GameManager.Instance.Player.AddItem(10000, 5);
    }
}