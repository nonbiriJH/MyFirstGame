using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemMapping
{
    public string itemName;
    public int itemNumber;
    public int shopQuantity;


    //Constructor
    public ItemMapping(string itemName, int itemNumber, int shopQuantity)
    {
        this.itemName = itemName;
        this.itemNumber = itemNumber;
        this.shopQuantity = shopQuantity;
    }

    //Apply to Consumer Items
    public void ReduceAmount()
    {
        itemNumber--;
        if (itemNumber < 0)
        {
            itemNumber = 0;
        }
    }

    public void BuyItem()
    {
        if (shopQuantity >= 1)
        {
            shopQuantity--;
            itemNumber++;
        }
    }
}

[System.Serializable]
[CreateAssetMenu(fileName = "Inventory", menuName = "Inventory/ItemMapping")]
public class ItemQuantityLookup : ScriptableObject
{

    public List<ItemMapping> itemMappings = new List<ItemMapping>();

    public int GetItemShopQuantity(string itemName)
    {
        for (int i = 0; i < itemMappings.Count; i++)
        {
            if (itemMappings[i].itemName == itemName)
            {
                return itemMappings[i].shopQuantity;
            }
        }
        return -1;
    }

    public int GetItemNumber(string itemName)
    {
        for (int i = 0; i < itemMappings.Count; i++)
        {
            if (itemMappings[i].itemName == itemName)
            {
                return itemMappings[i].itemNumber;
            }
        }
        return -1;
    }

    public void BuyItem(string itemName)
    {
        for (int i = 0; i < itemMappings.Count; i++)
        {
            if (itemMappings[i].itemName == itemName)
            {
                itemMappings[i].BuyItem();
            }
        }

    }

    public void ReduceAmount(string itemName)
    {
        for (int i = 0; i < itemMappings.Count; i++)
        {
            if (itemMappings[i].itemName == itemName)
            {
                itemMappings[i].ReduceAmount();
            }
        }

    }

    public void IncreaseAmount(string itemName)
    {
        for (int i = 0; i < itemMappings.Count; i++)
        {
            if (itemMappings[i].itemName == itemName)
            {
                itemMappings[i].itemNumber++;
            }
        }

    }

}
