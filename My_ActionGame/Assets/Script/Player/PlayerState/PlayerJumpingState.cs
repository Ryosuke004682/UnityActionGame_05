using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpingState : PlayerBaseState
{
    private readonly int JumpHash = Animator.StringToHash("Jumping");

    private Vector3 momentum;

    public PlayerJumpingState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    private const float CrossFadeDuration = 0.1f;

    public override void Enter()
    {
        stateMachine.Receiver.Jump(stateMachine.JumpForce);

        momentum   = stateMachine.Controller.velocity;
        momentum.y = 0.0f;

        stateMachine.Animator.CrossFadeInFixedTime(JumpHash , CrossFadeDuration);

        stateMachine.LedgeDetector.OnLedgeDetect += HandleLedgeDetect;
    }


    public override void Tick(float deltaTime)
    {
        Move(momentum , deltaTime);

        if(stateMachine.Controller.velocity.y <= 0.0f)
        {
            stateMachine.SwitchState(new PlayerFallingState(stateMachine));
            return;
        }

        FaceTarget();
    }


    public override void Exit()
    {
        stateMachine.LedgeDetector.OnLedgeDetect -= HandleLedgeDetect;
    }

    private void HandleLedgeDetect(Vector3 ledgeForward , Vector3 closestPoint)
    {
        stateMachine.SwitchState(new PLayerHangingState(stateMachine , ledgeForward , closestPoint));
    }

}
