using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptStTest1 : OptionState
{
    public GameObject actionGameObject;

    //Constructor
    public OptStTest1(OptionManager optionManager, GameObject gameObject) : base(optionManager)
    {
        actionGameObject = gameObject; 
    }

    public override void OnClick(int index)
    {
        actionGameObject.GetComponent<OptionTrigger>().ChoiceActions(index);
    }
}
