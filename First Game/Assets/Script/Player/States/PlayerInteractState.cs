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
        Debug.Log(interacted);
        base.UpdateLogics();
        if (!interacted)
        {
            //set animation
            player.animator.SetBool("GetItem", true);
            player.AddItemToInventory();
            interacted = true;
        }
        else
        {
            //set animation
            player.animator.SetBool("GetItem", false);
            //get item pic
            player.itemSprite.sprite = null;
            interacted = false;
            player.ChangeState(player.idleState);
        }
        
    }

}
