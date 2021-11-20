using UnityEngine;

public class ProtectorIdelState : ProtectorState
{
    private float staySecondCountDown;

    //Constructor, Link a StateMachine instance with a Player instance
    public ProtectorIdelState(Protector protector) : base(protector)
    {
    }


    public override void BeginState()
    {
        base.BeginState();
        staySecondCountDown = protector.staySecond;
        //always look at right.
        protector.UpdateWalkAnimParameter(new Vector2(1, 0));
    }


    public override void UpdateLogics()
    {
        base.UpdateLogics();
        staySecondCountDown -= Time.deltaTime;

        //During shop mode
        if (!protector.attacking)
        {
            if (protector.playerInRange && protector.checkPointR2.helpYellow)
            {
                protector.ChangeState(new ProtectorSignState(protector));
            }
            else if (protector.playerInRange || protector.playerInWarningZone || protector.playerInDangerZone)
            {
                protector.ChangeState(new ProtectorInteractState(protector));
            }
            else if(staySecondCountDown <= 0)
            {
                protector.ChangeState(new ProtectorWalkState(protector));
            }
        }
        else
        {
            protector.bGMManager.ChangeBGM("BossBattle");
            protector.ChangeState(new ProtectorAttackState(protector));
        }
    }
}
