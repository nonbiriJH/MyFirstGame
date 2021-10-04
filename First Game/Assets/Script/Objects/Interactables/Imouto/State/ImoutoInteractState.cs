using UnityEngine;

public class ImoutoInteractState : ImoutoState
{
    private string[] nextDialog;
    private bool happyDialog;

    public ImoutoInteractState(Imouto imouto) : base(imouto)
    {
    }

    public override void BeginState()
    {
        imouto.StopWalk();
        imouto.FacePlayer();
    }

    public override void UpdateLogics()
    {
        if (imouto.playerInRange)
        {
            imouto.FacePlayer();
            imouto.OneTimeDialog();
        }
        else
        {
            imouto.ChangeState(new ImoutoIdleState(imouto));
        }
        
    }
}