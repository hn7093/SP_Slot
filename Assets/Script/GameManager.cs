using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } private set { _instance = value; } }
    public Character Player { get; private set; }
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

    void Start()
    {
        SetPlayer(0);
        UIManager.Instance.ActivateUIMainMenu(true);
    }
    public void SetPlayer(int key)
    {
        string loadedText = Resources.Load<TextAsset>("PlayerData").text;
        List<CharacterData> dataList = JsonUtility.FromJson<Extension.Wrapper<CharacterData>>(loadedText).Items;
        Player = new Character(dataList[key]);
    }
}
