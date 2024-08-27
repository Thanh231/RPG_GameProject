using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Inventory : MonoBehaviour
{
    public static Inventory ins;

    public GameObject inventoryDialog;

    
    public List<ItemInventory> inventoryItem;
    public List<ItemInventory> stashItem;
    public List<ItemInventory> equipmentItem;

    public Dictionary<ItemData, ItemInventory> inventoryDictionary;
    public Dictionary<ItemData, ItemInventory> stashDictionary;
    public Dictionary<ItemEquipment, ItemInventory> equipmentDictionary;

    public Transform inventoryUI;
    private UI_ItemSlot[] inventotySlot;

    public Transform stash;
    private UI_ItemSlot[] stashSlot;

    public Transform equipment;
    private UI_Equipment[] equipmentSlot;

    private void Awake()
    {
        if(ins == null)
        {
            ins = this;
        }
        else if(ins != null)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        inventoryItem = new List<ItemInventory>();
        stashItem = new List<ItemInventory>();
        equipmentItem = new List<ItemInventory>();

        inventoryDictionary = new Dictionary<ItemData, ItemInventory>();
        stashDictionary = new Dictionary<ItemData, ItemInventory>();
        equipmentDictionary = new Dictionary<ItemEquipment, ItemInventory>();

        inventotySlot = inventoryUI.GetComponentsInChildren<UI_ItemSlot>();
        stashSlot = stash.GetComponentsInChildren<UI_ItemSlot>();
        equipmentSlot = equipment.GetComponentsInChildren<UI_Equipment>();
    }
    public void EquiItem(ItemData _item)
    {
        ItemEquipment newEquipment = _item as ItemEquipment;
        ItemInventory newItem = new ItemInventory(_item);

        ItemEquipment oldEquiment = null;

        foreach (KeyValuePair<ItemEquipment, ItemInventory> item in equipmentDictionary)
        {
            if (item.Key.equimenttype == newEquipment.equimenttype)
            {
                oldEquiment = item.Key;
            }
        }

        if (oldEquiment != null)
        {
            UnEquipment(oldEquiment);
            AddItemToInventory(oldEquiment);
        }

        equipmentItem.Add(newItem);
        equipmentDictionary.Add(newEquipment, newItem);
        newEquipment.AddModify();

        RemoveItemFromInventory(_item);


        UpdateUI();
    }

    public void UnEquipment(ItemEquipment itemToDelete)
    {
        if (equipmentDictionary.TryGetValue(itemToDelete, out ItemInventory value))
        {
            equipmentItem.Remove(value);
            equipmentDictionary.Remove(itemToDelete);
            itemToDelete.RemoveModify();
        }
    }

    public void UpdateUI()
    {
        for (int i = 0; i < equipmentSlot.Length; i++)
        {
            foreach (KeyValuePair<ItemEquipment, ItemInventory> item in equipmentDictionary)
            {
                if (item.Key.equimenttype == equipmentSlot[i].slotType)
                {
                    equipmentSlot[i].UpdateItem(item.Value);
                }
            }
        }

        for (int i = 0; i < inventotySlot.Length; i++)
        {
            inventotySlot[i].ClenUpSlot();
        }
        for (int i = 0; i < stashSlot.Length; i++)
        {
            stashSlot[i].ClenUpSlot();
        }


        for (int i = 0;i < inventoryItem.Count;i++)
        {
            inventotySlot[i].UpdateItem(inventoryItem[i]);
        }
        for(int i = 0; i < stashItem.Count;i++)
        {
            stashSlot[i].UpdateItem(stashItem[i]);
        }
    }
    public void AddItemToInventory(ItemData _item)
    {
        if(_item.itemType == ItemType.Equipment)
        {
            AddInventory(_item);
        }
        else if(_item.itemType == ItemType.Material)
        {
            AddStash(_item);
        }

        UpdateUI();
    }
    private void AddStash(ItemData _item)
    {
        if(stashDictionary.TryGetValue(_item, out ItemInventory value))
        {
            value.AddItem();
        }
        else
        {
            ItemInventory item = new ItemInventory(_item);
            stashItem.Add(item);
            stashDictionary.Add(_item, item);
            item.AddItem();
        }
    }
    private void AddInventory(ItemData _item)
    {
        if (inventoryDictionary.TryGetValue(_item, out ItemInventory value))
        {
            value.AddItem();
        }
        else
        {
            ItemInventory newInItem = new ItemInventory(_item);
            inventoryItem.Add(newInItem);
            inventoryDictionary.Add(_item, newInItem);
            newInItem.AddItem();

        }
    }

    public void RemoveItemFromInventory(ItemData _item)
    {
        if(inventoryDictionary.TryGetValue(_item,out ItemInventory value))
        {
            if(value.stack > 1)
            {
                value.RemoveItem();
            }
            else
            {
                inventoryItem.Remove(value);
                inventoryDictionary.Remove(_item);
            }
        }

        if(stashDictionary.TryGetValue(_item,out ItemInventory statshValue))
        {
            if(statshValue.stack > 1)
            {
                statshValue.RemoveItem();
            }
            else
            {
                stashItem.Remove(statshValue);
                stashDictionary.Remove(_item);  
            }

        }

        UpdateUI();
    }

    public void OpenInvetory()
    {
        inventoryDialog.SetActive(true);
        Time.timeScale = 0;
    }    
    public void CloseInvetory()
    {
        inventoryDialog.SetActive(false);
        Time.timeScale = 1;
    }
    public void Replay()
    {
        SceneManager.LoadScene(1);
    }
    
}
