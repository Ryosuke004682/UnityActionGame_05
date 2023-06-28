using UnityEngine;

public class PlayerTestState : PlayerBaseState
{
    public PlayerTestState(PlayerStateMachine stateMachine) : base(stateMachine){}

    public override void Enter()
    {
       
    }

    public override void Tick(float deltaTime)
    {
        //�ړ��X�s�[�h
        float speed      = stateMachine.FreeLookMoveSpeed;
        Vector2 input    = stateMachine.InputReader.MovementValue;
        Vector3 movement = new Vector3(input.x , 0 , input.y);


        stateMachine.transform .Translate(movement * deltaTime);
        stateMachine.Controller.Move(movement * speed * deltaTime);

        if (input == Vector2.zero)
        {
            //Player�������Ȃ����A�j���[�V�������~�߂�
            stateMachine.Animator.SetFloat("FreeLookSpeed" , 0, 0.1f, deltaTime);
            return;
        }

        //Player�������Ă鎞�A�j���[�V�������Đ�����B
        stateMachine.Animator.SetFloat("FreeLookSpeed", 1, 0.1f, deltaTime);
        stateMachine.transform.rotation = Quaternion.LookRotation(movement);
    }

    public override void Exit()
    {
        
    }
}
