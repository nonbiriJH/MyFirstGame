using UnityEngine;

public class SaintTree : Interactables
{
    [Header("Dialogs")]
    [SerializeField]
    string[] startDialog;
    [SerializeField]
    string[] arrowChangeDialog;
    [SerializeField]
    string[] endDialogOp0;
    [SerializeField]
    string[] endDialogOp1;
    [SerializeField]
    string[] endDialogOp2;
    [SerializeField]
    string[] normalDialog;

    [Header("Options")]
    [SerializeField]
    GameObject optionPanel;
    [SerializeField]
    Options options;

    [Header("Items")]
    [SerializeField]
    ItemQuantityLookup itemQuantityLookup;
    [SerializeField]
    Item drop;
    [SerializeField]
    Item arrow;
    [SerializeField]
    Item saintArrow;

    [Header("CheckPoint")]
    [SerializeField]
    CheckPointR2 checkPointR2;
    [SerializeField]
    SignalSender positionReg;

    private Animator animator;
    private string[] endDialog;


    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (checkPointR2.gateLogR2LOpenGate && !checkPointR2.getPureArrow) Interact();
        else SimpleDialog(normalDialog);
    }

    private void Interact()
    {
        if (interactStep == 0)
        {
            if (playerInRange && Input.GetButtonDown("Attack"))
            {
                StartDialog(startDialog);
            }
            AddStep();
        }
        if (interactStep == 1)
        {
            if (!animator.GetBool("CanGrow"))
            {
                animator.SetBool("CanGrow", true);
                itemQuantityLookup.ReduceAmount(drop.itemName);
            }
            
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("IdleLeaf"))
            {
                interactStep += 1;
            }
        }
        if (interactStep == 2)
        {
            itemQuantityLookup.ReduceAmount(arrow.itemName);
            itemQuantityLookup.IncreaseAmount(saintArrow.itemName);
            StartDialog(arrowChangeDialog);
            AddStep();
        }
        if(interactStep == 3)
        {
            GiveOption();
        }
        if (interactStep == 4)
        {
            if (!optionPanel.activeInHierarchy)
            {
                StartDialog(endDialog);
                AddStep();
            }
        }
        if (interactStep == 5)
        {
            checkPointR2.getPureArrow = true;
            positionReg.SendSignal();
            InteractEnd();
        }
    }

    //open door method by door type
    private void GiveOption()
    {
        //Stop Update
        interactStep = -1;
        //pop up options
        if (!optionPanel.activeInHierarchy)
        {
            OptionManager optionManager = optionPanel.GetComponent<OptionManager>();
            optionManager.options = options;
            optionManager.currentOptionState = new SaintTreeOption(optionManager, this);
            optionPanel.SetActive(true);
        }
    }


    public void ChooseAction(int index)
    {
        if (index == 0) endDialog = endDialogOp0;
        else if (index == 1) endDialog = endDialogOp1;
        else if (index == 2) endDialog = endDialogOp2;
        //Resume interaction step
        interactStep = 4;
    }
}
