using UnityEngine;

public class StateMachine: MonoBehaviour
{
    public State currentState { get; private set; }

    public void Initialize(State startingState)
    {
        currentState = startingState;
        startingState.BeginState();
    }

    public void ChangeState(State newState)
    {
        currentState.ExitState();
        currentState = newState;
        Debug.Log(currentState);
        newState.BeginState();
        StartCoroutine(newState.BeginStateCo());
    }


}