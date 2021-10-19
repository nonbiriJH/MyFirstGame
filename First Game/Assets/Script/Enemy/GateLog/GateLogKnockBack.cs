using UnityEngine;

public class GateLogKnockBack : MonoBehaviour
{
    private Rigidbody2D myRigidBody;
    private GateLog logRef;

    private void Start()
    {
        myRigidBody = this.GetComponentInParent<Rigidbody2D>();
        logRef = this.gameObject.GetComponentInParent<GateLog>();
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
            GateLogStaggerState staggerState = new GateLogStaggerState(logRef);
            staggerState.knockBackTime = knockBackTime;
            logRef.ChangeState(staggerState);
        }
        else
        {
            logRef.Death();
        }

    }
}
