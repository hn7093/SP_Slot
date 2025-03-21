using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventory : MonoBehaviour, IUIPage
{
    Animator animator;
    public void Enter()
    {
        animator.Play("UIInventory In");
    }

    public void Exit()
    {
        animator.Play("UIInventory Out");
    }
    void Awake()
    {
        animator = GetComponent<Animator>();
    }
}
