using UnityEngine;

public class KeyDoor : Interactables
{

    [Header("Checkpoint")]
    [SerializeField]
    private CheckPointR1 checkPointR1;
    [SerializeField]
    private SignalSender regPositionOnCheckPoint;

    [Header("Key Door Variable")]
    //Close Sprite
    public Sprite closeSprite;
    //Open Sprite
    public Sprite openSprite;
    //Required key
    public Item requiredKey;
    public Options options;
    public GameObject optionPanel;

    [Header("Dialog Variables")]
    public string[] startDialog;
    public string[] failDialog;
    public string[] successDialog;
    public string[] giveUpDialog;

    [Header("Utility")]
    //disable collider when open
    public BoxCollider2D physicCollide;
    //control sprite for door open
    private SpriteRenderer spriteRenderer;
    private bool success = false;// tracker of success/fail dialog

    private void Start()
    {
        spriteRenderer = gameObject.GetComponentInParent<SpriteRenderer>();
        if (!checkPointR1.openGate)
        {
            spriteRenderer.sprite = closeSprite;
        }
        else
        {
            spriteRenderer.sprite = openSprite;
            //diable collider
            physicCollide.enabled = false;
        }
    }

    private void Update()
    {
        if (playerInRange && !checkPointR1.openGate)
        {
            if (requiredKey.itemNumber >= 1) WithKeyInteraction();
            else NoKeyInteraction();
        }
    }


    private void NoKeyInteraction()
    {
        if (interactStep == 0)
        {
            if (Input.GetButtonDown("Attack")) StartDialog(startDialog);
            AddStep();
        }
        else if (interactStep == 1)
        {
            StartDialog(failDialog);
            AddStep();
        }

        else if (interactStep == 2)
        {
            InteractEnd();
        }
    }

    private void WithKeyInteraction()
    {
        if (interactStep == 0)
        {
            if (Input.GetButtonDown("Attack")) StartDialog(startDialog);
            AddStep();
        }
        else if (interactStep == 1)
        {
            //Stop Door Update
            interactStep = -1;
            //pop up options
            if (!optionPanel.activeInHierarchy)
            {
                OptionManager optionManager = optionPanel.GetComponent<OptionManager>();
                optionManager.options = options;
                optionManager.currentOptionState = new DoorOption(optionManager, this.gameObject);
                optionPanel.SetActive(true);
            }
            AddStep();
        }

        else if (interactStep == 2)
        {
            if (success) StartDialog(successDialog);
            else StartDialog(giveUpDialog);
            AddStep();
        }

        else if (interactStep == 3)
        {
            if (success)
            {
                //change sprite to open
                spriteRenderer.sprite = openSprite;
                //disable collider
                physicCollide.enabled = false;
                checkPointR1.openGate = true;
                regPositionOnCheckPoint.SendSignal();
            }
            InteractEnd();
        }
    }

    public void ChooseAction(int index)
    {
        //Resume Door Update
        interactStep = 2;
        if (index == 0)
        {
            //use key
            requiredKey.ReduceAmount();
            success = true;
            disableContentHint.SendSignal();
        }
        else
        {
            return;
        }
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (!checkPointR1.openGate)
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

    public override void OnTriggerExit2D(Collider2D other)
    {
        if (!checkPointR1.openGate)
        {
            base.OnTriggerExit2D(other);
        }
        else
        {
            playerInRange = false;
        }

    }

}
