using UnityEngine;

public class DealerInteractState : DealerState
{
    private Vector3 interactDirection;
    public DealerInteractState(Dealer dealer): base(dealer)
    {
        
    }

    public override void BeginState()
    {
        base.BeginState();
        interactDirection = dealer.target.position - dealer.transform.position;
        dealer.UpdateWalkAnimParameter(interactDirection);
        if (dealer.shopOnly)
        {
            dealer.ChangeInternalDialog(dealer.startDialogShopOnly, dealer.endDialogShopOnly);
        }
        else if (dealer.checkPointR1.buyBlade)
        {
            dealer.ChangeInternalDialog(dealer.startDialogAftBlade, dealer.endDialogAftBlade);
        }
        else
        {
            dealer.ChangeInternalDialog(dealer.startDialog, dealer.endDialog);
        }
    }

    public override void UpdateLogics()
    {
        if (dealer.playerInRange)
        {
            interactDirection = dealer.target.position - dealer.transform.position;
            dealer.UpdateWalkAnimParameter(interactDirection);
            ShopDialogProcess();
        }
        else
        {
            if (!dealer.shopOnly)
            {
                dealer.ChangeState(new DealerWalkState(dealer));
            }
            else
            {
                dealer.UpdateWalkAnimParameter(Vector2.down);//when shop only always interact state.
            }
        }
    }

    private void ShopDialogProcess()
    {
        if (dealer.interactStep == 0)
        {
            //Press button to start interact
            if (Input.GetButtonDown("Attack")) dealer.StartDialog(dealer.internalStartDialog);
            dealer.AddStep();
        }
        else if (dealer.interactStep == 1)//enable shop panel
        {
            dealer.shopPanel.SetActive(true);
            dealer.interactStep += 1;
        }

        else if (dealer.interactStep == 2)//when shop panel is set inactive again
        {
            if (!dealer.shopPanel.activeInHierarchy)//buy finish
            {
                if (dealer.itemQuantityLookup.GetItemNumber(dealer.triggerItem.itemName) > 0 && !dealer.checkPointR1.buyBlade)
                {
                    dealer.checkPointR1.buyBlade = true;
                    dealer.regPositionOnCheckPoint.SendSignal();
                    dealer.internalEndDialog = dealer.endDialogGetBlade;
                }

                dealer.StartDialog(dealer.internalEndDialog);
                dealer.AddStep();
            }
        }

        else if (dealer.interactStep == 3)
        {
            dealer.InteractEnd();
            if (!dealer.shopOnly)
            {
                dealer.ChangeState(new DealerWalkState(dealer));
            }
        }
    }
}
