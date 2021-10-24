using UnityEngine;

public class ProtectorInteractState : ProtectorState
{
    private bool hasGot;

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
            if (!protector.helpYellow)
            {
                NormalInteract();
            }
            else if(!protector.gotArrow)
            {
                hasGot = GiveItem(protector.arrow);
                if (hasGot)
                {
                    protector.gotArrow = true;
                }
            }
            else if (!protector.gotDrop)
            {
                hasGot = GiveItem(protector.pureDrop);
                if (hasGot)
                {
                    protector.gotDrop = true;
                }
            }
            else if (!protector.gotKey)
            {
                hasGot = GiveItem(protector.key);
                if (hasGot)
                {
                    protector.gotKey = true;
                    protector.checkPointR2.helpYellow = true;
                }
            }
            else
            {
                protector.ChangeState(new ProtectorWalkState(protector));
            }
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
            if (!protector.answerYes)
            {
                protector.ChangeState(new ProtectorWalkState(protector));
            }
            else
            {
                protector.helpYellow = true;
                protector.regPositionOnCheckPoint.SendSignal();//reg player position for load
            }
        }
    }

    private bool GiveItem(Item item)
    {
        if (protector.interactStep == 0)
        {
            protector.playerInventory.newItemName = item.itemName;//add item to Inventory
            protector.interactSignal.SendSignal();//in interact state again
            protector.interactStep += 1;//add step
            return false;
        }
        else if (protector.interactStep == 1)
        {
            protector.StartDialog(item.getDescription);
            protector.AddStep();
            return false;
        }
        else if (protector.interactStep == 2)
        {
            protector.InteractEnd();
            return true;
        }
        else
        {
            return false;
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
