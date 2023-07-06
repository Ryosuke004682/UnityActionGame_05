using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemyAttackingState : EnemyBaseState
{
    private readonly int LeftAttack  = Animator.StringToHash("EnemyAttack");
    private readonly int RightAttack = Animator.StringToHash("EnemyAttack1");

    private Attack attack;

    private const  float  TransitionDuration = 0.1f;

    public EnemyAttackingState(EnemyStateMachine stateMachine) : base(stateMachine) 
    {
        
    }

    public override void Enter()
    {
        stateMachine.Weapon.SetWeaponAttack_One(stateMachine.LeftAttack);


        stateMachine.Animator.CrossFadeInFixedTime(LeftAttack , TransitionDuration);
    }

    public override void Tick(float deltaTime)
    {
       
    }

    public override void Exit()
    {

    }
}
