using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class log : Enemy
{
    public Transform target;
    public float chaseRadius;
    public float attackRadius;
    public Rigidbody2D myRigidBody;
    public Animator myAnimator;

    // Start is called before the first frame update
    public virtual void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        myAnimator = GetComponent<Animator>();
        myRigidBody = GetComponent<Rigidbody2D>();
        currentState = EnemyState.idle;
    }


    // Update is called once per frame
    void Update()
    {
        CheckDistance();
    }

    public virtual void CheckDistance()
    {
        if (Vector3.Distance(target.position
            , transform.position) <= chaseRadius
            && Vector3.Distance(target.position
            , transform.position) > attackRadius)
        {
            if(currentState == EnemyState.idle || currentState == EnemyState.walk)
            {
                Vector3 tempPosition = Vector3.MoveTowards(transform.position,
                target.position
                , speed * Time.deltaTime);

                ChangeWalkAnimator(tempPosition - transform.position);//change walk ani
                myRigidBody.MovePosition(tempPosition);//make displacement
                ChangeState(EnemyState.walk);//change state
            }

            myAnimator.SetBool("wake", true);

        }
        else if (Vector3.Distance(target.position, transform.position) > chaseRadius)
        {
            myAnimator.SetBool("wake", false);
        }
    }

    public void ChangeState(EnemyState newState)
    {
        if(currentState != newState)
        {
            currentState = newState;
        }
    }

    public void ChangeWalkAnimator(Vector2 direction)
    {
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if (direction.x > 0)
            {
                UpdateWalkAnimParameter(Vector2.right);
            }
            else if (direction.x < 0)
            {
                UpdateWalkAnimParameter(Vector2.left);
            }

        }
        else if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y))
        {
            if (direction.y > 0)
            {
                UpdateWalkAnimParameter(Vector2.up);
            }
            else if (direction.y < 0)
            {
                UpdateWalkAnimParameter(Vector2.down);
            }

        }
        else
        {
            UpdateWalkAnimParameter(Vector2.down);
        }
    }

    public void UpdateWalkAnimParameter(Vector2 boolDirection)
    {
        myAnimator.SetFloat("moveX", boolDirection.x);
        myAnimator.SetFloat("moveY", boolDirection.y);
    }

}
