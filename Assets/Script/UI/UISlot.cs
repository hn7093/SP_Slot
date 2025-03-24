using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UISlot : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemCount;
    private Button button;
    private int index;
    public int key;
    public int count;
    void Awake()
    {
        button = GetComponent<Button>();

    }
    public void SetCallback(UnityAction<int, int> callback)
    {
        button.onClick.AddListener(() => callback?.Invoke(index, key));
    }
    public void SetItem(int index, Item item, int count)
    {
        this.index = index;
        this.key = item.key;
        this.count = count;
        itemCount.text = count.ToString();
        itemImage.gameObject.SetActive(true);
        itemImage.sprite = item.sprite;
    }
    public void RefreshUI(Item item)
    {

    }
}
