using System.Collections;
using UnityEngine;

public class PlayerInteractState : State
{

    //Constructor
    public PlayerInteractState(Player player) : base(player)
    {
    }

    public override void BeginState()
    {
        base.UpdateLogics();
        if (!player.interacted)
        {
            if (player.inventory.newItemName != null)
            {
                //set RBG Renderer
                Vector3 itemColor = player.itemMaster.GetItem(player.inventory.newItemName).RBG;
                if (itemColor != Vector3.zero)
                {
                    player.getItem.GetComponent<SpriteRenderer>().color = new Color(itemColor.x, itemColor.y, itemColor.z);
                }
                else
                {
                    player.getItem.GetComponent<SpriteRenderer>().color = new Color(1,1,1);
                }
                
                //set animation
                player.animator.SetBool("GetItem", true);
                player.AddItemToInventory();
            }
            player.interacted = true; //next time enter interact state will exit.
        }
        else
        {
            if (player.inventory.newItemName != null)
            {
                //set animation
                player.animator.SetBool("GetItem", false);
                //get item pic
                player.itemSprite.sprite = null;
                player.inventory.newItemName = null;
            }

            player.interacted = false;
            player.ChangeState(player.idleState);
        }
        
    }

}
