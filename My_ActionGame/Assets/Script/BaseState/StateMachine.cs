using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    private State currentState;

    //実行する前のステート、または実行中のステートを遷移させる場所
    public void SwitchState(State newState)
    {
        currentState?.Exit();
        
        currentState = newState;
        
        currentState?.Enter();
    }

    //マイフレームステートを実行するところ。
    private void Update()
    {
        currentState?.Tick(Time.deltaTime);
    }
}
