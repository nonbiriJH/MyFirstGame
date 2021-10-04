using UnityEngine;

public class Imouto : Interactables
{
    [SerializeField]
    private float speed;
    public float IdleSecond;

    [Header("CheckPoints")]
    public CheckPointR1 checkPointR1;
    public SignalSender regSignal;

    [Header("Route Variables")]
    public Vector2[] WalkPath;

    [Header("No Need Asign")]
    public string[] nextDialog;
    public bool nextDialogHappy;
    public Vector2 targetPosition;
    private int patrolTargetIndex;
    private Rigidbody2D myRigidBody;
    private Animator animator;
    private Vector2 facingDirection;
    private Transform targetTransform;
    public ImoutoState currentState;


    //state machine
    public void Initialize(ImoutoState startingState)
    {
        currentState = startingState;
        startingState.BeginState();
    }

    public void ChangeState(ImoutoState newState)
    {
        currentState.ExitState();
        currentState = newState;
        newState.BeginState();
        StartCoroutine(newState.BeginStateCo());
    }

    //animation
    public void UpdateWalkAnimParameter(Vector2 direction)
    {
        animator.SetFloat("MoveX", direction.x);
        animator.SetFloat("MoveY", direction.y);
    }

    public void SetHappy(bool isHappy)
    {
        animator.SetBool("Happy", isHappy);
    }

    public void SetPajama(bool isPajama)
    {
        animator.SetBool("Pajama", isPajama);
    }

    public string CurrentAnimName()
    {
        //Fetch the current Animation clip information for the base layer
        AnimatorClipInfo[] m_CurrentClipInfo = animator.GetCurrentAnimatorClipInfo(0);
        //Access the Animation clip name
        return m_CurrentClipInfo[0].clip.name;
    }

    //physics
    public void MoveObject(Vector2 direction)
    {
        direction.Normalize();
        myRigidBody.velocity = direction * speed;
        animator.SetBool("Walk", true);
        UpdateWalkAnimParameter(direction);
    }

    public void StopWalk()
    {
        myRigidBody.velocity = Vector2.zero;
        animator.SetBool("Walk", false);
    }

    public void FacePlayer()
    {
        facingDirection = targetTransform.position - transform.position;
        UpdateWalkAnimParameter(facingDirection);
    }

    public void ChangeTarget()
    {
        if (patrolTargetIndex == WalkPath.Length - 1)
        {
            patrolTargetIndex = 0;
        }
        else
        {
            patrolTargetIndex++;
        }
        targetPosition = WalkPath[patrolTargetIndex];
    }


    //dialog

    public void SetDialog(string[] dialog, bool isHappy)
    {
        nextDialog = dialog;
        nextDialogHappy = isHappy;
    }

    public void OneTimeDialog()
    {
        if (interactStep == 0)
        {
            if (Input.GetButtonDown("Attack"))
            {
                SetHappy(nextDialogHappy);
                StartDialog(nextDialog);
            }
            AddStep();
        }
        else if (interactStep == 1
            && !dialogBox.activeInHierarchy)
        {
            //player quite interact state
            InteractEnd();
            SetHappy(false);
        }
    }

    //Starts
    public void GenericAwake()
    {
        targetTransform = GameObject.Find("Player").transform;
        animator = gameObject.GetComponent<Animator>();
        myRigidBody = gameObject.GetComponent<Rigidbody2D>();
        targetPosition = WalkPath[0];
        Initialize(new ImoutoIdleState(this));
    }

    //monobehaviour
    private void Awake()
    {
        GenericAwake();
    }

    void Update()
    {
        currentState.UpdateLogics();
    }


}
