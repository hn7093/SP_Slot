using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStatus : MonoBehaviour, IUIPage
{
    Animator animator;
    public void Enter()
    {
        animator.Play("UIStatus In");
    }

    public void Exit()
    {
        animator.Play("UIStatus Out");
    }
    void Awake()
    {
        animator = GetComponent<Animator>();
    }
}
