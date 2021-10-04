public class RMCS02R1ED : CutSceGeneric
{
    public CheckPointR1 checkPointR1;
    public string[] singleDialog;
    public string[] endDialog;
    public string[] dieDialog;


    public override bool PlayCondition()
    {
        return checkPointR1.getApple && !checkPointR1.endBad;
    }


    public override void EndPlayHandle()
    {
        SyncObjectPosition(actors[0], replacedGameObjects[0]);
        SyncObjectPosition(actors[1], replacedGameObjects[1]);
    }


    private void Awake()
    {
        dialogs.Add(singleDialog);
        dialogs.Add(endDialog);
        dialogs.Add(dieDialog);
    }

}
