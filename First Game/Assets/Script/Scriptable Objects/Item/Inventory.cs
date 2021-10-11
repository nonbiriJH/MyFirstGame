using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Register reference to items
[System.Serializable]
[CreateAssetMenu(fileName = "Inventory", menuName = "Inventory/Player Inventory")]
public class Inventory : ScriptableObject
{
    public string newItemName;
    public string chosenItemName;// Item Use and UI
    public List<string> itemList = new List<string>();
    public int coinValue;//will be changed by power up

}
