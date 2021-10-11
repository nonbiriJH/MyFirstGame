using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemMapping
{
    public string itemName;
    public int itemNumber;
    public int shopQuantity;
}

[System.Serializable]
[CreateAssetMenu(fileName = "Inventory", menuName = "Inventory/ItemMapping")]
public class ItemQuantityLookup : ScriptableObject
{

    public ItemMapping[] itemMappings;
}
