using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*攻撃について*/
public class PlayerAttackingState : PlayerBaseState
{
    private Attack attack;

    public PlayerAttackingState(PlayerStateMachine stateMachine, int attackID) : base(stateMachine)
    {
        attack = stateMachine.Attacks[attackID];
    }

    public override void Enter()
    {
        //トランジションがごちゃごちゃになるのが嫌なのでCrossFadeInFixedTimeを採用
        stateMachine.Animator.CrossFadeInFixedTime(attack.animationName , 0.1f);
    }

    public override void Tick(float deltaTime)
    {
        
    }

    public override void Exit()
    {
        
    }
}
