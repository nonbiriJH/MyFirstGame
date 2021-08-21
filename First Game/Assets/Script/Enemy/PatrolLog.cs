using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolLog : log
{
    public Transform[] path;
    private float roundingError = 0.01f;
    private Transform patrolTarget;
    private int patrolTargetIndex = 0;

    public override void Start()
    {
        base.Start();
        patrolTarget = path[0];
    }

    public override void CheckDistance()
    {
        if (Vector3.Distance(target.position
            , transform.position) <= chaseRadius
            && Vector3.Distance(target.position
            , transform.position) > attackRadius)
        {
            if (currentState == EnemyState.idle || currentState == EnemyState.walk)
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
            myAnimator.SetBool("wake", true);
            float petrolTargetDistance = Vector3.Distance(patrolTarget.position, transform.position);
            if (petrolTargetDistance < roundingError)
            {
                ChangeTarget();
            }
            //move to patrol target
            Vector3 tempPosition = Vector3.MoveTowards(transform.position,
                patrolTarget.position
                , speed * Time.deltaTime);

            ChangeWalkAnimator(tempPosition - transform.position);//change walk ani
            myRigidBody.MovePosition(tempPosition);//make displacement
            ChangeState(EnemyState.walk);//change state

        }
    }

    private void ChangeTarget()
    {
        if (patrolTargetIndex == path.Length - 1)
        {
            patrolTargetIndex = 0;
        }
        else
        {
            patrolTargetIndex++;
        }
        patrolTarget = path[patrolTargetIndex];
    }
}
