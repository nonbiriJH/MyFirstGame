using UnityEngine;

public class LogIdleState : LogState
{
    private float idleSecondBeforeSleepCountdown;

    public LogIdleState(Log log) : base(log)
    {
    }

    public override void BeginState()
    {
        log.MoveObject(Vector2.zero);
        log.animator.SetBool("Walk", false);
        idleSecondBeforeSleepCountdown = log.idleSecondBeforeSleep;
    }

    public override void UpdateLogics()
    {
        if (log.canInteract)
        {
            if (log.playerInRange)//only when log.canInteract = true
            {
                log.ChangeState(new LogInteractState(log));
            }
            else
            {
                idleSecondBeforeSleepCountdown -= Time.deltaTime;
                if (idleSecondBeforeSleepCountdown <= 0)
                {
                    log.animator.SetBool("WakeUp", false);
                }
            }
        }
        else
        {
            //detect if player in attack range
            log.CheckAttackDistance();
            if (log.attacking)
            {
                log.ChangeState(new LogAttackState(log));
            }
            else
            {
                idleSecondBeforeSleepCountdown -= Time.deltaTime;
                if (idleSecondBeforeSleepCountdown <= 0)
                {
                    log.animator.SetBool("WakeUp", false);
                }
            }
        }



    }
}