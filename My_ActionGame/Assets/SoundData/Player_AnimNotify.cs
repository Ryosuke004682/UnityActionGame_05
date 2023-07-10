using UnityEngine;

public class Player_AnimNotify : AnimNotify
{
    [SerializeField] private Collider blockCollider;

    PlayerBlockingState blockingState;

    private void Start()
    {
        Debug.Log(blockingState.IsBlocking(true));
        Debug.Log(blockingState.IsBlocking(false));
    }

    public void PlayerAttackSound()
    {
        for (var i = 0; i < weaponAttackSE.Length; i++)
        {
            source.PlayOneShot(weaponAttackSE[Random.Range(0, weaponAttackSE.Length)]) ;
        }
    }

    public void PlayerBlockSE()
    {
        source.PlayOneShot(blockSE);
    }

    public void PlayerFootStepSE()
    {
        source.PlayOneShot(footStepSE);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("EnemyAttackPoint"))
        {
            
            source.PlayOneShot(blockSE);
        }
    }
}
