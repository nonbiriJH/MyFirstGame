using UnityEngine;

public class Interactables : MonoBehaviour
{
    [Header("Interactable Base Variable")]
    public bool playerInRange;
    public SignalSender enableContentHint;
    public SignalSender disableContentHint;
    //signal to end player interact state
    public SignalSender interactSignal;
    //dialogBox
    public GameObject dialogBox;

    public int interactStep = 0;// tracker of update steps.
    public int dialogBoxState;


    public virtual void StartDialog(string[] newDialog)
    {
        if (!dialogBox.activeInHierarchy
            && dialogBox.GetComponent<Dialog>().nInvoked == dialogBoxState)
        {
            dialogBox.GetComponent<Dialog>().dialog = newDialog;
            dialogBox.SetActive(true);
        }
    }

    public virtual void AddStep()
    {
        //If dialog finished but dialog state not sync, sync state and progress step.
        if (!dialogBox.activeInHierarchy
           && dialogBox.GetComponent<Dialog>().nInvoked > dialogBoxState)
        {
            interactStep += 1;
            dialogBoxState = dialogBox.GetComponent<Dialog>().nInvoked;
        }
    }


    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            enableContentHint.SendSignal();
            playerInRange = true;
            //reset dialog state
            dialogBoxState = dialogBox.GetComponent<Dialog>().nInvoked;
            //reset interaction step
            interactStep = 0;
        }
    }

    //Triggered by other events (dialogs/options...)
    public virtual void InteractEnd()
    {
        /*In final step, if dialog finished,
         reset state and send signal to end player interact state. */
        if (!dialogBox.activeInHierarchy)
        {
            interactStep = 0;
            interactSignal.SendSignal();//end interact state
        }
    }

    public void SimpleDialog (string[] dialog)
    {
        if(interactStep == 0)
        {
            if (playerInRange && Input.GetButtonDown("Attack"))
            {
                StartDialog(dialog);
            }
            AddStep();
        }
        if(interactStep == 1)
        {
            InteractEnd();
        }
    }

    public virtual void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            disableContentHint.SendSignal();
            playerInRange = false;
        }
    }
}
