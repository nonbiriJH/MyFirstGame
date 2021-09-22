using System.Collections;
using UnityEngine;

public class PlayerInteractState : State
{
    private bool interacted = false;

    //Constructor
    public PlayerInteractState(Player player) : base(player)
    {
    }

    public override void BeginState()
    {
        base.UpdateLogics();
        if (!interacted)
        {
            if (player.inventory.newItem != null)
            {
                //set animation
                player.animator.SetBool("GetItem", true);
                player.AddItemToInventory();
            }
            interacted = true; //next time enter interact state will exit.
        }
        else
        {
            if (player.inventory.newItem != null)
            {
                //set animation
                player.animator.SetBool("GetItem", false);
                //get item pic
                player.itemSprite.sprite = null;
                player.inventory.newItem = null;
            }
            
            interacted = false;
            player.ChangeState(player.idleState);
        }
        
    }

}
