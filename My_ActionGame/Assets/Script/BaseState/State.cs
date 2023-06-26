using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*ステート構造の設計をする*/
public abstract class State
{
    public abstract void Enter();

    public abstract void Tick(float deltaTime);

    public abstract void Exit();

}
