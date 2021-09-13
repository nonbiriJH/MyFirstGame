using UnityEngine;

public class PlayerIdleState:State
{
    private Vector2 inputDirection;

    //Constructor, Link a StateMachine instance with a Player instance
    public PlayerIdleState(Player player) : base(player)
    {
    }


    public override void BeginState()
    {
        base.BeginState();
        inputDirection = Vector2.zero;
    }

    public override void HandleInput()
    {
        base.HandleInput();
        inputDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    public override void UpdateLogics()
    {
        base.UpdateLogics();
        if (inputDirection.magnitude != 0)
        {
            player.ChangeState(player.walkState);
        }
        else if (Input.GetButtonDown("Attack"))
        {
            player.ChangeState(player.attackState);
        }
        else if (Input.GetButtonDown("Ability"))
        {
            player.ChangeState(player.abilityState);
        }
    }

}