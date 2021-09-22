using UnityEngine;


[CreateAssetMenu(fileName = "EvilBlade", menuName = "Scriptable Objects/Abilities/Evil Blade")]
public class Evilblade : GenericAbility
{
    public GameObject blade;

    public override void Ability(Vector2 myPosition
     , Vector2 myFacingDirection
     , GameObject player = null)
    {
        GameObject evilBlade = GameObject.Find("EvilBlade(Clone)");
        if (blade != null && evilBlade == null)
        {

            GameObject newWeapon = Instantiate(blade, myPosition, Quaternion.identity);
            //Add new item holder to child of content
            newWeapon.transform.SetParent(player.transform);
            newWeapon.transform.localScale = new Vector3(1.5f, 1.5f, 1);
            player.GetComponent<Player>().evilMode = true;
        }
    }

    public override void AbilityOnAttackState(Vector2 myPosition
    , Vector2 myFacingDirection
    , GameObject player = null)
    {
        GameObject evilBlade = GameObject.Find("EvilBlade(Clone)");
        if (evilBlade != null)
        {
            //rename Blade
            evilBlade.name = "EvilBladeShoot";
            //Rotate Weapon
            float spriteDegreeToLeft = Mathf.Atan2(myFacingDirection.y, myFacingDirection.x) * Mathf.Rad2Deg + 90;
            evilBlade.transform.rotation = Quaternion.Euler(0, 0, spriteDegreeToLeft);

            //Move Weapon
            evilBlade.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            evilBlade.GetComponent<Rigidbody2D>().gravityScale = 0;
            evilBlade.GetComponent<Movement>().MoveObject(myFacingDirection);
        }
    }

}
