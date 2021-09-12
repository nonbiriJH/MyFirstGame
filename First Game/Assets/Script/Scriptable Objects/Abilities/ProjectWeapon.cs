using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ProjectWeapon", menuName = "Scriptable Objects/Abilities/Project Weapon")]
public class ProjectWeapon : GenericAbility
{
    public GameObject weapon;

    public override void Ability(Vector2 myPosition
        , Vector2 myFacingDirection
        , Animator animator = null
        , Rigidbody2D myRigidbody = null)
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

        //Project Weapon
        if (weapon)
        {
            //Instantiate Weapon
            float spriteDegreeToLeft = Mathf.Atan2(myFacingDirection.y, myFacingDirection.x) * Mathf.Rad2Deg;
            GameObject newWeapon = Instantiate(weapon, myPosition, Quaternion.Euler(0, 0, spriteDegreeToLeft));

            //Move Weapon
            newWeapon.GetComponent<Movement>().MoveObject(myFacingDirection);

        }
    }
}
