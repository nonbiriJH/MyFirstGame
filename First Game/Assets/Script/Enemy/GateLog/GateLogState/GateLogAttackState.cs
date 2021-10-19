using System.Collections;
using UnityEngine;
public class GateLogAttackState : GateLogState
{
    private bool wakeup;

    public GateLogAttackState(GateLog gateLog) : base(gateLog)
    {
    }


    public override IEnumerator BeginStateCo()
    {
        wakeup = false;
        gateLog.animator.SetBool("WakeUp", true);
        yield return new WaitForSeconds(.5f);
        wakeup = true;
        gateLog.animator.SetBool("Walk", true);
    }

    public override void UpdateLogics()
    {
        //detect if player in attack range
        gateLog.CheckAttackDistance();
        if (gateLog.attacking && wakeup)
        {
            gateLog.AttackMove();
        }
        else if (!gateLog.attacking)
        {
            gateLog.ChangeState(new GateLogIdleState(gateLog));
        }
    }

}