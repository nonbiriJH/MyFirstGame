using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class DR2_OpenLGate : CutSceGenBasic
{
    [SerializeField]
    private CheckPointR2 checkPointR2;
    [SerializeField]
    private SignalSender interactSignal;
    [SerializeField]
    private SignalSender regPositionOnCheckPoint;
    [SerializeField]
    private GameObject dialogBox;
    [SerializeField]
    private string[] dialog;


    public override bool PlayCondition()
    {
        return checkPointR2.gateLogR2Talked && !checkPointR2.gateLogR2LOpenGate;
    }

    public override void StartWhenConditionMet()
    {
        interactSignal.SendSignal();
        dialogBox.GetComponent<Dialog>().dialog = dialog;
    }

    public override void UpdateDuringPlay()
    {

        if (playableDirector.state != PlayState.Playing)
        {
            interactSignal.SendSignal();
            checkPointR2.gateLogR2LOpenGate = true;
            regPositionOnCheckPoint.SendSignal();
        }
    }

}
