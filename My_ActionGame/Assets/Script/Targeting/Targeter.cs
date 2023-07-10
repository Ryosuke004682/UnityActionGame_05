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
        //���̃R���C�_�[��[target]�������Ă����ꍇ�����𔲂��āA�����Ă����ꍇ���X�g�ɒǉ�����B
        if (!other.TryGetComponent<Target>(out Target target)) { return; }

        targets.Add(target);
        target.OnDestroyed += RemoveTarget;
    }

    private void OnTriggerExit(Collider other)
    {
        //���̃R���C�_�[��[target]�������Ă����ꍇ�����𔲂��āA�����Ă����ꍇ���X�g����O���B
        if (!other.TryGetComponent<Target>(out Target target)) { return; }

        RemoveTarget(target); //�͈͊O�ɍs�����玩���Ńt�H�[�J�X����������
    }


    //��������̋�������ԋ߂��G�Ƀt�H�[�J�X�𓖂Ă�A�t�H�[�J�X�𓖂Ă����ɓG�̕���������
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

    //�t�H�[�J�X������
    public void Cancel()
    {
        if (CurrentTarget == null) { return; }
       
        targetGroup.RemoveMember(CurrentTarget.transform);
        CurrentTarget = null;
    }

    //�^�[�Q�b�g���폜����
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
