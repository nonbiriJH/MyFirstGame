using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKnockBack : GenericKnockBack
{
    private Rigidbody2D myRigidBody;

    private void Start()
    {
        myRigidBody = this.GetComponentInParent<Rigidbody2D>();
    }

    public void Knock(float knockBackTime, float damage)
    {
        if(this.gameObject.GetComponentInParent<Enemy>().currentState != EnemyState.stagger)
        {
            this.gameObject.GetComponentInParent<Enemy>().currentState = EnemyState.stagger;
            EnemyHealth myHealth = this.gameObject.GetComponent<EnemyHealth>();

            if (myHealth)
            {
                //Take Damage
                myHealth.TakeDamage(damage);
            }

            if (myHealth.runTimeHealth > 0)
            {
                //Knock Back
                StartCoroutine(KnockCo(knockBackTime));
            }
            else
            {
                this.GetComponentInParent<log>().Death();
            }
        }

    }

    private IEnumerator KnockCo(float knockBackTime)
    {
        if (myRigidBody != null)
        {
            yield return new WaitForSeconds(knockBackTime);
            myRigidBody.velocity = Vector2.zero;
            myRigidBody.GetComponent<Enemy>().currentState = EnemyState.idle;
        }

    }
}
