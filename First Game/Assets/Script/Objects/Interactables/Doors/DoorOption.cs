using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOption : OptionState
{
    public GameObject actionGameObject;

    //Constructor
    public DoorOption(OptionManager optionManager, GameObject gameObject) : base(optionManager)
    {
        actionGameObject = gameObject;
    }

    public override void OnClick(int index)
    {
        actionGameObject.GetComponent<KeyDoor>().ChooseAction(index);
    }
}
