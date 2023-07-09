using UnityEngine;

public class PlayerDeathState : PlayerBaseState
{
    public PlayerDeathState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        stateMachine.Ragdoll.ToggleRagdoll(true);//Ž€–S‚³‚¹‚é
        stateMachine.Weapon.gameObject.SetActive(false);

        Debug.Log("ŒÄ‚Î‚ê‚Ä‚é‚º");
    }

    public override void Tick(float deltaTime)
    {

    }

    public override void Exit()
    {
        
    }
}
