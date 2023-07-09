using System;
using UnityEngine;

public class PlayerFreeLookState : PlayerBaseState
{
    public PlayerFreeLookState(PlayerStateMachine stateMachine) : base(stateMachine){}


    private readonly int FreelookBlendTreeHash = Animator.StringToHash("FreeLookBlendTree");
    private readonly int     FreeLookSpeedHash = Animator.StringToHash("FreeLookSpeed");
    private const  float      AnimatorDampTime = 0.1f;

    private const float CrossFadeDuration = 0.1f;

    public override void Enter()
    {
        stateMachine.InputReader.TargetEvent += OnTarget;
        stateMachine.Animator.CrossFadeInFixedTime(FreelookBlendTreeHash , CrossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {

        if(stateMachine.InputReader.IsAttacking)
        {
            stateMachine.SwitchState(new PlayerAttackingState(stateMachine , 0));
            return;
        }


        Vector2 input    = stateMachine.InputReader.MovementValue;
        Vector3 movement = new Vector3(input.x , 0 , input.y);


        stateMachine.transform .Translate   (movement * deltaTime);

        Move(movement * stateMachine.FreeLookMoveSpeed , deltaTime);


        if (input == Vector2.zero)
        {
            //Playerが動かない時アニメーションを止める
            stateMachine.Animator.SetFloat(FreeLookSpeedHash, 0.0f, AnimatorDampTime, deltaTime);
            return;
        }

        //Playerが動いてる時アニメーションを再生する。
        stateMachine.Animator.SetFloat(FreeLookSpeedHash, 1.0f, AnimatorDampTime, deltaTime);

        FaceMovementDirection(movement , deltaTime);
    }

    public override void Exit()
    {
        stateMachine.InputReader.TargetEvent -= OnTarget;
    }

    private void OnTarget()
    {
        if (!stateMachine.Targeter.SelectTarget()) { return; }


        stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
    }


    //滑らかに回転させる
    private void FaceMovementDirection(Vector3 movement , float deltaTime)
    {
        stateMachine.transform.rotation = Quaternion.Lerp(
                                          stateMachine.transform.rotation  ,
                                          Quaternion.LookRotation(movement),
                                          deltaTime * stateMachine.RotationDamping);
    }

}
