using System.Collections;

public abstract class LogState
{
    protected Log log;

    //Constructor
    protected LogState(Log log)
    {
        this.log = log;
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
