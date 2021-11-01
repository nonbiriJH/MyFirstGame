using UnityEngine;

public class EnemyKnockBack : GenericKnockBack
{
    private Rigidbody2D myRigidBody;
    [HideInInspector]
    public Log logRef;

    private void Start()
    {
        myRigidBody = this.GetComponentInParent<Rigidbody2D>();
        logRef = this.gameObject.GetComponentInParent<Log>();
    }

    public void Knock(float knockBackTime, float damage)
    {
        EnemyHealth myHealth = this.gameObject.GetComponent<EnemyHealth>();

        if (myHealth)
        {
            //Take Damage
            myHealth.TakeDamage(damage);
        }

        if (myHealth.runTimeHealth > 0)
        {
            //Knock Back
            LogStaggerState staggerState = new LogStaggerState(logRef);
            staggerState.knockBackTime = knockBackTime;
            logRef.ChangeState(staggerState);
        }
        else
        {
            logRef.Death();
        }
    }
}
