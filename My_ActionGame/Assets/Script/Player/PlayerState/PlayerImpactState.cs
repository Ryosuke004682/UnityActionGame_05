using UnityEngine;

public class PlayerImpactState : PlayerBaseState
{

    private readonly int ImpactHash = Animator.StringToHash("DamageReaction");
    private const float CrossFadeDuration = 0.1f;

    private float duration = 1.0f;

    public PlayerImpactState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(ImpactHash , CrossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);

        duration -= deltaTime;

        if(duration <= 0.0f)
        {
            ReturnToLocomotion();
        }
    }

    public override void Exit()
    {
        
    }
}
