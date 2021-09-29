using UnityEngine;

public class DealerAfterBoss : DealerState
{
    private bool firstDialogFinished;
    private Vector3 interactDirection;

    public DealerAfterBoss(Dealer dealer) : base(dealer)
    {
    }

    public override void BeginState()
    {
        dealer.internalStartDialog = dealer.bossDieDialog;
        firstDialogFinished = false;
    }

    public override void UpdateLogics()
    {
        if (dealer.playerInRange)
        {
            dealer.disableContentHint.SendSignal();
            interactDirection = dealer.target.position - dealer.transform.position;
            dealer.UpdateWalkAnimParameter(interactDirection);
            if (firstDialogFinished)
            {

                dealer.internalStartDialog = dealer.bossDieDialogPost;
            }
            OneTimeDialog_BossDie(dealer.internalStartDialog);
        }
    }

    private void OneTimeDialog_BossDie(string[] dialog)
    {
        if (dealer.interactStep == 0)
        {
            dealer.interactSignal.SendSignal();
            dealer.interactStep += 1;
        }

        if(dealer.interactStep == 1)
        {
            dealer.StartDialog(dialog);
            dealer.AddStep();
        }

        else if (dealer.interactStep == 2
            && !dealer.dialogBox.activeInHierarchy)
        {
            //player quite interact state
            dealer.InteractEnd();
            dealer.playerInRange = false;
            firstDialogFinished = true;
        }
    }
}