using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerTargetingState : PlayerBaseState
{

    private readonly int TargetingBlendTreeHash = Animator.StringToHash("TargetingBlendTree");
    private readonly int   TargetingForwardHash = Animator.StringToHash("TargetingForward");
    private readonly int     TargetingRightHash = Animator.StringToHash("TargetingRight");


    public PlayerTargetingState(PlayerStateMachine stateMachine) : base(stateMachine) { }
    
    public override void Enter()
    {
        stateMachine.InputReader.CancelEvent += OnCancel;
        stateMachine.Animator.Play(TargetingBlendTreeHash);
    }

    public override void Tick(float deltaTime)
    {
        if(stateMachine.Targeter.CurrentTarget == null)
        {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
            return;
        }

        Vector3 movement = CalcurationMovement();
        Move(movement * stateMachine.TargetingMoveSpeed, deltaTime);

        UpdateAnimator(deltaTime);
        //ロックオンしたときのPlayer移動を設定する
        FaceTarget();
    }

    public override void Exit()
    {
        stateMachine.InputReader.CancelEvent -= OnCancel;
    }

    private void OnCancel()
    {
        stateMachine.Targeter.Cancel();

        stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
    }

    private Vector3 CalcurationMovement()
    {
        Vector3 movement = new Vector3();

        movement += stateMachine.transform.right   * stateMachine.InputReader.MovementValue.x;
        movement += stateMachine.transform.forward * stateMachine.InputReader.MovementValue.y;

        return  movement;
    }

    /*ロックオンした時の移動*/
    private void UpdateAnimator(float deltaTime)
    {
        if(stateMachine.InputReader.MovementValue.y == 0)
        {
            stateMachine.Animator.SetFloat(TargetingForwardHash, 0.0f , 0.1f, deltaTime);
        }
        else
        {
            float value = stateMachine.InputReader.MovementValue.y > 0 ? 1.0f : -1.0f;
            stateMachine.Animator.SetFloat(TargetingForwardHash, value, 0.1f , deltaTime);
        }


        if(stateMachine.InputReader.MovementValue.x == 0)
        {
            stateMachine.Animator.SetFloat(TargetingRightHash, 0.0f , 0.1f , deltaTime);
        }
        else
        {
            float value = stateMachine.InputReader.MovementValue.x > 0 ? 1.0f : -1.0f;
            stateMachine.Animator.SetFloat(TargetingRightHash, value , 0.1f , deltaTime);
        }


    }


}
