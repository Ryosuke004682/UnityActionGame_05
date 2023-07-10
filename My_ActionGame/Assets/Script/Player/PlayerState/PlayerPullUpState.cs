using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPullUpState : PlayerBaseState
{
    private readonly int PullUpHash = Animator.StringToHash("Freehang Climb");
    private readonly Vector3 Offset = new Vector3(0.0f , 2.5f , 0.6f);

    private const float CrossFadeDuration = 0.1f;
    


    public PlayerPullUpState(PlayerStateMachine stateMachine) : base(stateMachine) { }


    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(PullUpHash , CrossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        if (GetNormalizedTime(stateMachine.Animator , "Climbing") < 1.0f) { return; }

        stateMachine.Controller.enabled = false;
        stateMachine.transform.Translate(Offset , Space.Self);
        stateMachine.Controller.enabled = true;

        stateMachine.SwitchState(new PlayerFreeLookState(stateMachine , false));
    }

    public override void Exit()
    {
        stateMachine.Controller.Move(Vector3.zero);
        stateMachine.Receiver.Reset();
    }
}
