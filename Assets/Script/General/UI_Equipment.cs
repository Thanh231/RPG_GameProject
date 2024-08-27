using UnityEngine.EventSystems;

public class UI_Equipment : UI_ItemSlot
{

    public Equipmenttype slotType;

    private void OnValidate()
    {
        gameObject.name = "Equipment Slot - " + slotType.ToString();
    }
    public override void OnPointerDown(PointerEventData eventData)
    {
        Inventory.ins.UnEquipment(itemData.item as ItemEquipment);
        Inventory.ins.AddItemToInventory(itemData.item as ItemEquipment);   
        ClenUpSlot();
    }
}
    

