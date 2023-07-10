using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AnimNotify : AnimNotify
{
    public void EnemyAttackSound()
    {
        for (var i = 0; i < weaponAttackSE.Length; i++)
        {
            source.PlayOneShot(weaponAttackSE[Random.Range(0, weaponAttackSE.Length)]);
        }
    }

    public void EnemyBlockSE()
    {
        source.PlayOneShot(blockSE);
    }

    public void EnemyFootStepSE()
    {
        source.PlayOneShot(footStepSE);
    }
}
