using System.Collections;
using UnityEngine;

public class LogStaggerState : LogState
{

    public float knockBackTime;

    public LogStaggerState(Log log) : base(log)
    {
    }

    public override IEnumerator BeginStateCo()
    {
        log.MoveObject(Vector2.zero);
        log.animator.SetBool("Walk", false);
        yield return new WaitForSeconds(knockBackTime);
        log.ChangeState(new LogIdleState(log));
    }
}