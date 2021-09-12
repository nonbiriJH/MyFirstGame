using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Attach to object with rigid body component
public class GenericKnockBack : MonoBehaviour
{
    public virtual void Knock()
    {
        Destroy(this.gameObject);
    }
}
