using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class EnemyIdleState : EnemyBaseState
{
    private readonly int LocomotionHash = Animator.StringToHash("Locomotion");
    private readonly int SpeedHash      = Animator.StringToHash("Speed");

    private const float CrossFadeDuration = 0.1f;
    private const float AnimatorDampTime  = 0.1f;


    public EnemyIdleState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }


    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime
                            (LocomotionHash , CrossFadeDuration);

        stateMachine.Animator.SetFloat(SpeedHash , 0.0f);
    }


    public override void Tick(float deltaTime)
    {
        Move(deltaTime);

        if (IsInChaseRange())
        {
            Debug.Log("チェイス圏内です。");

            //TODO : 追いかけるアニメーションを設定
            return;
        }

        stateMachine.Animator.SetFloat(SpeedHash , 0.0f , AnimatorDampTime, deltaTime);
    }


    public override void Exit()
    {
        throw new System.NotImplementedException();
    }
}
