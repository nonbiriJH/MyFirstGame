using System.Collections;

public abstract class ImoutoState
{
    protected Imouto imouto;

    //Constructor
    protected ImoutoState(Imouto imouto)
    {
        this.imouto = imouto;
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
