using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*攻撃について*/
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
        stateMachine.Weapon.SetWeaponAttack_One(attack.WeaponDamage , attack.Knockback);
        stateMachine.Foot  .SetWeaponAttack_Two  (attack.FootDamage , attack.Knockback);

        //トランジションがごちゃごちゃになるのが嫌なのでCrossFadeInFixedTimeを採用
        stateMachine.Animator.CrossFadeInFixedTime(attack.AnimationName , attack.TransitionDuration);
    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);
        FaceTarget();

        float normalizedTime = GetNormalizedTime(stateMachine.Animator);

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
        //攻撃モーションの境界範囲外
        if (attack.ComboStateIndex == -2)            { return; }

        //次の攻撃までの受付時間
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

}
