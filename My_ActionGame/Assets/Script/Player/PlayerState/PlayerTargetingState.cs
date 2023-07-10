using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerTargetingState : PlayerBaseState
{
    private Vector2 dodgingDirectionInput;
    private float      remainingDodgeTime;

    private readonly int TargetingBlendTreeHash = Animator.StringToHash("TargetingBlendTree");
    private readonly int TargetingForwardHash   = Animator.StringToHash("TargetingForward");
    private readonly int TargetingRightHash     = Animator.StringToHash("TargetingRight");

    private const  float CrossFadeDuration      = 0.1f;

    public PlayerTargetingState(PlayerStateMachine stateMachine) : base(stateMachine) { }
    
    public override void Enter()
    {
        stateMachine.InputReader.TargetEvent += OnTarget;
        stateMachine.InputReader.DodgeEvent  += OnDodge ;
        stateMachine.InputReader.JumpEvent   += OnJump  ;


        stateMachine.Animator.CrossFadeInFixedTime(TargetingBlendTreeHash , CrossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        if(stateMachine.InputReader.IsAttacking)
        {
            stateMachine.SwitchState(new PlayerAttackingState(stateMachine , 0));
            return;
        }

        if(stateMachine.InputReader.IsBlocking)
        {
            stateMachine.SwitchState(new PlayerBlockingState(stateMachine));
            return;
        }


        if(stateMachine.Targeter.CurrentTarget == null)
        {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
            return;
        }

        Vector3 movement = CalcurationMovement(deltaTime);
        Move(movement * stateMachine.TargetingMoveSpeed, deltaTime);

        UpdateAnimator(deltaTime);
        //ロックオンしたときのPlayer移動を設定する
        FaceTarget();
    }

    public override void Exit()
    {
        stateMachine.InputReader.TargetEvent -= OnTarget;
        stateMachine.InputReader.DodgeEvent  -= OnDodge ;
        stateMachine.InputReader.JumpEvent   -= OnJump  ;
    }

    private void OnTarget()
    {
        stateMachine.Targeter.Cancel();

        stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
    }

    private void OnDodge()
    {
        if (Time.time - stateMachine.PreviousDodgeTime < stateMachine.DodgeCooldown) { return; }


        //回避のクールタイムを設定
        stateMachine.SetDodgeTime(Time.time);

        dodgingDirectionInput = stateMachine.InputReader.MovementValue;
        remainingDodgeTime    = stateMachine.DodgeDuration;

        stateMachine.Particle.Play();
        stateMachine.MyGameObject.SetActive(false);
    }

    private void OnJump()
    {
        stateMachine.SwitchState(new PlayerJumpingState(stateMachine));
    }



    //戦闘時の移動ベクトル
    private Vector3 CalcurationMovement(float deltaTime)
    {
        Vector3 movement = new Vector3();


        //通常時の移動ベクトル
        var movementRigth   = stateMachine.transform.right   * stateMachine.InputReader.MovementValue.x;

        var movementForward = stateMachine.transform.forward * stateMachine.InputReader.MovementValue.y;

        
        //回避時の移動ベクトル
        var dodgeMovement_Right   = stateMachine.transform.right   * dodgingDirectionInput.x 
                                      * stateMachine.DodgeLength / stateMachine.DodgeDuration;

        var dodgeMovement_Forward = stateMachine.transform.forward * dodgingDirectionInput.y 
                                      * stateMachine.DodgeLength / stateMachine.DodgeDuration;


        if (remainingDodgeTime > 0.0f)
        {
            movement += dodgeMovement_Right  ;
            movement += dodgeMovement_Forward;

            remainingDodgeTime = Mathf.Max(remainingDodgeTime - deltaTime , 0.0f);
        }
        else
        {
            stateMachine.MyGameObject.SetActive(true);
            stateMachine.Particle.Stop();
            movement += movementRigth;
            movement += movementForward;
        }

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
