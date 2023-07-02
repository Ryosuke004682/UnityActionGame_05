using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    [field: SerializeField] public InputReader        InputReader { get; private set; }
    [field: SerializeField] public CharacterController Controller { get; private set; }
    [field: SerializeField] public Animator              Animator { get; private set; }
    [field: SerializeField] public Targeter              Targeter { get; private set; }
    [field: SerializeField] public ForceReceiver       Receiveret { get; private set; }

    [field: SerializeField] public float      TargetingMoveSpeed { get; private set; }
    [field: SerializeField] public float        FreeLookMoveSpeed { get; private set; }
    [field: SerializeField] public float          RotationDamping { get; private set; }

    private void Start()
    {
        SwitchState(new PlayerFreeLookState(this));
    }

}
