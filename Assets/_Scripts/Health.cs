using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Health : MonoBehaviour
{
    public int maxHealth;
    
    protected int currentHealth;

    // Start is called before the first frame update
    public virtual void Awake()
    {
        currentHealth = maxHealth;
    }

    public virtual void ChangeHealth(int _change, GameObject _from)
    {
        currentHealth += _change;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    protected abstract void Die();
}
