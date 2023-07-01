using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*�^�[�Q�b�g�������C�x���g�����B*/
public class Target : MonoBehaviour
{
    public event Action<Target> OnDestroyed;

    private void OnDestroy()
    {
        OnDestroyed?.Invoke(this);
    }
}
