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
        //���̃R���C�_�[��[target]�������Ă����ꍇ�����𔲂��āA�����Ă����ꍇ���X�g�ɒǉ�����B
        if (!other.TryGetComponent<Target>(out Target target)) { return; }
        targets.Add(target);
        
    }

    private void OnTriggerExit(Collider other)
    {
        //���̃R���C�_�[��[target]�������Ă����ꍇ�����𔲂��āA�����Ă����ꍇ���X�g����O���B
        if (!other.TryGetComponent<Target>(out Target target)) { return; }
        targets.Remove(target);
    }
}
