using System.Collections;
using UnityEngine;

public class ImoutoBedIdelState : ImoutoBedState
{
    private bool doneAnim;

    //set animation and wait state changes
    public ImoutoBedIdelState(ImoutoBed imoutoBed) : base(imoutoBed)
    {
    }

    public override IEnumerator BeginStateCo()
    {
        doneAnim = false;
        //if awake wait for animation to sleep
        if (!imoutoBed.AnimatorBoolValue("Sleep"))
        {
            imoutoBed.IsSleepInBed(true);
            yield return new WaitForSeconds(1.5f);
        }
        doneAnim = true;
    }

    public override void UpdateLogics()
    {

        if (doneAnim)
        {
            if (imoutoBed.playerInRange)
            {
                imoutoBed.ChangeState(new ImoutoBedInteractState(imoutoBed));
            }
        }
        
    }
}