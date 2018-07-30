using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Utility/Inventory")]
public class Inventory : ScriptableObject {

    //public EquippedItems equippedItems;
    public References references;

    public List<Item> inventoryItems = new List<Item>();

    public void AddItem(Item item)
    {
        inventoryItems.Add(item);
    }

    public void RemoveItem(int index)
    {
        inventoryItems.RemoveAt(index);
    }

    public Item GetItem(int index)
    {
        return inventoryItems[index];
    }
    public string GetItemName(int index)
    {
        return inventoryItems[index].itemName;
    }

}
