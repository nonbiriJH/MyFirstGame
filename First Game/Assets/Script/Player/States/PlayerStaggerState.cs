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
        player.MoveObject(Vector2.zero);
        yield return new WaitForSeconds(knockBackTime);
        player.ChangeState(player.idleState);
    }



}