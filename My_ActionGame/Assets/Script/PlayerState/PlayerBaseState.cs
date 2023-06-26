using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseState : State
{
    protected StateMachine stateMachine;

    public PlayerBaseState(StateMachine stateMachine)
    {
        this.stateMachine = stateMachine;

        new Vector3();
    }
}
