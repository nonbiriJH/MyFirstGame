using UnityEngine;

public class Protector: Interactables
{
    [Header("Checkpoints")]
    public CheckPointR1 checkPointRoute1;
    public SignalSender regPositionOnCheckPoint;

    [Header("Protector Variables")]
    public float speed;
    public float staySecond;
    public GameObject hitZone;

    [Header("Route Variables")]
    public Vector2[] patrolPath;
    public Transform targetWhenAttack;

    [Header("Dialog Variables")]
    //Dialogs
    public GameObject optionPanel;
    public Options options;
    public string[] warnZoneDialog;
    public string[] dangerZoneDialog;
    public string[] logKilledDialog;
    public string[] normalStartDialog;
    public string[] normalYesDialog;
    public string[] normalNoDialog;
    public string[] deathDialog;

    [Header("Utility No need to assign")]
    public Vector2 patrolTarget;
    public Animator animator;
    public Vector2 interactDirection;
    public bool playerInWarningZone;
    public bool playerInDangerZone;
    public bool attacking;
    public bool isSignState;
    public string[] endDialog;

    public ProtectorState currentState;
    private Rigidbody2D myRigidBody;
    private int patrolTargetIndex = 0;

    public void Initialize(ProtectorState startingState)
    {
        currentState = startingState;
        startingState.BeginState();
    }

    public void ChangeState(ProtectorState newState)
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
        if (attacking)
        {

        }
        else
        {
            base.OnTriggerEnter2D(other);
            if (other.CompareTag("Player") && !other.isTrigger)
            {
                interactDirection = other.transform.position - this.transform.position;
            }
        }
    }

    public override void OnTriggerExit2D(Collider2D other)
    {
        if (attacking)
        {

        }
        else
        {
            base.OnTriggerExit2D(other);
        }
    }

    public void UpdateWalkAnimParameter(Vector2 direction)
    {
        animator.SetFloat("MoveX", direction.x);
        animator.SetFloat("MoveY", direction.y);
    }


    public void ChangeTarget()
    {
        if (patrolTargetIndex == patrolPath.Length - 1)
        {
            patrolTargetIndex = 0;
        }
        else
        {
            patrolTargetIndex++;
        }
        patrolTarget = patrolPath[patrolTargetIndex];
    }


    public void ChooseAction(int index)
    {
        if (index == 0)
        {
            isSignState = true;
            endDialog = normalYesDialog;
            disableContentHint.SendSignal();
        }
        else
        {
            endDialog = normalNoDialog;
        }
        //Resume interaction step
        interactStep = 2;
    }

    //Starts
    private void StartGeneric()
    {
        myRigidBody = this.gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
    }
    private void StartBegin()
    {
        patrolTarget = patrolPath[0];
        isSignState = false;
        attacking = false;
        Initialize(new ProtectorIdelState(this));
    }
    private void StartR1YellowDie()
    {
        this.gameObject.SetActive(false);
    }
    private void StartR1YellowAttack()
    {
        attacking = true;
        Initialize(new ProtectorAttackState(this));
    }


    //Monobehaviours
    private void Start()
    {
        StartGeneric();
        if (checkPointRoute1.yellowDie) StartR1YellowDie();
        else if (checkPointRoute1.yellowAttack) StartR1YellowAttack();
        else StartBegin();
    }

    void Update()
    {
        currentState.UpdateLogics();
    }

}
