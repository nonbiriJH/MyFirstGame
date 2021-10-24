public class SaintTreeOption : OptionState
{
    public SaintTree saintTree;

    public SaintTreeOption(OptionManager optionManager, SaintTree saintTree):base(optionManager)
    {
        this.optionManager = optionManager;
        this.saintTree = saintTree;
    }

    public override void OnClick(int index)
    {
        saintTree.ChooseAction(index);
    }
}