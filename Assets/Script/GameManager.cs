using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } private set { _instance = value; } }
    public Character Player { get; private set; }
    public ItemManager ItemManager { get; private set; }
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
        ItemManager = new ItemManager();
        SetPlayer(1);
        UIManager.Instance.ActivateUIMainMenu(true);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            //Player.Additem();
        }
    }
    public void SetPlayer(int key)
    {
        string loadedText = Resources.Load<TextAsset>("PlayerData").text;
        List<CharacterData> dataList = JsonUtility.FromJson<Extension.Wrapper<CharacterData>>(loadedText).Items;
        CharacterData foundData = dataList.FirstOrDefault(data => data.key == key);
        // 찾은 데이터가 null이 아니라면 Character 생성
        if (foundData != null)
        {
            Player = new Character(foundData);
        }
        else
        {
            Debug.LogWarning("해당 key를 가진 데이터가 없습니다.");
        }
    }
}
