using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.Collections;
using UnityEngine;

public class Targeter : MonoBehaviour
{
    [SerializeField] private List<Target> targets = new List<Target>();


    private void OnTriggerEnter(Collider other)
    {
        //他のコライダーが[target]を持っていた場合処理を抜けて、持っていた場合リストに追加する。
        if (!other.TryGetComponent<Target>(out Target target)) { return; }
        targets.Add(target);
        
    }

    private void OnTriggerExit(Collider other)
    {
        //他のコライダーが[target]を持っていた場合処理を抜けて、持っていた場合リストから外す。
        if (!other.TryGetComponent<Target>(out Target target)) { return; }
        targets.Remove(target);
    }
}
