using UnityEngine;

public class ProtectorSignState : ProtectorState
{
    public ProtectorSignState(Protector protector) : base(protector)
    {
    }

    public override void BeginState()
    {
        protector.isSignState = true;
    }

    public override void UpdateLogics()
    {
        base.UpdateLogics();
    }
}