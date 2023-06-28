using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(AudioClip))]
public class AnimNotify_FootStep : MonoBehaviour
{
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip footStepSound;

    public void PlayerFootstepSound()
    {
        source.PlayOneShot(footStepSound);
    }

}
