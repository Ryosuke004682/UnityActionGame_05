using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(AudioClip))]
public class AnimNotify_FootStep : MonoBehaviour
{
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip   footStepSound;
    [SerializeField] AudioClip[] weaponAttackSound;


    private void Awake()
    {
        source.playOnAwake = false;
    }

    public void PlayFootstepSound()
    {
        source.PlayOneShot(footStepSound);
    }

    public void AttackSound()
    {
        for(var i = 0; i < weaponAttackSound.Length; i++)
        {
            source.PlayOneShot(weaponAttackSound[Random.Range(i, weaponAttackSound.Length)]);
        }
    }

    public void StopSound()
    {
        source.Stop();
    }
}
