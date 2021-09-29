using UnityEngine;

public class ProtectorInteractState : ProtectorState
{

    //Constructor, Link a StateMachine instance with a Player instance
    public ProtectorInteractState(Protector protector) : base(protector)
    {
    }


    public override void BeginState()
    {
        base.BeginState();
        protector.UpdateWalkAnimParameter(protector.interactDirection);
        //sync current dialog state
        protector.dialogBoxState = protector.dialogBox.GetComponent<Dialog>().nInvoked;
        protector.interactStep = 0;
    }

    public override void UpdateLogics()
    {
        if (protector.playerInWarningZone)
        {
            protector.UpdateWalkAnimParameter(new Vector2(-1,0));
            OneTimeDialog(protector.warnZoneDialog);

        }
        else if (protector.playerInDangerZone)
        {
            protector.UpdateWalkAnimParameter(new Vector2(-1, 0));
            protector.attacking = true;
            OneTimeDialog(protector.dangerZoneDialog);
        }
        else if (protector.playerInRange)
        {
            NormalInteract();
        }
        else
        {
            protector.ChangeState(new ProtectorWalkState(protector));
        }
    }

    public override void ExitState()
    {
        base.ExitState();
        protector.playerInDangerZone = false;
        protector.playerInWarningZone = false;
    }

    private void OneTimeDialog(string[] dialog)
    {
        if (protector.interactStep == 0)
        {
            if (!protector.dialogBox.activeInHierarchy
              && protector.dialogBox.GetComponent<Dialog>().nInvoked == protector.dialogBoxState)//same as start dialog condition
            {
                //player enter interact state
                protector.interactSignal.SendSignal();
            }
            protector.StartDialog(dialog);
            protector.AddStep();
        }
        else if (protector.interactStep == 1
            && !protector.dialogBox.activeInHierarchy)
        {
            //player quite interact state
            protector.InteractEnd();
            protector.ChangeState(new ProtectorWalkState(protector));
        }
    }
   


    private void NormalInteract()
    {
        if (protector.interactStep == 0)
        {
            //Press button to start interact
            if (Input.GetButtonDown("Attack")) protector.StartDialog(protector.normalStartDialog);
            protector.AddStep();
        }
        else if (protector.interactStep == 1)//enable shop panel
        {
            GiveOption();
        }

        else if (protector.interactStep == 2)//when shop panel is set inactive again
        {
            if (!protector.optionPanel.activeInHierarchy)
            {
                protector.StartDialog(protector.endDialog);
                protector.AddStep();
            }
        }

        else if (protector.interactStep == 3)
        {
            protector.InteractEnd();
            if (protector.isSignState)
            {
                protector.ChangeState(new ProtectorSignState(protector));
            }
            else
            {
                protector.ChangeState(new ProtectorWalkState(protector));
            }
        }
    }

    //open door method by door type
    private void GiveOption()
    {
        //Stop Door Update
        protector.interactStep = -1;
        //pop up options
        if (!protector.optionPanel.activeInHierarchy)
        {
            OptionManager optionManager = protector.optionPanel.GetComponent<OptionManager>();
            optionManager.options = protector.options;
            optionManager.currentOptionState = new ProtectorOption(optionManager, protector.gameObject);
            protector.optionPanel.SetActive(true);
        }
    }


}
