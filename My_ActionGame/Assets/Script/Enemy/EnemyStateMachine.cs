using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : StateMachine
{
    [field: SerializeField] public Animator              Animator { get; private set; }
    [field: SerializeField] public CharacterController Controller { get; private set; }
    [field: SerializeField] public ForceReceiver         Receiver { get; private set; }

    [field: SerializeField] public float PlayerChasingRange { get; private set; }

    public GameObject Player { get; private set; }

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");

        SwitchState(new EnemyIdleState(this));
    }

    //���G�͈͂����o�I�ɕ\���B�i�f�o�b�O���y������j
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position , PlayerChasingRange);
    }
}
