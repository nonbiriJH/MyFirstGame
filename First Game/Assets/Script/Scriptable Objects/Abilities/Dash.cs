using UnityEngine;
using DG.Tweening;

[CreateAssetMenu(fileName = "Dash", menuName = "Scriptable Objects/Abilities/Dash")]
public class Dash : GenericAbility
{
    public float DashForce;

    public override void Ability(Vector2 myPosition
        , Vector2 myFacingDirection
        , GameObject player = null)
    {
        //Check Magic
        if (playerMagic.runtimeValue >= magicCost)
        {
            playerMagic.runtimeValue -= magicCost;
            magicUISignal.SendSignal();
        }
        else
        {
            return; //Stop if the magic not enough
        }

        // perform Dash
        Rigidbody2D myRigidbody = player.GetComponent<Rigidbody2D>();
        if (myRigidbody)
        {
            Vector2 targetPosition = myPosition + myFacingDirection.normalized * DashForce;
            myRigidbody.DOMove(targetPosition, duartion);
        }
        
    }
}
