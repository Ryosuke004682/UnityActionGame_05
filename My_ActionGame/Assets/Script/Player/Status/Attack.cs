using System;
using UnityEngine;

[Serializable]
public class Attack
{
    [field: SerializeField,Tooltip("�X�e�[�g�̖��O���i�[")]
    public string     AnimationName { get; private set; }

    [field: SerializeField,Tooltip("���̃A�j���[�V�����܂őJ�ڂ��鎞��")]
    public float    ComboAttackTime { get; private set; }

    [field: SerializeField,Tooltip("�A�j���[�V�����̋��������т��ѓ�����")]
    public float TransitionDuration { get; private set; }

    [field: SerializeField,Tooltip("�͂������鎞��")]
    public float          ForceTime { get; private set; }
    
    [field: SerializeField,Tooltip("�U�����ɑO���ɐi�ޗ�")]
    public float              Force { get; private set; }

    [field: SerializeField]
    public int      ComboStateIndex { get; private set; } = -1;

    [field: SerializeField,Tooltip("���ł̍U����")]
    public int         WeaponDamage { get; private set; }

    [field: SerializeField,Tooltip("�R��ł̍U����")]
    public int           FootDamage { get; private set; }
}