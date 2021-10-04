using System.Collections;

public abstract class ImoutoBedState
{
    protected ImoutoBed imoutoBed;

    //Constructor
    protected ImoutoBedState(ImoutoBed imoutoBed)
    {
        this.imoutoBed = imoutoBed;
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

    public virtual void ExitState()
    {

    }
}
