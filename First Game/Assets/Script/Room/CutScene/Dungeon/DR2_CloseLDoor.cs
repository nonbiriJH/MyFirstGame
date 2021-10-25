using UnityEngine;

public class DR2_CloseLDoor : CutSceGeneric
{
    [SerializeField]
    private CheckPointR2 checkPointR2;
    [SerializeField]
    private string[] preCloseDialog;
    [SerializeField]
    private string[] closeDialog;
    [SerializeField]
    private string[] endDialog;
    [SerializeField]
    private SignalSender interactSignal;
    [SerializeField]
    private Player player;
    [SerializeField]
    private SignalSender positionReg;
    private bool inInteract = false;

    private void Start()
    {
        if (checkPointR2.r2LGateClose)
        {
            this.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(checkPointR2.getPureArrow && !checkPointR2.r2LGateClose)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (!inInteract)
                {
                    inInteract = true;
                    interactSignal.SendSignal();
                    player.SetFacingAnim(Vector2.left);
                }
            }
        }
    }

    public override bool PlayCondition()
    {
        return inInteract;
    }


    public override void EndPlayHandle()
    {
        player.SetFacingAnim(Vector2.down);
        SyncObjectPosition(actors[0], replacedGameObjects[0]);
        checkPointR2.r2LGateClose= true;
        positionReg.SendSignal();
        interactSignal.SendSignal();
    }


    private void Awake()
    {
        dialogs.Add(preCloseDialog);
        dialogs.Add(closeDialog);
        dialogs.Add(endDialog);
    }

}
