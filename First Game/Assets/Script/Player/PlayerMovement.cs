using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : Movement
{

    [Header("Player Attributes")]
    public floatValue playerMagic;
    public SkillLookup playerSkill;
    public GenericAbility currentAbility;

    [Header("Utilities")]
    public SignalSender magicSignal;
    public vectorValue initialPosition;
    public StateMachine playerStateMachine;

    //GetItem Para
    [Header("Item Parameters")]
    public SpriteRenderer itemSprite;//show item sprite when get item
    public Inventory inventory;//add new item to inventory; refer the new item pic

    private Vector2 currentDirection = Vector2.down;
    private Vector2 inputDirection = Vector2.zero;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        playerStateMachine.ChangeState(GenericState.idle);
        animator = GetComponent<Animator>();
        animator.SetFloat("MoveX", 0);
        animator.SetFloat("MoveY", -1);
        transform.position = initialPosition.runtimeValue;

    }

    // Update is called once per frame
    void Update()
    {
        if (!IsRestrictedState(playerStateMachine.currentState))
        {
            GetInput();
        }
    }

    //Define States that not alowed to get input
    bool IsRestrictedState(GenericState currentState)
    {   // when interact or stagger, wait until state change
        if (playerStateMachine.currentState == GenericState.interact
            || playerStateMachine.currentState == GenericState.stagger
            || playerStateMachine.currentState == GenericState.attack
            || playerStateMachine.currentState == GenericState.ability)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void GetInput()
    {
        if (Input.GetButtonDown("Attack"))
        {
            StartCoroutine(attackCo());

        }
        else if (Input.GetButtonDown("Ability"))
        {
            StartCoroutine(AbilityCo(currentAbility.duartion));

        }

        //Walk
        else
        {
            //Move Object
            inputDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            MoveObject(inputDirection);
            //Change State
            playerStateMachine.currentState = GenericState.walk;
            //Animation
            updateAnimation();
        }
    }

    //Moving Animation
    void updateAnimation()
    {
        if (inputDirection != Vector2.zero)
        {
           
            animator.SetFloat("MoveX", inputDirection.x);
            animator.SetFloat("MoveY", inputDirection.y);
            animator.SetBool("Walking", true);
        }
        else
        {
            animator.SetBool("Walking", false);
            playerStateMachine.currentState = GenericState.idle;
        }
        //Update facing direction for ability
        currentDirection.x = animator.GetFloat("MoveX");
        currentDirection.y = animator.GetFloat("MoveY");
    }


    //Attacking
    private IEnumerator attackCo()
    {
        animator.SetBool("Attack", true);
        playerStateMachine.currentState = GenericState.attack;
        yield return null; //Delay for one frame
        animator.SetBool("Attack", false); //do not enter attack again;
        MoveObject(Vector2.zero); //some times fallback after attack
        yield return new WaitForSeconds(.12f);//delay for finishing animation
        if (playerStateMachine.currentState != GenericState.interact)
        {
            playerStateMachine.currentState = GenericState.idle;//back to idle state
        }
        
    }


    //Interacting
    //This will be called on item signal
    public void GetItem()
    {
        if(playerStateMachine.currentState != GenericState.interact)
        {
            //change state
            playerStateMachine.currentState = GenericState.interact;
            //set animation
            animator.SetBool("GetItem", true);
            //get item pic
            itemSprite.sprite = inventory.newItem.itemSprite;//pass new item sprite to game scene
            //add to inventory
            inventory.AddItem(inventory.newItem);
        }
        else
        {
            //change state
            playerStateMachine.currentState = GenericState.idle;
            //set animation
            animator.SetBool("GetItem", false);
            //get item pic
            itemSprite.sprite = null;
        }
    }

    public void Death()
    {
        this.gameObject.SetActive(false);
    }

    private IEnumerator AbilityCo(float abilityDuration)
    {
        playerStateMachine.currentState = GenericState.ability;
        currentAbility.Ability(transform.position, currentDirection, animator, myRigidBody);
        yield return new WaitForSeconds(abilityDuration);
        playerStateMachine.currentState = GenericState.idle;
    }

}
