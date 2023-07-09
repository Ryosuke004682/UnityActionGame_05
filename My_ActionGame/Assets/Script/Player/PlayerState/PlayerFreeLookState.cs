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
            //Player�������Ȃ����A�j���[�V�������~�߂�
            stateMachine.Animator.SetFloat(FreeLookSpeedHash, 0.0f, AnimatorDampTime, deltaTime);
            return;
        }

        //Player�������Ă鎞�A�j���[�V�������Đ�����B
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


    //���炩�ɉ�]������
    private void FaceMovementDirection(Vector3 movement , float deltaTime)
    {
        stateMachine.transform.rotation = Quaternion.Lerp(
                                          stateMachine.transform.rotation  ,
                                          Quaternion.LookRotation(movement),
                                          deltaTime * stateMachine.RotationDamping);
    }

}
