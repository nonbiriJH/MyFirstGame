using UnityEngine;

public class GateLog : Interactables
{
    [Header("Log Attributes")]
    public float speed;
    public float chaseRadius;
    public float idleSecondBeforeSleep;
    public GameObject triggerZone;
    public Vector2[] guidePath;
    [SerializeField]
    private Vector2 initPos;
    [SerializeField]
    private Vector2 openGatePosition;
    [SerializeField]
    private Item trigerEvilItem;
    [SerializeField]
    private ItemQuantityLookup itemQuantityLookup;
    public SignalSender evilSignal;

    [Header("Checkpoint")]
    public CheckPointR1 checkPointR1;
    public CheckPointR2 checkPointR2;
    public SignalSender regPositionOnCheckPoint;

    [Header("Dialog")]
    public bool canInteract;
    [SerializeField]
    private string[] openDialog;
    [SerializeField]
    private string[] evilDialog;
    [SerializeField]
    private string[] giveWayDialog;
    [SerializeField]
    private string[] openGateDialog;
    [SerializeField]
    private string[] openedGateDialog;

    [Header("Log Death")]
    public GameObject deadAnimation;
    public LootTable lootTable;
    public SignalSender DeathSignal;//for room to deregister.
    public SignalSender gateLogDeathSignal;
    public bool isDying;//for room to deregister.

    [Header("Utilities No Need Asign")]
    public Transform target;
    public Rigidbody2D myRigidBody;
    public Animator animator;
    public GateLogState currentState;
    public bool attacking;//change between attack and idle when !canInteract.
    public string[] nextDialog;
    public bool isEvil;

    //StateMachine methods
    public void Initialize(GateLogState startingState)
    {
        currentState = startingState;
        startingState.BeginState();
    }

    public void ChangeState(GateLogState newState)
    {
        currentState.ExitState();
        currentState = newState;
        newState.BeginState();
        StartCoroutine(newState.BeginStateCo());
    }

    //Movement
    public void MoveObject(Vector2 direction)
    {
        direction.Normalize();
        myRigidBody.velocity = direction * speed;
    }

