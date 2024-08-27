using UnityEngine;

public class ItemObject : MonoBehaviour
{
    [SerializeField] private ItemData itemData;

    private void OnValidate()
    {
        GetComponent<SpriteRenderer>().sprite = itemData.itemIcon;
        gameObject.name = "Item Object - " + itemData.itemName;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Player>() != null)
        {
            Inventory.ins.AddItemToInventory(itemData);
            AudioController.Ins.PlaySound(AudioController.Ins.coinPickup);
            Destroy(gameObject);
        }
    }
}
