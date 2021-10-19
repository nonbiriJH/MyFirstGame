using UnityEngine;

public class ProtectorSignState : ProtectorState
{
    public ProtectorSignState(Protector protector) : base(protector)
    {
    }

    public override void BeginState()
    {
        protector.UpdateWalkAnimParameter(protector.interactDirection);
        protector.dialogBoxState = protector.dialogBox.GetComponent<Dialog>().nInvoked;
        protector.interactStep = 0;
    }

    public override void UpdateLogics()
    {
        if (protector.playerInRange)
        {
            OneTimeDialog(protector.afterYesDialog);
        }
        else
        {
            protector.ChangeState(new ProtectorWalkState(protector));
        }
    }

    private void OneTimeDialog(string[] dialog)
    {
        if (protector.interactStep == 0) //start preGetDialog
        {
            if (Input.GetButtonDown("Attack"))
            {
                protector.StartDialog(dialog);
            }
            protector.AddStep();
        }
        else if (protector.interactStep == 1)
        {
            //player quite interact state
            protector.InteractEnd();
            protector.ChangeState(new ProtectorWalkState(protector));
        }
    }
}