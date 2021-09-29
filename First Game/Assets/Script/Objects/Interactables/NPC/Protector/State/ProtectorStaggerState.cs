using System.Collections;
using UnityEngine;

public class ProtectorStaggerState : ProtectorState
{
    public float knockBackTime;

    //Constructor, Link a StateMachine instance with a Player instance
    public ProtectorStaggerState(Protector protector) : base(protector)
    {
    }

    public override IEnumerator BeginStateCo()
    {
        protector.MoveObject(Vector2.zero);
        protector.animator.SetBool("Walk", false);
        yield return new WaitForSeconds(knockBackTime);
        protector.ChangeState(new ProtectorIdelState(protector));
    }
}

