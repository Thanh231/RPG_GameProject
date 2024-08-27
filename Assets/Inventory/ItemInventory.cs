using UnityEngine;

[System.Serializable]
public class ItemInventory 
{
    public ItemData item;

    public int stack;

    public ItemInventory(ItemData _item)
    {
        item = _item;
    }
    public void AddItem() => stack++;
    public void RemoveItem() => stack--;
}
