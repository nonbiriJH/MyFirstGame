using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImoutoIdleState : ImoutoState
{
    private float IdleSecondCountDown;

    public ImoutoIdleState (Imouto imouto): base(imouto)
    {
    }

    public override void BeginState()
    {
        imouto.StopWalk();
        IdleSecondCountDown = imouto.IdleSecond;
    }

    public override void UpdateLogics()
    {
        IdleSecondCountDown -= Time.deltaTime;
        if (imouto.playerInRange)
        {
            imouto.ChangeState(new ImoutoInteractState(imouto));
        }
        else if (IdleSecondCountDown <= 0)
        {
            imouto.ChangeState(new ImoutoWalkState(imouto));
        }
    }

}
