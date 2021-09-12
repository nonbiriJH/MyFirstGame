using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Inventory Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public Sprite itemSprite;
    public string itemName;
    public string itemDescription;
    public int itemNumber;
    public bool canUse;
    public UnityEvent useItemEvent;

    //Apply item effects
    public void useItem()
    {
        useItemEvent.Invoke();
    }

    //Apply to Consumer Items
    public void ReduceAmount()
    {
        itemNumber--;
        if(itemNumber < 0)
        {
            itemNumber = 0;
        }
    }
}
