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


    //��������̋�������ԋ߂��G�Ƀt�H�[�J�X�𓖂Ă�
    public bool SelectTarget()
    {
        if (targets.Count == 0) { return false; }

        CurrentTarget = targets[0];
        targetGroup.AddMember(CurrentTarget.transform , 1.0f , 2.0f);

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
            CurrentTarget = null;
        }

        target.OnDestroyed -= RemoveTarget;
        targets.Remove(target);
    }
}