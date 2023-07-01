using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*ターゲットを消すイベントを作る。*/
public class Target : MonoBehaviour
{
    public event Action<Target> OnDestroyed;

    private void OnDestroy()
    {
        OnDestroyed?.Invoke(this);
    }
}
