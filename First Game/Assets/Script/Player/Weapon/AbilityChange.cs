using UnityEngine;

public class AbilityChange : MonoBehaviour
{
    public CurrentAbility currentAbility;
    public GenericAbility ability;

    public void ChangeAbility()
    {
        currentAbility.currentAbility = ability;
    }
}
