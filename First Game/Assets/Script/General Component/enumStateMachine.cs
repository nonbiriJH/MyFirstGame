using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GenericState
{
    idle,
    walk,
    attack,
    stun,
    dead,
    receiveItem,
    interact,
    stagger,
    ability
}

public class enumStateMachine : MonoBehaviour
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