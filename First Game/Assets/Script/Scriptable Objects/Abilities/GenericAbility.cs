
using UnityEngine;


public class GenericAbility : ScriptableObject
{
    [Header("Base Attribute")]
    public float magicCost;
    public float duartion;

    [Header("Base Utility")]
    public SignalSender magicUISignal;
    public floatValue playerMagic;

    public virtual void Ability(Vector2 myPosition
        , Vector2 myFacingDirection
        , Animator animator = null
        , Rigidbody2D myRigidbody = null)
    {

    }

}
