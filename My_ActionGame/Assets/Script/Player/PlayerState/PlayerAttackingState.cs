using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*�U���ɂ���*/
public class PlayerAttackingState : PlayerBaseState
{
    private float previousFrameTime;
    private bool alreadyAppliedForce;

    private Attack attack;

    public PlayerAttackingState(PlayerStateMachine stateMachine, int attackIndex) : base(stateMachine)
    {
        attack = stateMachine.Attacks[attackIndex];
    }

    public override void Enter()
    {
        stateMachine.Weapon.SetWeaponAttack(attack.WeaponDamage);
        stateMachine.Foot  .SetFootAttack  (attack.FootDamage);

        //�g�����W�V�����������Ⴒ����ɂȂ�̂����Ȃ̂�CrossFadeInFixedTime���̗p
        stateMachine.Animator.CrossFadeInFixedTime(attack.AnimationName , attack.TransitionDuration);
    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);
        FaceTarget();

        float normalizedTime = GetNormalizedTime();

        if(normalizedTime >= previousFrameTime && normalizedTime < 1.0f)
        {
            if(normalizedTime >= attack.ForceTime)
            {
                TryApplyForce();
            }

            if(stateMachine.InputReader.IsAttacking)
            {
                TryComboAttack(normalizedTime);
            }
        }
        else
        {
            if(stateMachine.Targeter.CurrentTarget != null)
            {
                stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
            }
            else
            {
                stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
            }
        }
        previousFrameTime    = normalizedTime;
    }

    public override void Exit()
    {
        
    }


    private void TryComboAttack(float normalizedTime)
    {
        //�U�����[�V�����̋��E�͈͊O
        if (attack.ComboStateIndex == -2)            { return; }

        //���̍U���܂ł̎�t����
        if (normalizedTime < attack.ComboAttackTime) { return; }

        stateMachine.SwitchState
            (new PlayerAttackingState(stateMachine, attack.ComboStateIndex));
    }

    private void TryApplyForce()
    {
        if (alreadyAppliedForce) { return; }

        stateMachine.Receiver.AddForce(stateMachine.transform.forward * attack.Force);

        alreadyAppliedForce = true;
    }


    private float GetNormalizedTime()
    {
        AnimatorStateInfo currentInfo = stateMachine.Animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo    nextInfo = stateMachine.Animator.GetNextAnimatorStateInfo(0);

        if(stateMachine.Animator.IsInTransition(0) && nextInfo.IsTag("Attack"))
        {
            return nextInfo.normalizedTime;
        }
        else if(!stateMachine.Animator.IsInTransition(0) && currentInfo.IsTag("Attack"))
        {
            return currentInfo.normalizedTime;
        }
        else
        {
            return 0.0f;
        }
    }
}
