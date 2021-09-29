using UnityEngine;

public class PlayerWalkState : State
{
    //Constructor
    public PlayerWalkState(Player player) : base(player)
    {
    }

    private Vector2 inputDirection;

    public override void BeginState()
    {
        base.BeginState();
        player.animator.SetBool("Walking", true);
    }

    public override void HandleInput()
    {
        base.HandleInput();
        inputDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    public override void UpdateLogics()
    {
        base.UpdateLogics();

        Transform contentHintTrans = player.gameObject.transform.Find("ContentHint");
        if (inputDirection.magnitude == 0)
        {
            //When no input. no change anim para, change state.
            player.ChangeState(player.idleState);
        }
        else if (Input.GetButtonDown("Attack") && contentHintTrans.gameObject.activeInHierarchy)
        {
            player.ChangeState(player.interactState);
        }
        //Transit to Attack
        else if (Input.GetButtonDown("Attack"))
        {
            player.ChangeState(player.attackState);
        }
        //Transit to Ability
        else if (Input.GetButtonDown("Ability"))
        {
            player.ChangeState(player.abilityState);
        }
        //Finally if no input, keep walking
        else
        {
            player.animator.SetFloat("MoveX", inputDirection.x);
            player.animator.SetFloat("MoveY", inputDirection.y);
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
        player.MoveObject(inputDirection);
    }

    public override void ExitState()
    {
        base.ExitState();
        player.MoveObject(Vector2.zero);//stop moving
        player.animator.SetBool("Walking", false);//stop anim
    }
}