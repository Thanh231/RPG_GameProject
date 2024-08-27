using UnityEngine;

public enum ItemType
{
    Equipment,
    Material
}

[CreateAssetMenu(menuName = "Create new Data/Material",fileName = "Item")]
public class ItemData : ScriptableObject
{
    public ItemType itemType;
    public Sprite itemIcon;
    public string itemName;

}
