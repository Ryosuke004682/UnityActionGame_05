using System.Reflection;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(AudioClip))]
public class AnimNotify : MonoBehaviour
{
    [SerializeField] protected AudioSource         source;
    [SerializeField] protected AudioClip       footStepSE;
    [SerializeField] protected AudioClip          blockSE;
    [SerializeField] protected AudioClip[] weaponAttackSE;
    [SerializeField] protected AudioClip[]         blowSE;

    private void Awake()
    {
        source.playOnAwake = false;
    }

    public void StopSound()
    {
        source.Stop();
    }
}
