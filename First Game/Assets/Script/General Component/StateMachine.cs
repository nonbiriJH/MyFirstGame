using UnityEngine;


public enum GenericState
{
    idle,
    walk,
    attack,
    stagger,
    interact,
    ability
}

public class StateMachine : MonoBehaviour
{
    public GenericState currentState;

    public void ChangeState(GenericState newState)
    {
        if (currentState != newState)
        {
            currentState = newState;
        }
    }
}
