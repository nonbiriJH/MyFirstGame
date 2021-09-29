using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : GenericHealth
{
    //When enter room enemies will be enabled.
    public virtual void OnEnable()
    {
        runTimeHealth = Health.initialValue;
    }
}
