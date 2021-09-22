using UnityEngine;

public class Single3StepInteract : SingleInteract
{
    [Header("Base Dialog Variables")]
    //Dialogs
    public string[] startDialog;
    public string[] successDialog;
    public string[] failDialog;
    public bool success = false;// tracker of success/fail dialog

    public override void Update()
    {
        if (playerInRange && !interacted.runtimeValue)
        {

            if (interactStep == 0)
            {
                //Press button to start interact
                if (Input.GetButtonDown("Attack")) StartDialog(startDialog);
                AddStep();
            }
            else if (interactStep == 1)
            {
                InteractApply();
                AddStep();
            }

            else if (interactStep == 2)
            {
                if (success) InteractSuccess();
                else InteractFail();

                AddStep();
            }

            else if (interactStep == 3)
            {
                if (success) interacted.runtimeValue = true;
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


    public virtual void InteractApply()
    {
        success = true;
        disableContentHint.SendSignal();
        interactStep += 1;
    }

    public virtual void InteractFail()
    {
        StartDialog(failDialog);
    }

    public virtual void InteractSuccess()
    {
        StartDialog(successDialog);
    }


    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (!interacted.runtimeValue)
        {
            //if not interacted, trun on playerInRage and send contentHint signal.
            base.OnTriggerEnter2D(other);
            //Add reset dialog state
            dialogBoxState = dialogBox.GetComponent<Dialog>().nInvoked;
        }
        else
        {
            playerInRange = false;
        }
    }
}
