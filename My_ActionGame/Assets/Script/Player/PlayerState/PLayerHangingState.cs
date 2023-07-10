using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLayerHangingState : PlayerBaseState
{
    private Vector3 ledgeForward;
    private Vector3 closestPoint;

    private readonly int HandingHash = Animator.StringToHash("Hanging Idle");
    private const  float CrossFadeDuration = 0.1f;


    public PLayerHangingState(PlayerStateMachine stateMachine, 
                                         Vector3 ledgeForward,
                                         Vector3 closestPoint) : base(stateMachine)
    {
        this.ledgeForward = ledgeForward;
        this.closestPoint = closestPoint;
    }


    public override void Enter()
    {
        stateMachine.transform.rotation = Quaternion.LookRotation(ledgeForward , Vector3.up);


        stateMachine.Controller.enabled = false;
        stateMachine.transform.position = closestPoint - (stateMachine.LedgeDetector.transform.position 
                                                       - stateMachine.transform.position);
        stateMachine.Controller.enabled = true;



        stateMachine.Animator.CrossFadeInFixedTime(HandingHash , CrossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {

        if (stateMachine.InputReader.MovementValue.y > 0.0f)
        {
            stateMachine.SwitchState(new PlayerPullUpState(stateMachine));
        }
        else if(stateMachine.InputReader.MovementValue.y < 0.0f)
        {
            stateMachine.Controller.Move(Vector3.zero);
            stateMachine.Receiver  .Reset();
            stateMachine.SwitchState(new PlayerFallingState(stateMachine));
        }

    }

    public override void Exit()
    {
       
    }
}
