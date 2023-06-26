using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    private State currentState;

    //���s����O�̃X�e�[�g�A�܂��͎��s���̃X�e�[�g��J�ڂ�����ꏊ
    public void SwitchState(State newState)
    {
        currentState?.Exit();
        
        currentState = newState;
        
        currentState?.Enter();
    }

    //�}�C�t���[���X�e�[�g�����s����Ƃ���B
    private void Update()
    {
        currentState?.Tick(Time.deltaTime);
    }
}
