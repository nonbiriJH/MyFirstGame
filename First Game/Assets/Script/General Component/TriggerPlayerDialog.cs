using UnityEngine;

public class TriggerPlayerDialog : MonoBehaviour
{
    [SerializeField]
    public bool triggered;
    [SerializeField]
    private string[] dialog;
    [SerializeField]
    private SignalSender interactSignal;

    private GameObject dialogBox;
    private int dialogBoxState;
    private int interactStep;
    private bool playerInRange;

    public virtual void Start()
    {
        dialogBox = Resources.FindObjectsOfTypeAll<Dialog>()[0].gameObject;
        playerInRange = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void Update()
    {
        if (playerInRange && !triggered)
        {
            OneTimeDialog(dialog);
        }
    }

    private void OneTimeDialog(string[] dialog)
    {
        if (interactStep == 0)
        {
            interactSignal.SendSignal();//start interact state
            interactStep += 1;
        }
        else if (interactStep == 1)
        {
            StartDialog(dialog);
            AddStep();
        }
        else if (interactStep == 2
            && !dialogBox.activeInHierarchy)
        {
            //player quite interact state
            InteractEnd();
            triggered = true;
            PreDestroy();
            Destroy(gameObject);
        }
    }

    public virtual void PreDestroy()
    {
    }

    private void StartDialog(string[] newDialog)
    {
        if (!dialogBox.activeInHierarchy
            && dialogBox.GetComponent<Dialog>().nInvoked == dialogBoxState)
        {
            dialogBox.GetComponent<Dialog>().dialog = newDialog;
            dialogBox.SetActive(true);
        }
    }

    private void AddStep()
    {
        //If dialog finished but dialog state not sync, sync state and progress step.
        if (!dialogBox.activeInHierarchy
           && dialogBox.GetComponent<Dialog>().nInvoked > dialogBoxState)
        {
            interactStep += 1;
            dialogBoxState = dialogBox.GetComponent<Dialog>().nInvoked;
        }
    }

    //Triggered by other events (dialogs/options...)
    private void InteractEnd()
    {
        /*In final step, if dialog finished,
         reset state and send signal to end player interact state. */
        if (!dialogBox.activeInHierarchy)
        {
            interactStep = 0;
            interactSignal.SendSignal();//end interact state
        }
    }

}
