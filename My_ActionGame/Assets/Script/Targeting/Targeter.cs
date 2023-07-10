using Cinemachine;
using System.Collections.Generic;
using UnityEngine;

public class Targeter : MonoBehaviour
{
    [SerializeField] private CinemachineTargetGroup targetGroup;


    private Camera mainCamera;
    private List<Target> targets = new List<Target>();
    public  Target CurrentTarget { get; private set; }

    private void Start()
    {
        mainCamera = Camera.main;
    }


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


    //自分からの距離が一番近い敵にフォーカスを当てる、フォーカスを当てた時に敵の方向を向く
    public bool SelectTarget()
    {
        if (targets.Count == 0) { return false; }

        Target  closestTarget = null;
        float closestDistance = Mathf.Infinity;


        foreach (Target target in targets)
        {
            Vector2 viewPos = mainCamera.WorldToViewportPoint(target.transform.position);

            if (!target.GetComponentInChildren<Renderer>().isVisible)
            {
                continue;
            }

            Vector2 toCenter = viewPos - new Vector2(0.5f , 0.5f);
            
            if(toCenter.sqrMagnitude < closestDistance)
            {
                closestTarget   = target;
                closestDistance = toCenter.sqrMagnitude;
            }
        }

        if (closestTarget == null) { return false; }

        CurrentTarget = closestTarget;
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
            CurrentTarget   = null;
        }

        target.OnDestroyed -= RemoveTarget;
        targets.Remove(target);
    }
}
