using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : StateMachine
{
    [Header("Checkpoints")]
    public CheckPointR1 checkPointR1;

    [Header("Player Attributes")]
    public floatValue playerMagic;
    public CurrentAbility currentAbility;
    [SerializeField] private float speed;
    public bool evilMode;

    [Header("Utilities")]
    public SignalSender magicSignal;
    public vectorValue initialPosition;
    public Rigidbody2D myRigidBody;
    public Animator animator;
    public bool interacted = false;

    //GetItem Para
    [Header("Item Parameters")]
    public GameObject getItem;
    public SpriteRenderer itemSprite;//show item sprite when get item
    public Inventory inventory;//add new item to inventory; refer the new item pic
    public ItemList itemMaster;
    public ItemQuantityLookup itemQuantityLookup;

    //states
    public PlayerIdleState idleState;
    public PlayerWalkState walkState;
    public PlayerAttackState attackState;
    public PlayerStaggerState staggerState;
    public PlayerInteractState interactState;
    public PlayerAbilityState abilityState;

    [HideInInspector]
    public SoundManager soundManager;
    [HideInInspector]
    public BGMManager bGMManager;

    public void RegPosition()
    {
        initialPosition.runtimeValue = transform.position;
    }

    // Start is called before the first frame update
    void Awake()
    {
        transform.position = initialPosition.runtimeValue;
        currentAbility.currentAbility = null;
        inventory.newItemName = null;
        soundManager = (SoundManager)FindObjectOfType(typeof(SoundManager));
        bGMManager = (BGMManager)FindObjectOfType(typeof(BGMManager));

        if (checkPointR1.revenge)
        {
            TurnRed();
        }
        animator.SetFloat("MoveX", 0);
        animator.SetFloat("MoveY", -1);

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


    public void MoveObject(Vector2 direction, float speedRatio = 1f)
    {
        direction.Normalize();
        myRigidBody.velocity = direction * speed * speedRatio;
    }

    public void SetFacingAnim (Vector2 dirction)
    {
        animator.SetFloat("MoveX", dirction.x);
        animator.SetFloat("MoveY", dirction.y);
    }

    public void AddItemToInventory()
    {
        //get item pic
        itemSprite.sprite = itemMaster.GetItem(inventory.newItemName).itemSprite;//pass new item sprite to game scene
        //add to inventory
        AddItem(inventory.newItemName);
    }

    public void AddItem(string newItemName)
    {
        if (itemQuantityLookup.GetItemNumber(newItemName) == 0)
        {
            ReorderEmptyItem();
        }
        itemQuantityLookup.IncreaseAmount(newItemName);
    }

    //New Item does not apear before existing item.
    //Trigger only when get new item not in inventory (number = 0)
    public void ReorderEmptyItem()
    {
        for (int i = 0; i < inventory.itemList.Count; i++)
        {
            if (itemQuantityLookup.GetItemNumber(inventory.itemList[i]) <= 0)
            {
                string itemToReorder = inventory.itemList[i];
                inventory.itemList.Remove(itemToReorder);
                inventory.itemList.Add(itemToReorder);
            }
        }
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

    public void TurnRed()
    {
        //cache xy anim para
        float x = animator.GetFloat("MoveX");
        float y = animator.GetFloat("MoveY");
        RuntimeAnimatorController ac = animator.runtimeAnimatorController;
        animator.runtimeAnimatorController = null;
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0);
        animator.runtimeAnimatorController = ac;
        animator.SetFloat("MoveX", x);
        animator.SetFloat("MoveY", y);
    }
}
