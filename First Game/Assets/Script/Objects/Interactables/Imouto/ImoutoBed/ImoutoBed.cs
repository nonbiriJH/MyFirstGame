using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImoutoBed : Interactables
{
    public float awakeSecond;

    [Header("No Need Asign")]
    public string[] nextDialog;
    private Animator animator;
    public bool wakeable;
    public ImoutoBedState currentState;

    //State Machine
    public void Initialize(ImoutoBedState startingState)
    {
        currentState = startingState;
        startingState.BeginState();
        StartCoroutine(startingState.BeginStateCo());
    }

    public void ChangeState(ImoutoBedState newState)
    {
        currentState.ExitState();
        currentState = newState;
        Debug.Log(currentState);
        newState.BeginState();
        StartCoroutine(newState.BeginStateCo());
    }

    //Animation
    public void IsInbed(bool isInBed)
    {
        animator.SetBool("Inbed", isInBed);
    }
    public void IsSleepInBed(bool isSleep)
    {
        animator.SetBool("Sleep", isSleep);
    }
    public void WakeUpTurnRigh(bool isRight)
    {
        animator.SetBool("FaceRight", isRight);
    }
    public bool AnimatorBoolValue(string boolParaName)
    {
        return animator.GetBool(boolParaName);
    }


    public string CurrentAnimName()
    {
        //Fetch the current Animation clip information for the base layer
        AnimatorClipInfo[] m_CurrentClipInfo = animator.GetCurrentAnimatorClipInfo(0);
        //Access the Animation clip name
        return m_CurrentClipInfo[0].clip.name;
    }

    //Dialog
    public void OneTimeDialog()
    {
        if (interactStep == 0)
        {
            if (Input.GetButtonDown("Attack"))
            {
                StartDialog(nextDialog);
            }
            AddStep();
        }
        else if (interactStep == 1
            && !dialogBox.activeInHierarchy)
        {
            //player quite interact state
            InteractEnd();
        }
    }

    //tigger collider
    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (AnimatorBoolValue("Inbed"))
        {
            base.OnTriggerEnter2D(other);
        }
        else
        {
            playerInRange = false;
        }
    }

    public override void OnTriggerExit2D(Collider2D other)
    {
        if (AnimatorBoolValue("Inbed"))
        {
            base.OnTriggerExit2D(other);
        }
    }

    //Mono
    void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
        //set default variables
        wakeable = false;
        Initialize(new ImoutoBedIdelState(this));
    }

    void Update()
    {
        currentState.UpdateLogics();
    }
}
