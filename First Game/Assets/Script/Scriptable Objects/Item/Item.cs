using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
[CreateAssetMenu(fileName = "Inventory Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public Sprite itemSprite;
    public string itemName;
    public string itemDescription;
    public string[] getDescription;
    //public int itemNumber;
    public bool canUse;
    public UnityEvent useItemEvent;
    public bool weapon;
    public Vector3 RBG;

    public int price;
    //public int shopQuantity;

    //Apply item effects
    public void useItem()
    {
        useItemEvent.Invoke();
    }

    /*//Apply to Consumer Items
    public void ReduceAmount()
    {
        itemNumber--;
        if(itemNumber < 0)
        {
            itemNumber = 0;
        }
    }

    public void BuyItem()
    {
        if(shopQuantity >= 1)
        {
            shopQuantity--;
            itemNumber++;
        }
    }*/
}
