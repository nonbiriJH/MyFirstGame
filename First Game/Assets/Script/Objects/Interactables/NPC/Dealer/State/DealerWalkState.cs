using UnityEngine;

public class DealerWalkState : DealerState
{
    private float roundingError = 0.1f;

    public DealerWalkState(Dealer dealer): base(dealer)
    {

    }

    public override void BeginState()
    {
        base.BeginState();
        dealer.animator.SetBool("Walk", true);
        float petrolTargetDistance = Vector3.Distance(dealer.patrolTarget, dealer.transform.position);

        if (petrolTargetDistance < roundingError)
        {
            dealer.ChangeTarget();
            dealer.ChangeState(new DealerIdleState(dealer));
        }
        else
        {
            //move to patrol target
            Vector2 direction = dealer.patrolTarget
                - new Vector2(dealer.transform.position.x, dealer.transform.position.y);
            dealer.MoveObject(direction);
            //update animation
            dealer.UpdateWalkAnimParameter(direction);
        }
    }


    public override void UpdateLogics()
    {
        base.UpdateLogics();

        if (dealer.playerInRange)
        {
            dealer.ChangeState(new DealerInteractState(dealer));
        }
        else
        {
            float petrolTargetDistance = Vector3.Distance(dealer.patrolTarget, dealer.transform.position);
            if (petrolTargetDistance < roundingError)
            {
                dealer.ChangeTarget();
                dealer.ChangeState(new DealerIdleState(dealer));
            }
        }
    }

    public override void ExitState()
    {
        base.ExitState();
        dealer.animator.SetBool("Walk", false);
        dealer.MoveObject(Vector2.zero);
    }

}
