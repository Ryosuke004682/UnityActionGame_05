using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public abstract class PlayerBaseState : State
{
    protected PlayerStateMachine stateMachine;

    public PlayerBaseState(PlayerStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    /*Playerの動き*/
    protected void Move(Vector3 motion , float deltaTime)
    {
        stateMachine.Controller.Move((motion + stateMachine.Receiveret.movement) * deltaTime);
    }

    /*敵にフォーカスを当てているときに動き*/
    protected void FaceTarget()
    {
        if (stateMachine.Targeter.CurrentTarget == null) { return; }

        Vector3 lookPos = stateMachine.Targeter.CurrentTarget.transform.position - stateMachine.transform.position;
        lookPos.y = 0.0f;

        stateMachine.transform.rotation = Quaternion.LookRotation(lookPos);
    }
}
