using UnityEngine;

public class PlayerDeathState : PlayerBaseState
{
    public PlayerDeathState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        stateMachine.Ragdoll.ToggleRagdoll(true);//死亡させる
        stateMachine.Weapon.gameObject.SetActive(false);

        Debug.Log("呼ばれてるぜ");
    }

    public override void Tick(float deltaTime)
    {

    }

    public override void Exit()
    {
        
    }
}
