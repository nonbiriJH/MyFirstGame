using UnityEngine;

public class Dealer : Interactables
{
    [Header("Checkpoint")]
    public CheckPointR1 checkPointR1;
    public SignalSender regPositionOnCheckPoint;
    public Item triggerItem;

    [Header("Dealer Variables")]
    public float speed;
    public float attackSpeed;
    public float projectDelay;
    public GameObject project;
    public float staySecond;
    public bool shopOnly = false;
    [SerializeField]
    private bool inDungeon;
    public GameObject hitZone;
    public float attackRadius;
    public float chaseRadius;
    public ItemQuantityLookup itemQuantityLookup;

    [Header("Route Variables")]
    public Transform targetWhenAttack;
    public Vector2[] shopPath;
    [SerializeField]
    private Vector2 dungeonFirstStart;
    [SerializeField]
    private Vector2 bossDiePosition;
    [SerializeField]
    private Vector2 preBossPosition;
    [SerializeField]
    private Vector2 preR1EndBattlePosition;

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
    [HideInInspector]
    public BGMManager bGMManager;

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
        if (currentState is DealerAttackState || currentState is DealerStaggerState)
        {
            myRigidBody.velocity = direction * attackSpeed;
        }
        else
        {
            myRigidBody.velocity = direction * speed;
        }
        animator.SetBool("Walk", true);
        UpdateWalkAnimParameter(direction);
    }
    public void StopObject()
    {
        myRigidBody.velocity = Vector2.zero;
        animator.SetBool("Walk", false);
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if(currentState is DealerAttackState || currentState is DealerStaggerState)
        {
            //when attack no interact
        }
        else
        {
            base.OnTriggerEnter2D(other);
        }
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

    //Project Arrow
    public void LaunchArrow(Vector2 direction)
    {
        //Instantiate Weapon
        float spriteDegreeToLeft = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        GameObject newWeapon = Instantiate(project, transform.position, Quaternion.Euler(0, 0, spriteDegreeToLeft));
        //Move Weapon
        newWeapon.GetComponent<Movement>().MoveObject(direction);
    }

    //triggers

    public void BossDie()
    {
        transform.position = bossDiePosition;
        UpdateWalkAnimParameter(Vector2.down);
        ChangeState(new DealerAfterBoss(this));
    }

    private void GenericStart()
    {
        myRigidBody = this.gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").transform;
        Initialize(new DealerIdleState(this));
    }

    public void PreBossStart()
    {
        shopOnly = true;
        gameObject.transform.position = preBossPosition;
        UpdateWalkAnimParameter(Vector2.down);
        animator.SetBool("Walk", false);
        myRigidBody.velocity = Vector2.zero;
        Initialize(new DealerInteractState(this));
    }

    private void DungeonStart()
    {
        //hide when start in Dungeon.
        transform.position = dungeonFirstStart;
        shopOnly = true;
        Initialize(new DealerInteractState(this));
    }

    public void R1EndStart()
    {
        //change to attack state
        //triggerred by cutscene script
        bGMManager = (BGMManager)FindObjectOfType(typeof(BGMManager));
        bGMManager.ChangeBGM("BossBattle");
        ChangeState(new DealerAttackState(this));
        transform.position = preR1EndBattlePosition;
    }

    //Monobehaviours
    private void Awake()
    {
        GenericStart();
        if(!inDungeon && checkPointR1.redAppear && !checkPointR1.preBattleDealer)
        {
            //after redAppear remove red from overworld until prebattle
            this.gameObject.SetActive(false);
        }
        else if(inDungeon && checkPointR1.preBattleDealer)
        {
            //after R1preBattle remove red from dungeon
            this.gameObject.SetActive(false);
        }
    }
    private void Start()
    {
        //when prebattleR1 do nothing.
        if (checkPointR1.preBattleDealer)
        {
            R1EndStart();
        }
        else if (checkPointR1.bossDown)
        {
            BossDie();
        }
        else if (checkPointR1.redAppear)
        {
            PreBossStart();
        }
        else if (inDungeon)
        {
            DungeonStart();
        }
        else
        {
            patrolTarget = shopPath[0];
        }
    }

    private void Update()
    {
        currentState.UpdateLogics();
    }

}
