using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    [SerializeField] private Collider myCollider;

    private List<Collider> alreadyCollidedWith = new List<Collider>();

    private int damage;

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
    }

    public void SetWeaponAttack_One(int weaponDamage_One)
    {
        this.damage = weaponDamage_One;
    }

    public void SetWeaponAttack_Two(int weaponDamage_Two)
    {
        this.damage = weaponDamage_Two;
    }
}
