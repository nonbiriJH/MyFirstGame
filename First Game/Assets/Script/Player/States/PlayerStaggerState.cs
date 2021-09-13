using System.Collections;
using UnityEngine;

public class PlayerStaggerState : State
{
    public float knockBackTime;
    //Constructor
    public PlayerStaggerState(Player player) : base(player)
    {
    }


    public override IEnumerator BeginStateCo()
    {
        yield return new WaitForSeconds(knockBackTime);
        player.ChangeState(player.idleState);
    }



}