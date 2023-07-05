using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBaseState : State
{
    protected EnemyStateMachine stateMachine;

    public EnemyBaseState(EnemyStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    protected void Move(float deltaTime)
    {
        Move(Vector3.zero, deltaTime);
    }

    /*Player‚Ì“®‚«*/
    protected void Move(Vector3 motion, float deltaTime)
    {
        stateMachine.Controller.Move((motion + stateMachine.Receiver.movement) * deltaTime);
    }


    //Player‚ªˆê’è”ÍˆÍ“ü‚Á‚½‚©‚Ç‚¤‚©‚Ì”»’è‚ð‚µ‚Ä‚é‚Æ‚±
    protected bool IsInChaseRange()
    {
        float toPlayerDistanceSqr   = (stateMachine.Player.transform.position - stateMachine.transform.position).sqrMagnitude;

        return toPlayerDistanceSqr <= stateMachine.PlayerChasingRange * stateMachine.PlayerChasingRange;
    }
}
