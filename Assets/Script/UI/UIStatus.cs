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
        Set(GameManager.Instance.Player.characterData);
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
        attackText.text = data.attack.ToString();
        defenseText.text = data.defense.ToString();
        healthText.text = data.health.ToString();
        criticalText.text = data.critical.ToString();
    }
    public void Close()
    {
        UIManager.Instance.UIMainMenu.BackPage();
    }
}
