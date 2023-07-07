using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    [SerializeField] private Collider myCollider;

    private List<Collider> alreadyCollidedWith = new List<Collider>();

    private int   damage;
    private float Knockback;

    private void OnEnable()
    {
        alreadyCollidedWith.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == myCollider)                 { return; }
        if (alreadyCollidedWith.Contains(other)) { return; }

        alreadyCollidedWith.Add(other);

        if (other.TryGetComponent<Health>(out Health health))
        {
            health.DealDamage(damage);//É_ÉÅÅ[ÉWÇó^Ç¶ÇÈ
        }

        if(other.TryGetComponent<ForceReceiver>(out ForceReceiver forceReceiver))
        {
            Vector3 direction = ((other.transform.position - myCollider.transform.position).normalized);


            forceReceiver.AddForce(direction * Knockback);
        }

    }

    public void SetWeaponAttack_One(int weaponDamage_One , float knockback)
    {
        this.damage = weaponDamage_One;
        this.Knockback = knockback;
    }

    public void SetWeaponAttack_Two(int weaponDamage_Two , float knockback)
    {
        this.damage = weaponDamage_Two;
        this.Knockback = knockback;
    }
}
