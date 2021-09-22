using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptStTest2 : OptionState
{

    //Constructor
    public OptStTest2(OptionManager optionManager) : base(optionManager)
    {
    }

    public override void OnClick(int index)
    {
        if(index == 0)
        {
            Debug.Log("test2");
        }
        else
        {
            Debug.Log("test2 Other");
        }
    }

}
