using System.Collections;
using UnityEngine;

public class DealerStaggerState: DealerState
{
    public float knockBackTime;

    //Constructor, Link a StateMachine instance with a Player instance
    public DealerStaggerState(Dealer dealer) : base(dealer)
    {
    }

    public override IEnumerator BeginStateCo()
    {
        dealer.StopObject();
        yield return new WaitForSeconds(knockBackTime);
        dealer.ChangeState(new DealerAttackState(dealer));
    }
}