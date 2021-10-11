using System.Collections.Generic;
using UnityEngine;


//Register all Items with fix order for save/load
[System.Serializable]
[CreateAssetMenu(fileName = "Inventory", menuName = "Inventory/ItemList")]
public class ItemList : ScriptableObject
{
    public List<Item> itemList = new List<Item>();


    public Item GetItem(string itemName)
    {
        for (int i = 0; i < itemList.Count; i++)
        {
            if (itemList[i].itemName == itemName)
            {
                return itemList[i];
            }
        }
        return null;
    }
}
