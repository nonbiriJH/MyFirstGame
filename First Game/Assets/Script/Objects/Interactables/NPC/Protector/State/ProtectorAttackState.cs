using UnityEngine;

public class ProtectorAttackState : ProtectorState
{

    //Constructor, Link a StateMachine instance with a Player instance
    public ProtectorAttackState(Protector protector) : base(protector)
    {
    }

    public override void BeginState()
    {
        base.BeginState();
        protector.hitZone.SetActive(true);
        protector.speed = 6;
        protector.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        BoxCollider2D[] collider2Ds = protector.GetComponents<BoxCollider2D>();
        for(int i =0; i < collider2Ds.Length; i++)
        {
            if (collider2Ds[i].isTrigger)
            {
                collider2Ds[i].enabled = false;
            }
        }
    }

    public override void UpdateLogics()
    {
        base.UpdateLogics();
        Vector2 direction = protector.targetWhenAttack.position - protector.transform.position;
        protector.MoveObject(direction);
        protector.animator.SetBool("Walk", true);
        protector.UpdateWalkAnimParameter(direction);
    }

    public override void ExitState()
    {
        base.ExitState();
        protector.MoveObject(Vector2.zero);
    }
}
