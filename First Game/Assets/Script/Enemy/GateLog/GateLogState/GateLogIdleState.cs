using UnityEngine;

public class GateLogIdleState : GateLogState
{
    private float idleSecondBeforeSleepCountdown;

    public GateLogIdleState(GateLog gateLog) : base(gateLog)
    {
    }

    public override void BeginState()
    {
        gateLog.MoveObject(Vector2.zero);
        gateLog.animator.SetBool("Walk", false);
        idleSecondBeforeSleepCountdown = gateLog.idleSecondBeforeSleep;
    }

    public override void UpdateLogics()
    {
        if (gateLog.canInteract)
        {
            if (gateLog.playerInRange)//only when log.canInteract = true
            {
                gateLog.ChangeState(new GateLogInteractState(gateLog));
            }
            else
            {
                idleSecondBeforeSleepCountdown -= Time.deltaTime;
                if (idleSecondBeforeSleepCountdown <= 0)
                {
                    gateLog.animator.SetBool("WakeUp", false);
                }
            }
        }
        else
        {
            //detect if player in attack range
            gateLog.CheckAttackDistance();
            if (gateLog.attacking)
            {
                gateLog.ChangeState(new GateLogAttackState(gateLog));
            }
            else
            {
                idleSecondBeforeSleepCountdown -= Time.deltaTime;
                if (idleSecondBeforeSleepCountdown <= 0)
                {
                    gateLog.animator.SetBool("WakeUp", false);
                }
            }
        }
    }
}