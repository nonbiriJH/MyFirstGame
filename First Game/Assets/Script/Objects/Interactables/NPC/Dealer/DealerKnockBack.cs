using UnityEngine;

public class DealerKnockBack : MonoBehaviour
{
    private Rigidbody2D myRigidBody;
    private Dealer dealer;

    private void Start()
    {
        myRigidBody = this.GetComponentInParent<Rigidbody2D>();
        dealer = this.GetComponentInParent<Dealer>();
    }

    public void Knock(float knockBackTime, float damage)
    {
        
            DealerStaggerState staggerState = new DealerStaggerState(dealer);
            staggerState.knockBackTime = knockBackTime;
            dealer.ChangeState(staggerState);
        
    }
}
