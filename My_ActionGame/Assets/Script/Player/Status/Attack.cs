using System;
using UnityEngine;

[Serializable]
public class Attack
{
    [field: SerializeField,Tooltip("ステートの名前を格納")]
    public string     AnimationName { get; private set; }

    [field: SerializeField,Tooltip("次のアニメーションまで遷移する時間")]
    public float    ComboAttackTime { get; private set; }

    [field: SerializeField,Tooltip("アニメーションの挙動をきびきび動かす")]
    public float TransitionDuration { get; private set; }

    [field: SerializeField,Tooltip("力を加える時間")]
    public float          ForceTime { get; private set; }
    
    [field: SerializeField,Tooltip("攻撃時に前方に進む力")]
    public float              Force { get; private set; }

    [field: SerializeField]
    public int      ComboStateIndex { get; private set; } = -1;

    [field: SerializeField,Tooltip("剣での攻撃力")]
    public int         WeaponDamage { get; private set; }

    [field: SerializeField,Tooltip("蹴りでの攻撃力")]
    public int           FootDamage { get; private set; }
}