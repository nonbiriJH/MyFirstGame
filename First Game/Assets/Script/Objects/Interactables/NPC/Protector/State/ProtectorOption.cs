using UnityEngine;

public class ProtectorOption : OptionState
{
    public GameObject protector;

    public ProtectorOption(OptionManager optionManager, GameObject protector): base(optionManager)
    {
        this.protector = protector;
    }

    public override void OnClick(int index)
    {
        protector.GetComponent<Protector>().ChooseAction(index);
    }
}