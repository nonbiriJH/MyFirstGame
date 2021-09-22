using System.Collections;
using UnityEngine;

public class PlayerAbilityState : State
{
    private Vector2 currentDirection;

    //Constructor
    public PlayerAbilityState(Player player) : base(player)
    {
    }


    public override IEnumerator BeginStateCo()
    {
        if(player.currentAbility.currentAbility != null)
        {
            //get facing direction
            currentDirection.x = player.animator.GetFloat("MoveX");
            currentDirection.y = player.animator.GetFloat("MoveY");
            //instantiate ability
            player.currentAbility.currentAbility.Ability(player.transform.position
                , currentDirection
                , player.gameObject);
            //wait ability finish
            yield return new WaitForSeconds(player.currentAbility.currentAbility.duartion);
        }
        
        //back to idle
        player.ChangeState(player.idleState);
    }

}