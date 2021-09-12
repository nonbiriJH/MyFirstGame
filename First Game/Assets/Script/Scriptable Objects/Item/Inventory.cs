using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//There should only be one inventory preset all possible items a game object can get.
[System.Serializable]
[CreateAssetMenu(fileName = "Inventory", menuName = "Inventory/Player Inventory")]
public class Inventory : ScriptableObject
{
    public Item newItem;
    public Item chosenItem;// Item Use and UI
    public List<Item> itemList = new List<Item>();
    public int coinValue;//will be changed by power up

    public void AddItem(Item newItem)
    {
        if(newItem.itemNumber == 0)
        {
            ReorderEmptyItem();
        }
        newItem.itemNumber++;
    }

    //New Item does not apear before existing item.
    //Trigger only when get new item not in inventory (number = 0)
    public void ReorderEmptyItem()
    {
        for (int i = 0; i < itemList.Count; i++)
        {
            if (itemList[i].itemNumber <= 0)
            {
                Item itemToReorder = itemList[i];
                itemList.Remove(itemToReorder);
                itemList.Add(itemToReorder);
            }
        }
    }
}
