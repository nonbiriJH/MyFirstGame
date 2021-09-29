using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtectorDamage : GenericDamage
{
    [SerializeField]
    private Protector protector;

    public override void KnockPlayer(Collider2D other)
    {
        base.KnockPlayer(other);
        ProtectorStaggerState staggerState = new ProtectorStaggerState(protector);
        staggerState.knockBackTime = knockBackTime;
        protector.ChangeState(staggerState);
    }
}
