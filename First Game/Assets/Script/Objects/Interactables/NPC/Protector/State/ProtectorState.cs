using System.Collections;

public abstract class ProtectorState
{
    protected Protector protector;

    //Constructor
    protected ProtectorState(Protector protector)
    {
        this.protector = protector;
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
