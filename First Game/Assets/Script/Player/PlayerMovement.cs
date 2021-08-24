using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState{
    idle,
    walk,
    attack,
    stagger,
    interact
}

public class PlayerMovement : MonoBehaviour
{
    public PlayerState currentState;
    public float speed;
    private Rigidbody2D myRigidBody;
    private Vector3 change;
    private Animator animator;
    public floatValue playerHealth;
    public SignalSender healthSignal;
    public vectorValue initialPosition;
    //GetItem Para
    public SpriteRenderer itemSprite;//show item sprite when get item
    public Inventory inventory;//add new item to inventory; refer the new item pic

    //Screen Kick Effect
    public SignalSender screenKick;
    
    // Start is called before the first frame update
    void Start()
    {
        currentState = PlayerState.idle;
        myRigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.SetFloat("MoveX", 0);
        animator.SetFloat("MoveY", -1);
        transform.position = new Vector3(initialPosition.runtimeValue.x
                                            , initialPosition.runtimeValue.y
                                            , transform.position.z);
        
    }

    // Update is called once per frame
    void Update()
    {
        if(currentState == PlayerState.interact)// when interact keep the state
        {
            return;
        }
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("Attack") && currentState != PlayerState.attack)
        {
            StartCoroutine(attackCo());

        }
        else if (currentState == PlayerState.walk || currentState == PlayerState.idle)
        {
            updateAnimationAndMove();
        }
    }

    //Moving
    void updateAnimationAndMove()
    {
        if (change != Vector3.zero)
        {
            movePlayer();
            animator.SetFloat("MoveX", change.x);
            animator.SetFloat("MoveY", change.y);
            animator.SetBool("Walking", true);
        }
        else
        {
            animator.SetBool("Walking", false);
            currentState = PlayerState.idle;
        }
    }

    void movePlayer()
    {
        change.Normalize();
        myRigidBody.MovePosition(
            transform.position + speed * Time.deltaTime * change
            );
        currentState = PlayerState.walk;
    }

    //Attacking
    private IEnumerator attackCo()
    {
        animator.SetBool("Attack", true);
        currentState = PlayerState.attack;
        yield return null; //Delay for one frame
        animator.SetBool("Attack", false); //do not enter attack again;
        myRigidBody.velocity = Vector2.zero; //some times fallback after attack
        yield return new WaitForSeconds(.12f);//delay for finishing animation
        if (currentState != PlayerState.interact)
        {
            currentState = PlayerState.idle;//back to idle state
        }
        
    }

    public void Knock(float knockBackTime, float damage)
    {
        playerHealth.runtimeValue -= damage;
        healthSignal.SendSignal();//send signal to reduce heart UI.
        if (playerHealth.runtimeValue > 0)
        {
            StartCoroutine(KnockCo(knockBackTime));
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }


    private IEnumerator KnockCo(float knockBackTime)
    {
        if (myRigidBody != null)
        {
            Debug.Log("kick");
            screenKick.SendSignal();
            yield return new WaitForSeconds(knockBackTime);
            myRigidBody.velocity = Vector2.zero;
            currentState = PlayerState.idle;
        }
    }

    //Interacting
    //This will be called on item signal
    public void GetItem()
    {
        if(currentState != PlayerState.interact)
        {
            //change state
            currentState = PlayerState.interact;
            //set animation
            animator.SetBool("GetItem", true);
            //get item pic
            itemSprite.sprite = inventory.newItem.itemSprite;//pass new item sprite to game scene
            //add to inventory
            inventory.AddItem();
        }
        else
        {
            //change state
            currentState = PlayerState.idle;
            //set animation
            animator.SetBool("GetItem", false);
            //get item pic
            itemSprite.sprite = null;
        }
    }
        
}