    //2D Trigger
    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (canInteract)
        {
            base.OnTriggerEnter2D(other);
        }
    }

    public override void OnTriggerExit2D(Collider2D other)
    {
        if (canInteract)
        {
            base.OnTriggerExit2D(other);
        }
    }

    //animation
    public void UpdateWalkAnimParameter(Vector2 direction)
    {
        animator.SetFloat("MoveX", direction.x);
        animator.SetFloat("MoveY", direction.y);
    }



    //dialog

    public void PrepareDialog()
    {
        if (itemQuantityLookup.GetItemNumber(trigerEvilItem.itemName) >= 1)
        {
            nextDialog = evilDialog;
        }
        else if (checkPointR2.helpYellow && !checkPointR2.gateLogR1Move)
        {
            nextDialog = giveWayDialog;
        }
        else if (checkPointR2.gateLogR1Move && !checkPointR2.gateLogR2LOpenGate)
        {
            nextDialog = openGateDialog;
        }
        else if (checkPointR2.gateLogR2LOpenGate)
        {
            nextDialog = openedGateDialog;
        }
        else
        {
            nextDialog = openDialog;
        }
    }

    //dialog
    public void OneTimeDialog(string[] dialog)
    {
        if (interactStep == 0)
        {
            if (Input.GetButtonDown("Attack")) StartDialog(dialog);
            AddStep();
        }
        else if (interactStep == 1
            && !dialogBox.activeInHierarchy)
        {
            //player quite interact state
            InteractEnd();

            if (itemQuantityLookup.GetItemNumber(trigerEvilItem.itemName) >= 1)
            {
                EvilBehaviour();
            }
            if (checkPointR2.gateLogR1Move && !checkPointR2.gateLogR2Talked)
            {
                checkPointR2.gateLogR2Talked = true;//trigger DR2_OpenGate
            }
            if (checkPointR2.helpYellow && !checkPointR2.gateLogR1Move)
            {
                ChangeState(new LogGuideState(this));
            }
            else
            {
                ChangeState(new GateLogIdleState(this));
            }
        }
    }

    //Attack
    public virtual void AttackMove()
    {
        Vector2 tempPosition = target.position - transform.position;
        UpdateWalkAnimParameter(tempPosition);//change walk ani
        MoveObject(tempPosition);//make displacement
    }

    public virtual void CheckAttackDistance()
    {
        if (Vector3.Distance(target.position, transform.position) <= chaseRadius)
        {
            attacking = true;

        }
        else attacking = false;
    }

    //Become Evil
    public virtual void BecomeEvil()
    {
        isEvil = true;
        //Turn Red
        GenericStart();//can be called before start by gate log start
        RuntimeAnimatorController ac = GetComponent<Animator>().runtimeAnimatorController;
        animator.runtimeAnimatorController = null;
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0);
        animator.runtimeAnimatorController = ac;
        animator.SetBool("WakeUp", true);
        Vector2 tempPosition = target.position - transform.position;
        UpdateWalkAnimParameter(tempPosition);//change walk ani

        //Diable interact
        canInteract = false;
        disableContentHint.SendSignal();
        triggerZone.SetActive(true);
        myRigidBody.bodyType = RigidbodyType2D.Dynamic;
        idleSecondBeforeSleep = 9999;//never sleep
        chaseRadius = 25;

    }

    //Death Effects
    public void Death()
    {
        PreDeath();
        this.gameObject.SetActive(false);
    }

    public virtual void PreDeath()
    {
        isDying = true;
        DropLoot();
        DeathEffect();
        DeathSignal.SendSignal();
        //check point
        checkPointR1.gateLogKill = true;
        regPositionOnCheckPoint.SendSignal();
    }

    public virtual void DeathEffect()
    {
        if (deadAnimation != null)
        {
            GameObject effect = Instantiate(deadAnimation, transform.position, Quaternion.identity);
            Destroy(effect, 1f);
        }

    }

    public void DropLoot()
    {
        if (lootTable != null)
        {
            PowerUp newLoot = lootTable.ChooseLoot();
            if (newLoot != null)
            {
                Instantiate(newLoot, transform.position, Quaternion.identity);
            }
        }
    }

    public virtual void GateLogStart()
    {
        if (checkPointR1.gateLogKill)
        {
            gateLogDeathSignal.SendSignal();//signal to room to remove all enemies in room
        }
        else if (checkPointR1.gateLogEvil)
        {
            BecomeEvil();
            evilSignal.SendSignal();//signal to room to turn registered log to evil
        }
        else if (checkPointR2.gateLogR2LOpenGate)
        {
            transform.position = openGatePosition;
        }
        else if (checkPointR2.gateLogR1Move)
        {
            transform.position = guidePath[guidePath.Length - 1];
        }
        else if (!checkPointR2.gateLogR1Move)
        {
            transform.position = initPos;
        }
    }

    public virtual void EvilBehaviour()
    {
        BecomeEvil();
        evilSignal.SendSignal();//signal to room to turn registered log to evil
        checkPointR1.gateLogEvil = true;
        regPositionOnCheckPoint.SendSignal();
    }

    //Monobehaviours
    private void OnEnable()
    {
        if (isEvil)
        {
            ChangeState(new GateLogAttackState(this));
        }
    }

    public virtual void Start()
    {
        GenericStart();
        attacking = false;
        if (canInteract)
        {
            triggerZone.SetActive(false);
            myRigidBody.bodyType = RigidbodyType2D.Kinematic;
        }
        Initialize(new GateLogIdleState(this));
        GateLogStart();
    }

    private void GenericStart()
    {
        target = GameObject.FindWithTag("Player").transform;
        animator = this.gameObject.GetComponent<Animator>();
        myRigidBody = this.gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        currentState.UpdateLogics();
    }
}
