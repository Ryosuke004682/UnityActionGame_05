using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*�U���ɂ���*/
public class PlayerAttackingState : PlayerBaseState
{
    private Attack attack;

    public PlayerAttackingState(PlayerStateMachine stateMachine, int attackID) : base(stateMachine)
    {
        attack = stateMachine.Attacks[attackID];
    }

    public override void Enter()
    {
        //�g�����W�V�����������Ⴒ����ɂȂ�̂����Ȃ̂�CrossFadeInFixedTime���̗p
        stateMachine.Animator.CrossFadeInFixedTime(attack.animationName , 0.1f);
    }

    public override void Tick(float deltaTime)
    {
        
    }

    public override void Exit()
    {
        
    }
}
