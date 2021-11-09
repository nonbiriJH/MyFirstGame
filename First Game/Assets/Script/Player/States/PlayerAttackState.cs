using System.Collections;
using UnityEngine;

public class PlayerAttackState : State 
{
    private bool inAttack;
    private Vector2 currentDirection;
    //Constructor
    public PlayerAttackState(Player player) : base(player)
    {
    }


    public override IEnumerator BeginStateCo()
    {

        if (player.evilMode)
        {
            //get facing direction
            currentDirection.x = player.animator.GetFloat("MoveX");
            currentDirection.y = player.animator.GetFloat("MoveY");
            player.currentAbility.currentAbility.AbilityOnAttackState(player.transform.position
                , currentDirection
                , player.gameObject);
        }
        else
        {
            player.animator.SetBool("Attack", true);
            player.soundManager.PlaySound("Attack");
            yield return new WaitForSeconds(.12f);//delay for finishing animation
            player.animator.SetBool("Attack", false); //do not enter attack again;
        }

        if (player.interacted)
        {
            //On interaction triggered by attack, back to interact state.
            player.interacted = false;
            player.ChangeState(player.interactState);
        }
        else
        {
            player.ChangeState(player.idleState);
        }
        
    }
}