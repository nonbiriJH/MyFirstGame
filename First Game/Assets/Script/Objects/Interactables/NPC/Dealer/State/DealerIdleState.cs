using UnityEngine;

public class DealerIdleState : DealerState
{
    private float staySecondCountDown;

    //Constructor, Link a StateMachine instance with a Player instance
    public DealerIdleState(Dealer dealer) : base(dealer)
    {
    }


    public override void BeginState()
    {
        base.BeginState();
        staySecondCountDown = dealer.staySecond;
    }


    public override void UpdateLogics()
    {
        base.UpdateLogics();
        staySecondCountDown -= Time.deltaTime;

        //During shop mode
        if (dealer.playerInRange)
        {
            dealer.ChangeState(new DealerInteractState(dealer));
        }
        else if(staySecondCountDown <= 0)
        {
            dealer.ChangeState(new DealerWalkState(dealer));
        }
    }
}
