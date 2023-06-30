using System;
using UnityEngine;

public class PlayerFreeLookState : PlayerBaseState
{
    public PlayerFreeLookState(PlayerStateMachine stateMachine) : base(stateMachine){}

    private readonly int FreeLookSpeedHash = Animator.StringToHash("FreeLookSpeed");
    private const float AnimatorDampTime   = 0.1f;

    public override void Enter()
    {
        stateMachine.InputReader.TargetEvent += OnTarget;
    }

    public override void Tick(float deltaTime)
    {
        //移動スピード
        float speed      = stateMachine.FreeLookMoveSpeed;
        Vector2 input    = stateMachine.InputReader.MovementValue;
        Vector3 movement = new Vector3(input.x , 0 , input.y);


        stateMachine.transform .Translate(movement * deltaTime);
        stateMachine.Controller.Move(movement * speed * deltaTime);

        if (input == Vector2.zero)
        {
            //Playerが動かない時アニメーションを止める
            stateMachine.Animator.SetFloat(FreeLookSpeedHash, 0, AnimatorDampTime, deltaTime);
            return;
        }

        //Playerが動いてる時アニメーションを再生する。
        stateMachine.Animator.SetFloat(FreeLookSpeedHash, 1, AnimatorDampTime, deltaTime);

        FaceMovementDirection(movement , deltaTime);
    }

    public override void Exit()
    {
        stateMachine.InputReader.TargetEvent -= OnTarget;
    }

    private void OnTarget()
    {
        stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
    }


    private void FaceMovementDirection(Vector3 movement , float deltaTime)
    {
        stateMachine.transform.rotation = Quaternion.Lerp(
                                          stateMachine.transform.rotation,
                                          Quaternion.LookRotation(movement),
                                          deltaTime * stateMachine.RotationDamping);
    }

}
