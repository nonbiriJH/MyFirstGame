using UnityEngine;

public class Dealer : Interactables
{
    [Header("Checkpoint")]
    public CheckPointR1 checkPointR1;
    public SignalSender regPositionOnCheckPoint;
    public Item triggerItem;

    [Header("Dealer Variables")]
    public float speed;
    public float damage;
    public float staySecond;
    public bool shopOnly = false;

    [Header("Route Variables")]
    public Vector2[] shopPath;
    public Vector2 bossDiePosition;

    [Header("Base Dialog Variables")]
    //Dialogs
    public string[] startDialog;
    public string[] endDialog;
    public string[] endDialogGetBlade;
    public string[] startDialogAftBlade;
    public string[] endDialogAftBlade;
    public string[] startDialogShopOnly;
    public string[] endDialogShopOnly;
    public string[] bossDieDialog;
    public string[] bossDieDialogPost;
    public GameObject shopPanel;

    [Header("Utility No need to assign")]
    public Vector2 patrolTarget;
    public Animator animator;
    public Transform target;
    public string[] internalStartDialog;
    public string[] internalEndDialog;

    public DealerState currentState;
    private Rigidbody2D myRigidBody;
    private int patrolTargetIndex = 0;

    public void Initialize(DealerState startingState)
    {
        currentState = startingState;
        startingState.BeginState();
    }

    public void ChangeState(DealerState newState)
    {
        currentState.ExitState();
        currentState = newState;
        newState.BeginState();
        StartCoroutine(newState.BeginStateCo());
    }

    public void MoveObject(Vector2 direction)
    {
        direction.Normalize();
        myRigidBody.velocity = direction * speed;
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
    }

    public void UpdateWalkAnimParameter(Vector2 direction)
    {
        animator.SetFloat("MoveX", direction.x);
        animator.SetFloat("MoveY", direction.y);
    }


    public void ChangeTarget()
    {
        if (patrolTargetIndex == shopPath.Length - 1)
        {
            patrolTargetIndex = 0;
        }
        else
        {
            patrolTargetIndex++;
        }
        patrolTarget = shopPath[patrolTargetIndex];
    }

    //dialog
    public void ChangeInternalDialog(string[] newStart, string[] newEnd)
    {
        internalStartDialog = newStart;
        internalEndDialog = newEnd;
    }

    //triggers

    public void BossDie()
    {
        transform.position = bossDiePosition;
        UpdateWalkAnimParameter(Vector2.down);
        ChangeState(new DealerAfterBoss(this));
    }

    //Monobehaviours
    private void Start()
    {
        myRigidBody = this.gameObject.GetComponent<Rigidbody2D>();
        patrolTarget = shopPath[0];
        animator = gameObject.GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").transform;
        if (shopOnly) Initialize(new DealerInteractState(this));
        else Initialize(new DealerIdleState(this));
        Debug.Log(currentState);
    }

    private void Update()
    {
        currentState.UpdateLogics();
    }

}
