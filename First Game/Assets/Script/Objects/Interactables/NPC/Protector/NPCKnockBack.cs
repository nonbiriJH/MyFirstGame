using UnityEngine;

public class NPCKnockBack : MonoBehaviour
{
    private Rigidbody2D myRigidBody;
    private Protector protector;

    private void Start()
    {
        myRigidBody = this.GetComponentInParent<Rigidbody2D>();
        protector = this.GetComponentInParent<Protector>();
    }

    public void Knock(float knockBackTime, float damage)
    {
        GenericHealth myHealth = this.gameObject.GetComponent<GenericHealth>();

        if (myHealth)
        {
            //Take Damage
            myHealth.TakeDamage(damage);
        }

        if (myHealth.runTimeHealth > 0)
        {
            ProtectorStaggerState staggerState = new ProtectorStaggerState(protector);
            staggerState.knockBackTime = knockBackTime;
            protector.ChangeState(staggerState);
        }
        else
        {
            protector.ChangeState(new ProtectorDeathState(protector));
        }
    }
}
