using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimelineGetItem : MonoBehaviour
{
    [SerializeField]
    private Item item;
    [SerializeField]
    private Inventory inventory;//pass item to inventory new item
    [SerializeField]
    private Player player;
    [SerializeField]
    private GameObject dialogBox;

    private void OnEnable()
    {
        inventory.newItemName = item.itemName;
        //player into interact state
        player.interacted = false;
        player.ChangeState(new PlayerInteractState(player));

    }

    private void Update()
    {
        if (!dialogBox.activeInHierarchy && player.inventory.newItemName != null)
        {
            //set animation
            player.animator.SetBool("GetItem", false);
            //get item pic
            player.itemSprite.sprite = null;
            player.inventory.newItemName = null;
        }
    }
}
