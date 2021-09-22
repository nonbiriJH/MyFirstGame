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

    // Update is called once per frame
    public virtual void Update()
    {
        if (playerInRange && Input.GetButtonDown("Attack"))
        {
            Interact();
        }
    }

    public virtual void Interact()
    {

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

    public virtual void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            disableContentHint.SendSignal();
            playerInRange = false;
        }
    }
}
