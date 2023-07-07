using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemyAttackingState : EnemyBaseState
{
    private readonly int LeftAttack  = Animator.StringToHash("Attack");

    private Attack attack;

    private const  float  TransitionDuration = 0.1f;

    public EnemyAttackingState(EnemyStateMachine stateMachine) : base(stateMachine) 
    {
        
    }

    public override void Enter()
    {
        stateMachine.Weapon.SetWeaponAttack_One(stateMachine.LeftAttack , stateMachine.AttackKnockback);


        stateMachine.Animator.CrossFadeInFixedTime(LeftAttack , TransitionDuration);
    }

    public override void Tick(float deltaTime)
    {
        if (GetNormalizedTime(stateMachine.Animator) >= 1)
        {
            stateMachine.SwitchState(new EnemyChasingState(stateMachine));
        }
    }

    public override void Exit()
    {

    }
}
