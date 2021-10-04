using System.Collections;
using UnityEngine;

public class ImoutoBedInteractState : ImoutoBedState
{
    private float awakeSecondCountDown;
    private bool doneAnim;
    public ImoutoBedInteractState(ImoutoBed imoutoBed) : base(imoutoBed)
    {
    }

    public override void BeginState()
    {
        awakeSecondCountDown = imoutoBed.awakeSecond;

        if (imoutoBed.wakeable)
        {
            imoutoBed.IsSleepInBed(false);
            imoutoBed.WakeUpTurnRigh(true);
        }
    }

    public override void UpdateLogics()
    {
        if (imoutoBed.playerInRange)
        {
            //Reset wake time
            awakeSecondCountDown = imoutoBed.awakeSecond;
            //If in bed turn right
            if (imoutoBed.wakeable)
            {
                if(imoutoBed.CurrentAnimName() == "BedIdleRight")
                {
                    imoutoBed.OneTimeDialog();
                }
            }
            else
            {
                imoutoBed.OneTimeDialog();
            }
        }
        else
        {
            awakeSecondCountDown -= Time.deltaTime;
            if (awakeSecondCountDown <= 0)
            {
                imoutoBed.WakeUpTurnRigh(false);
                imoutoBed.ChangeState(new ImoutoBedIdelState(imoutoBed));
            }
        }
    }
}