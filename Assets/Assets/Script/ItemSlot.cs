using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    // Item Data
    public string itemName;
    public int itemQuantity;
    public Sprite itemIcon;
    public bool isFull;

    // Item Slot
    [SerializeField] private TMP_Text quantityText;
    [SerializeField] private Image itemImage;
    

    private void Start()
{
    if (!isFull)
    {
        quantityText.enabled = false;
        itemImage.enabled = false;
    }
}

   public void AddItem(ItemDetails item)
    {
        itemName = item.ItemName;
        itemQuantity = item.Quantity;
        itemIcon = item.ItemIcon;
        isFull = true;

        quantityText.text = itemQuantity.ToString();
        quantityText.enabled = true;
        itemImage.sprite = itemIcon;

        itemImage.sprite = itemIcon;
        itemImage.enabled = itemIcon != null;
    }
    
}
