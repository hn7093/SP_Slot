using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIMainMenu : UIPage
{
    [Header("Child UI")]
    public UIInventory UIInventory;
    public UIStatus UIStatus;
    public UISelect UISelect;
    [Header("My UI")]
    [SerializeField] private TextMeshProUGUI jobText;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI expText;
    [SerializeField] private Image expImage;
    [SerializeField] private TextMeshProUGUI descriptText;
    [SerializeField] private TextMeshProUGUI goldText;


    private List<UIPage> uIPages = new List<UIPage>();
    private UIPage currentPage;

    Animator animator;
    public override void Enter()
    {
        //animator.Play("UIMainMenu In");
        Set(GameManager.Instance.Player.CharacterData);
        SetActivate(true);
    }
    public override void Exit()
    {
        //animator.Play("UIMainMenu Out");
        SetActivate(false);
    }
    void Awake()
    {
        animator = GetComponent<Animator>();
        EnterPage(UISelect);
    }
    public void Set(CharacterData data)
    {
        jobText.text = Extension.GetDescription(data.job);
        nameText.text = data.characterName;
        levelText.text = data.level.ToString();
        float reqExp = data.level * 2 + 3;
        expText.text = data.exp + "/" + reqExp;
        expImage.fillAmount = (float)data.exp / reqExp;
        descriptText.text = data.descript;
        goldText.text = data.gold.ToString();
    }
    public void EnterPage(UIPage uIPage)
    {
        int cout = uIPages.Count;
        if (cout > 0)
        {
            UIPage currentPage = uIPages[cout - 1];
            currentPage.Exit();
        }
        uIPages.Add(uIPage);
        uIPage.Enter();
    }
    public void ExitPage(UIPage uIPage)
    {
        uIPage.Exit();
        uIPages.Remove(uIPage);
        UIPage currentPage = uIPages[uIPages.Count - 1];
        currentPage.Enter();
    }
    public void BackPage()
    {
        UIPage currentPage = uIPages[uIPages.Count - 1];
        currentPage.Exit();
        uIPages.Remove(currentPage);
        currentPage = uIPages[uIPages.Count - 1];
        currentPage.Enter();
    }
    public void ChangePage(UIPage newPage)
    {
        if (currentPage != newPage)
        {
            currentPage?.Exit();
            currentPage = newPage;
            currentPage?.Enter();
        }
    }
}