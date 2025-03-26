using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIStatus : UIPage
{
    [SerializeField] TextMeshProUGUI attackText;
    [SerializeField] TextMeshProUGUI defenseText;
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] TextMeshProUGUI criticalText;
    [SerializeField] Button closeButton;
    Animator animator;

    public override void Enter()
    {
        //animator.Play("UIStatus In");
        Set(GameManager.Instance.Player.CharacterData);
        SetActivate(true);
    }

    public override void Exit()
    {
        //animator.Play("UIStatus Out");
        SetActivate(false);
    }
    void Awake()
    {
        animator = GetComponent<Animator>();
        closeButton.onClick.AddListener(Close);
    }
    public void Set(CharacterData data)
    {
        attackText.text = $"{data.attack}" + (data.EqAtk > 0 ? $"<color=#00FF00> + {data.EqAtk}</color>" : "");
        defenseText.text = $"{data.defense}" + (data.EqDefense > 0 ? $"<color=#00FF00> + {data.EqDefense}</color>" : "");
        healthText.text = $"{data.health}" + (data.EqHealth > 0 ? $"<color=#00FF00> + {data.EqHealth}</color>" : "");
        criticalText.text = $"{data.critical}" + (data.EqCritical > 0 ? $"<color=#00FF00> + {data.EqCritical}</color>" : "");
    }
    public void Close()
    {
        UIManager.Instance.UIMainMenu.BackPage();
    }
}
