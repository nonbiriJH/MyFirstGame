using System.Collections;
using UnityEngine;
public class LogAttackState : LogState
{
    private bool wakeup;

    public LogAttackState(Log log) : base(log)
    {
    }


    public override IEnumerator BeginStateCo()
    {
        wakeup = false;
        log.animator.SetBool("WakeUp", true);
        yield return new WaitForSeconds(.5f);
        wakeup = true;
        log.animator.SetBool("Walk", true);
    }

    public override void UpdateLogics()
    {
        //detect if player in attack range
        log.CheckAttackDistance();
        if (log.attacking && wakeup)
        {
            log.AttackMove();
        }
        else if (!log.attacking)
        {
            log.ChangeState(new LogIdleState(log));
        }
    }

}