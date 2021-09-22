using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dealer : Interactables
{
    [Header("Base Dialog Variables")]
    //Dialogs
    public string[] startDialog;
    public string[] endDialog;
    public GameObject shopPanel;

    public override void Update()
    {
        if (playerInRange)
        {

            if (interactStep == 0)
            {
                //Press button to start interact
                if (Input.GetButtonDown("Attack")) StartDialog(startDialog);
                AddStep();
            }
            else if (interactStep == 1)//enable shop panel
            {
                shopPanel.SetActive(true);
                interactStep += 1;
            }

            else if (interactStep == 2)//when shop panel is set inactive again
            {
                if (!shopPanel.activeInHierarchy)
                {
                    StartDialog(endDialog);
                    AddStep();
                }
            }

            else if (interactStep == 3)
            {
                InteractEnd();
            }

        }

    }

    public virtual void StartDialog(string[] newDialog)
    {
        if (!dialogBox.activeInHierarchy
            && dialogBox.GetComponent<Dialog>().nInvoked == dialogBoxState)
        {
            dialogBox.GetComponent<Dialog>().dialog = newDialog;
            dialogBox.SetActive(true);
        }
    }
}
