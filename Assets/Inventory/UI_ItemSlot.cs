using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_ItemSlot : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private TextMeshProUGUI amount;
    [SerializeField] private Image iconItem;
    
    public ItemInventory itemData;
    public void UpdateItem(ItemInventory _itemData)
    {
        itemData = _itemData;
        iconItem.color = Color.white;
        if (itemData != null)
        {
            iconItem.sprite = itemData.item.itemIcon; 
            if (itemData.stack > 0)
            {
                amount.text = itemData.stack.ToString();
                
            }
            else
            {
                amount.text = "";
            }

        }
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        
       if (itemData != null && itemData.item != null && itemData.item.itemType == ItemType.Equipment)             
       {
            Inventory.ins.EquiItem(itemData.item);
       }
    }

    public void ClenUpSlot()
    {
        itemData = null;
        iconItem.sprite = null;
        iconItem.color = Color.clear;
        amount.text = "";
    }
}
