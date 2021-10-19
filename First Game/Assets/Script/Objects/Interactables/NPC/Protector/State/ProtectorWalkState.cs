using UnityEngine;

public class ProtectorWalkState : ProtectorState
{
    private float roundingError = 0.1f;

    //Constructor, Link a StateMachine instance with a Player instance
    public ProtectorWalkState(Protector protector) : base(protector)
    {
    }

    public override void BeginState()
    {
        base.BeginState();
        protector.animator.SetBool("Walk", true);
        float petrolTargetDistance = Vector3.Distance(protector.patrolTarget, protector.transform.position);

        if (petrolTargetDistance < roundingError)
        {
            protector.ChangeTarget();
            protector.ChangeState(new ProtectorIdelState(protector));
        }
        else
        {
            //move to patrol target
            Vector2 direction = protector.patrolTarget
                - new Vector2(protector.transform.position.x, protector.transform.position.y);
            protector.MoveObject(direction);
            //update animation
            protector.UpdateWalkAnimParameter(direction);
        }
    }


    public override void UpdateLogics()
    {
        base.UpdateLogics();

        if (!protector.attacking)
        {
            if (protector.playerInRange && protector.checkPointR2.helpYellow)
            {
                protector.ChangeState(new ProtectorSignState(protector));
            }
            else if (protector.playerInRange || protector.playerInWarningZone || protector.playerInDangerZone)
            {
                protector.ChangeState(new ProtectorInteractState(protector));
            }
            else
            {
                float petrolTargetDistance = Vector3.Distance(protector.patrolTarget, protector.transform.position);
                if (petrolTargetDistance < roundingError)
                {
                    protector.ChangeTarget();
                    protector.ChangeState(new ProtectorIdelState(protector));
                }
            }
        }
        else
        {
            protector.ChangeState(new ProtectorAttackState(protector));
        }
    }

    public override void ExitState()
    {
        base.ExitState();
        protector.animator.SetBool("Walk", false);
        protector.MoveObject(Vector2.zero);
    }

}
