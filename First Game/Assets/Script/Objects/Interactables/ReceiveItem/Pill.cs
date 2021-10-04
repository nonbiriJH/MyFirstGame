using UnityEngine;

public class Pill : Interactables
{
    [SerializeField]
    private Item pill;
    [SerializeField]
    private Inventory inventory;//pass item to inventory new item
    [SerializeField]
    private string[] preGetDialog;

    [Header("Checkpoint")]
    [SerializeField]
    private CheckPointR1 checkPointR1;
    [SerializeField]
    private SignalSender regPositionOnCheckPoint;

    private void Start()
    {
        dialogBox = Resources.FindObjectsOfTypeAll<Dialog>()[0].gameObject;
    }

    void Update()
    {
        if (playerInRange)
        {
            if (interactStep == 0) //start preGetDialog
            {
                if (Input.GetButtonDown("Attack"))
                {
                    StartDialog(preGetDialog);
                }

                AddStep();
            }

            else if (interactStep == 1)//finish preGet
            {
                interactSignal.SendSignal();//end interact state
                disableContentHint.SendSignal();//diable contenthint
                inventory.newItem = pill;//add item to Inventory

                //checkpoint
                checkPointR1.getApple = true;
                regPositionOnCheckPoint.SendSignal();

                interactSignal.SendSignal();//in interact state again
                interactStep += 1;//add step
                dialogBoxState = dialogBox.GetComponent<Dialog>().nInvoked;//prepare launching next dialog;
            }

            else if (interactStep == 2)
            {
                StartDialog(pill.getDescription);//start get dialog
                //make item disappear;
                Color tmpColor = gameObject.GetComponent<SpriteRenderer>().color;
                tmpColor.a = 0;
                gameObject.GetComponent<SpriteRenderer>().color = tmpColor;
                AddStep();
            }

            else if (interactStep == 3)//set bool value, exit interact
            {
                if (!dialogBox.activeInHierarchy)
                {
                    interactSignal.SendSignal();//end interact state
                    Destroy(this.gameObject);//remove game object
                }
            }

        }
    }
}
