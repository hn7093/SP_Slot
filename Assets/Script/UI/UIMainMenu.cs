using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMainMenu : MonoBehaviour, IUIPage
{
    Animator animator;
    public void Enter()
    {
        animator.Play("UIMainMenu In");
    }

    public void Exit()
    {
        animator.Play("UIMainMenu Out");
    }
    void Awake()
    {
        animator = GetComponent<Animator>();
    }
}
