using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    [field: SerializeField] public InputReader        InputReader { get; private set; }
    [field: SerializeField] public CharacterController Controller { get; private set; }
    [field: SerializeField] public Animator              Animator { get; private set; }
    [field: SerializeField] public Targeter              Targeter { get; private set; }
    [field: SerializeField] public ForceReceiver         Receiver { get; private set; }
    [field: SerializeField] public WeaponDamage            Weapon { get; private set; }
    [field: SerializeField] public WeaponDamage              Foot { get; private set; }
    [field: SerializeField] public Attack[]               Attacks { get; private set; }
    [field: SerializeField] public Health                  Health { get; private set; }
    [field: SerializeField] public Ragdoll                Ragdoll { get; private set; }


    [field: SerializeField] public float TargetingMoveSpeed { get; private set; }
    [field: SerializeField] public float FreeLookMoveSpeed  { get; private set; }
    [field: SerializeField] public float RotationDamping    { get; private set; }

    private void Start()
    {
        SwitchState(new PlayerFreeLookState(this));
    }

    private void OnEnable()
    {
        Health.OnTakeDamage += HandleTakeDamage;
        Health.OnDeath      += HandleDie;

        Debug.Log("呼ばれてるぜ");
    }

    private void OnDisable()
    {
        Health.OnTakeDamage -= HandleTakeDamage;
        Health.OnDeath      -= HandleDie;
    }

    private void HandleTakeDamage()
    {
        SwitchState(new PlayerImpactState(this));
    }

    private void HandleDie()
    {
        SwitchState(new PlayerDeathState(this));
    }

}
