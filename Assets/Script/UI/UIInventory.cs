using UnityEngine;
using UnityEngine.UI;

public class UIInventory : UIPage
{
    [SerializeField] Button closeButton;
    Animator animator;
    public override void Enter()
    {
        //animator.Play("UIInventory In");
        SetActivate(true);
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
