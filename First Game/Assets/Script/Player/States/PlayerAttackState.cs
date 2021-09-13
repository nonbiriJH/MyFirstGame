using System.Collections;
using UnityEngine;

public class PlayerAttackState : State 
{
    private bool inAttack;
    //Constructor
    public PlayerAttackState(Player player) : base(player)
    {
    }


    public override IEnumerator BeginStateCo()
    {
        player.animator.SetBool("Attack", true);
        yield return new WaitForSeconds(.12f);//delay for finishing animation
        player.animator.SetBool("Attack", false); //do not enter attack again;
        player.ChangeState(player.idleState);
        
    }
}