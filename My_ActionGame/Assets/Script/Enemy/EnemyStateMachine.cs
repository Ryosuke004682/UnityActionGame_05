using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateMachine : StateMachine
{
    [field: SerializeField] public Animator              Animator { get; private set; }
    [field: SerializeField] public CharacterController Controller { get; private set; }
    [field: SerializeField] public ForceReceiver         Receiver { get; private set; }
    [field: SerializeField] public NavMeshAgent             Agent { get; private set; }
    [field: SerializeField] public WeaponDamage            Weapon { get; private set; }

    [field: SerializeField] public int                 LeftAttack { get; private set; }
    [field: SerializeField] public int                RightAttack { get; private set; }
    [field: SerializeField] public float            MovementSpeed { get; private set; }
    [field: SerializeField] public float       PlayerChasingRange { get; private set; }
    [field: SerializeField] public float              AttackRange { get; private set; }


    public GameObject Player { get; private set; }

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Agent.updatePosition = false;
        Agent.updateRotation = false;

        SwitchState(new EnemyIdleState(this));
    }

    /*デバッグしやすいように検知範囲を可視化*/
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position , PlayerChasingRange);
    }
}
