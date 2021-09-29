using System.Collections;
using UnityEngine;

public class LogInteractState : LogState
{
    private bool wakeup;

    public LogInteractState(Log log) : base(log)
    {
    }


    public override IEnumerator BeginStateCo()
    {
        if (!log.animator.GetBool("WakeUp"))
        {
            wakeup = false;
            log.animator.SetBool("WakeUp", true);
            yield return new WaitForSeconds(.5f);
        }
        
        //sync current dialog state
        log.dialogBoxState = log.dialogBox.GetComponent<Dialog>().nInvoked;
        log.interactStep = 0;
        wakeup = true;
        log.PrepareDialog();
    }


    public override void UpdateLogics()
    {
        if (log.playerInRange)
        {
            //if not wake up do not back to idle
            if (wakeup)
            {
                Vector3 interactDirection = log.target.position - log.transform.position;
                log.UpdateWalkAnimParameter(interactDirection);
                log.OneTimeDialog(log.nextDialog);
            }

        }
        else
        {
            log.ChangeState(new LogIdleState(log));
        }
    }


    
}