public abstract class OptionState
{
    protected OptionManager optionManager;

    //Constructor
    protected OptionState(OptionManager optionManager)
    {
        this.optionManager = optionManager;
    }

    public virtual void OnClick(int index)
    {

    }
}