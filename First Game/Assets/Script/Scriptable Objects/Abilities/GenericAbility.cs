
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
        , GameObject player = null)
    {

    }

    public virtual void AbilityOnAttackState(Vector2 myPosition
    , Vector2 myFacingDirection
    , GameObject player = null)
    {

    }

}
