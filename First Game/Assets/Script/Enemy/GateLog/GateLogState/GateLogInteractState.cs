using System.Collections;
using UnityEngine;

public class GateLogInteractState : GateLogState//for access checkpoints
{
    private bool wakeup;

    public GateLogInteractState(GateLog gateLog) : base(gateLog)
    {
    }


    public override IEnumerator BeginStateCo()
    {
        if (!gateLog.animator.GetBool("WakeUp"))
        {
            wakeup = false;
            gateLog.animator.SetBool("WakeUp", true);
            yield return new WaitForSeconds(.5f);
        }

        //sync current dialog state
        gateLog.dialogBoxState = gateLog.dialogBox.GetComponent<Dialog>().nInvoked;
        gateLog.interactStep = 0;
        wakeup = true;
        gateLog.PrepareDialog();
    }


    public override void UpdateLogics()
    {
        if (gateLog.playerInRange)
        {
            //if not wake up do not back to idle
            if (wakeup)
            {
                Vector3 interactDirection = gateLog.target.position - gateLog.transform.position;
                gateLog.UpdateWalkAnimParameter(interactDirection);
                gateLog.OneTimeDialog(gateLog.nextDialog);
            }

        }
        else
        {
            gateLog.ChangeState(new GateLogIdleState(gateLog));
        }
    }


}