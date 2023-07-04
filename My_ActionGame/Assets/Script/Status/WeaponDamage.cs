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

    public void SetWeaponAttack(int weaponDamage)
    {
        this.damage = weaponDamage;
    }

    public void SetFootAttack(int footDamage)
    {
        this.damage = footDamage;
    }
}
