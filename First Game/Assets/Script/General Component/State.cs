using System.Collections;
using UnityEngine;

public abstract class State
{
    protected StateMachine stateMachine;
    protected Player player;

    //Constructor
    protected State(Player player)
    {
        this.player = player;
    }

    public virtual void BeginState()
    {

    }

    public virtual IEnumerator BeginStateCo()
    {
        yield break;
    }

    public virtual void HandleInput()
    {

    }

    public virtual void UpdateLogics()
    {

    }

    public virtual void UpdatePhysics()
    {

    }

    public virtual void ExitState()
    {

    }

}