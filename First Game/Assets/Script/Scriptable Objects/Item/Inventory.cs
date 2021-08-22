using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Inventory : ScriptableObject
{
    public Item newItem;
    public List<Item> itemList = new List<Item>();
    public int numKey;

    public void AddItem()
    {
        if (!itemList.Contains(newItem))
        {
            itemList.Add(newItem);
            if (newItem.isKey)
            {
                numKey++;
            }
        }
    }

}