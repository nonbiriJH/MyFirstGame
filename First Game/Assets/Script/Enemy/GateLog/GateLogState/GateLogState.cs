using System.Collections;

public abstract class GateLogState
{
    protected GateLog gateLog;

    //Constructor
    public GateLogState(GateLog gateLog)
    {
        this.gateLog = gateLog;
    }

    public virtual void BeginState()
    {

    }

    public virtual IEnumerator BeginStateCo()
    {
        yield break;
    }

    public virtual void UpdateLogics()
    {

    }

    public virtual void UpdatePhysics()
    {

    }

    public virtual void ExitState()
    {

    }
}
