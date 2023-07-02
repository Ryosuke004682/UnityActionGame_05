using Cinemachine;
using System.Collections.Generic;
using UnityEngine;

public class Targeter : MonoBehaviour
{
    [SerializeField] private CinemachineTargetGroup targetGroup;

    private List<Target> targets = new List<Target>();
    public  Target CurrentTarget { get; private set; }


    private void OnTriggerEnter(Collider other)
    {
        //他のコライダーが[target]を持っていた場合処理を抜けて、持っていた場合リストに追加する。
        if (!other.TryGetComponent<Target>(out Target target)) { return; }
        
        targets.Add(target);
        target.OnDestroyed += RemoveTarget;
        
    }

    private void OnTriggerExit(Collider other)
    {
        //他のコライダーが[target]を持っていた場合処理を抜けて、持っていた場合リストから外す。
        if (!other.TryGetComponent<Target>(out Target target)) { return; }

        RemoveTarget(target); //範囲外に行ったら自動でフォーカスを解除する
    }


    //自分からの距離が一番近い敵にフォーカスを当てる
    public bool SelectTarget()
    {
        if (targets.Count == 0) { return false; }

        CurrentTarget = targets[0];
        targetGroup.AddMember(CurrentTarget.transform , 0.2f , 1.5f);

        return true;
    }

    //フォーカスを解除
    public void Cancel()
    {
        if (CurrentTarget == null) { return; }
       
        targetGroup.RemoveMember(CurrentTarget.transform);
        CurrentTarget = null;
    }

    //ターゲットを削除する
    private void RemoveTarget(Target target)
    {
        if(CurrentTarget == target)
        {
            targetGroup.RemoveMember(CurrentTarget.transform);
            CurrentTarget = null;
        }

        target.OnDestroyed -= RemoveTarget;
        targets.Remove(target);
    }
}
