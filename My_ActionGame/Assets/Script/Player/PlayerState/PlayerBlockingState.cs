using Microsoft.Win32.SafeHandles;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerBlockingState : PlayerBaseState
{
    private readonly int BlockHash = Animator.StringToHash("Block");

    private const float CrossFadeDuration = 0.1f;

    public bool IsUnderAttack = false;

    public PlayerBlockingState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        stateMachine.Health  .SetInvulnerable(true);
        stateMachine.Animator.CrossFadeInFixedTime(BlockHash , CrossFadeDuration);

    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);

        if (!stateMachine.InputReader.IsBlocking)
        {
            IsBlocking(false);

            stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
            return;
        }
        if(stateMachine.Targeter.CurrentTarget == null)
        {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
            return;
        }

        IsBlocking(true);
        
    }

    public override void Exit()
    {
        stateMachine.Health.SetInvulnerable(false);
    }

    /*ガード用のコライダーの作成（これに当たったら効果音を流すように設定）*/
    public bool IsBlocking(bool block)
    {
       return stateMachine.BlockingCollider.enabled = block;
    }
}
