using System.Collections;
using UnityEngine;

public class GateLogStaggerState : GateLogState
{

    public float knockBackTime;

    public GateLogStaggerState(GateLog gateLog) : base(gateLog)
    {
    }

    public override IEnumerator BeginStateCo()
    {
        gateLog.MoveObject(Vector2.zero);
        gateLog.animator.SetBool("Walk", false);
        yield return new WaitForSeconds(knockBackTime);
        gateLog.ChangeState(new GateLogIdleState(gateLog));
    }
}