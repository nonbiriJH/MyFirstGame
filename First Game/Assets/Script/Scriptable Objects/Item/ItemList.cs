using System.Collections.Generic;
using UnityEngine;


//Register all Items with fix order for save/load
[System.Serializable]
[CreateAssetMenu(fileName = "Inventory", menuName = "Inventory/ItemList")]
public class ItemList : ScriptableObject
{
    public List<Item> itemList = new List<Item>();
}
