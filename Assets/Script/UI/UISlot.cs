using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UISlot : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemCount;
    [SerializeField] private Outline outline;
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
    public void SetItem(int index, Item item, int count, bool IsEquip)
    {
        // 초기 세팅
        this.index = index;
        this.key = item.key;
        this.count = count;
        itemCount.text = count.ToString();
        itemImage.gameObject.SetActive(true);
        itemImage.sprite = item.sprite;
        // 장착 시 선 표시
        outline.enabled = IsEquip;
    }
    public void RefreshUI(ItemSlot itemSlot)
    {
        // 갱신
        count = itemSlot.count;
        itemCount.text = count.ToString();
        if (count <= 0)
        {
            itemImage.gameObject.SetActive(false);
            itemCount.text = "";
            key = 0;
        }
        // 장착 시 선 표시
        outline.enabled = itemSlot.equip;
    }
}
