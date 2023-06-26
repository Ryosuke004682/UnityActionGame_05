using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*�X�e�[�g�\���̐݌v������*/
public abstract class State
{
    public abstract void Enter();

    public abstract void Tick(float deltaTime);

    public abstract void Exit();

}
