using UnityEngine;

public class DR2_OpenRRoad : CutSceGeneric
{
    [SerializeField]
    private CheckPointR2 checkPointR2;
    [SerializeField]
    private string[] wakeUpDialog;
    [SerializeField]
    private string[] endDialog;
    [SerializeField]
    private SignalSender interactSignal;
    [SerializeField]
    private SignalSender positionReg;

    private void Start()
    {
        if (checkPointR2.gateLogR2Move)
        {
            this.gameObject.SetActive(false);
        }
    }


    public override bool PlayCondition()
    {
        return checkPointR2.gateLogR2RTalked && !checkPointR2.gateLogR2Move;
    }

    public override void StartWhenConditionMet()
    {
        interactSignal.SendSignal();
        base.StartWhenConditionMet();
    }

    public override void EndPlayHandle()
    {
        SyncObjectPosition(actors[0], replacedGameObjects[0]);
        checkPointR2.gateLogR2Move = true;
        positionReg.SendSignal();
        interactSignal.SendSignal();
    }


    private void Awake()
    {
        dialogs.Add(wakeUpDialog);
        dialogs.Add(endDialog);
    }


}
