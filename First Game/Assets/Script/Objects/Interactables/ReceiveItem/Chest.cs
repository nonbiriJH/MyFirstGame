using UnityEngine;

public class Chest : Interactables
{
    [Header("Chest Variables")]
    public Item content;
    public Inventory inventory;//pass chest item to inventory new item

    [Header("Checkpoint")]
    [SerializeField]
    private CheckPointR1 checkPointR1;
    [SerializeField]
    private SignalSender regPositionOnCheckPoint;

    private Animator anim;

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (!checkPointR1.getKey)
        {
            //if not interacted, trun on playerInRage and send contentHint signal.
            base.OnTriggerEnter2D(other);
        }
        else
        {
            playerInRange = false;
        }
    }

    public override void OnTriggerExit2D(Collider2D other)
    {
        if (!checkPointR1.getKey)
        {
            base.OnTriggerExit2D(other);
        }
        else
        {
            playerInRange = false;
        }

    }

    // Start is called before the first frame update
    private void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        //change animation; Mainly for senece change
        anim.SetBool("open", checkPointR1.getKey);
    }

    private void Update()
    {
        if (playerInRange && !checkPointR1.getKey)
        {
            if (interactStep == 0)
            {
                if (Input.GetButtonDown("Attack"))
                {
                    //open dialog box
                    dialogBox.GetComponent<Dialog>().dialog = content.getDescription;
                    dialogBox.SetActive(true);
                    //change animation
                    anim.SetBool("open", true);
                    //pass chest item to inventory new item
                    inventory.newItemName = content.itemName;
                }

                AddStep();
            }

            else if (interactStep == 1)//set bool value, exit interact
            {
                disableContentHint.SendSignal();
                checkPointR1.getKey = true;
                regPositionOnCheckPoint.SendSignal();
                //send signal to trigger player get event
                interactSignal.SendSignal();
            }

        }
    }
}
