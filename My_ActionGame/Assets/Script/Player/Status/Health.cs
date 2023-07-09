using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    
    
    private int  health;
    private bool isInvunerble;


    public event Action OnTakeDamage;
    public event Action OnDeath;

    private void Start()
    {
        health = maxHealth;
    }

    public void SetInvulnerable(bool isInvunerable)
    {
        this.isInvunerble = isInvunerable;
    }

    public void DealDamage(int damage)
    {
        if (health == 0)  { return; }

        if (isInvunerble) { return; }
        

        health = Mathf.Max(health - damage, 0);

        OnTakeDamage?.Invoke();

        if(health == 0)
        {
            OnDeath?.Invoke();
        }

        print(health);
    }
}
