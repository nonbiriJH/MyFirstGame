using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : StateMachine
{

    [Header("Player Attributes")]
    public floatValue playerMagic;
    public GenericAbility currentAbility;
    [SerializeField] private float speed;

    [Header("Utilities")]
    public SignalSender magicSignal;
    public vectorValue initialPosition;
    public Rigidbody2D myRigidBody;
    public Animator animator;

    //GetItem Para
    [Header("Item Parameters")]
    public SpriteRenderer itemSprite;//show item sprite when get item
    public Inventory inventory;//add new item to inventory; refer the new item pic

    //states
    public PlayerIdleState idleState;
    public PlayerWalkState walkState;
    public PlayerAttackState attackState;
    public PlayerStaggerState staggerState;
    public PlayerInteractState interactState;
    public PlayerAbilityState abilityState;



    // Start is called before the first frame update
    void Start()
    {
        animator.SetFloat("MoveX", 0);
        animator.SetFloat("MoveY", -1);
        transform.position = initialPosition.runtimeValue;

        //Create State Classe Instances
        idleState = new PlayerIdleState(this);
        walkState = new PlayerWalkState(this);
        attackState = new PlayerAttackState(this);
        staggerState = new PlayerStaggerState(this);
        interactState = new PlayerInteractState(this);
        abilityState = new PlayerAbilityState(this);


        Initialize(idleState);
    }

    private void Update()
    {
        currentState.HandleInput();
        currentState.UpdateLogics();
    }

    private void FixedUpdate()
    {
        currentState.UpdatePhysics();
    }


    public void MoveObject(Vector2 direction)
    {
        direction.Normalize();
        myRigidBody.velocity = direction * speed;
    }

    public void AddItemToInventory()
    {
        //get item pic
        itemSprite.sprite = inventory.newItem.itemSprite;//pass new item sprite to game scene
        //add to inventory
        inventory.AddItem(inventory.newItem);
    }

    // for interact signal to work
    public void ChangeToInteractState()
    {
        ChangeState(interactState);
    }

    //death
    public void Death()
    {
        this.gameObject.SetActive(false);
    }
}
