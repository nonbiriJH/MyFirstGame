using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DR2_RGateOpenPostEvent : CutSceGenBasic
{
    [SerializeField]
    private CheckPointR2 checkPointR2;
    [SerializeField]
    private Transform logTransform;

    public override bool PlayCondition()
    {
        return checkPointR2.gateLogR2Move && logTransform.position.x == 37;
    }
}
