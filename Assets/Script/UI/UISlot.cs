using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISlot : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemCount;
    private Button button;
    private int index;
    public int key;
    public int count;
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(Show);
    }
    public void SetItem(int index, int key, int count)
    {
        this.index = index;
        this.key = key;
        this.count = count;
        itemCount.text = count.ToString();
        itemImage.gameObject.SetActive(true);
        Sprite sprite = GameManager.Instance.ItemManager.sprites[key];
        itemImage.sprite = GameManager.Instance.ItemManager.sprites[key];

    }
    public void RefreshUI()
    {

    }
    private void Show()
    {
        Debug.Log(index + ": " + key + " - " + count);
    }
}
