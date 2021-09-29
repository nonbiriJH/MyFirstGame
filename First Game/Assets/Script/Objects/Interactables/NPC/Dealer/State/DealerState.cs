using System.Collections;

public abstract class DealerState
{
    protected Dealer dealer;

    //Constructor
    protected DealerState(Dealer dealer)
    {
        this.dealer = dealer;
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
