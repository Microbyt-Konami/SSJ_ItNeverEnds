using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CharacterHealth : MonoBehaviour
{
    [field: SerializeField] public int MaxHealth { get; private set; } = 100;
    [field: SerializeField] public int Health { get; private set; } = 100;
    [field: SerializeField] public bool IsDead { get; private set; }

    // Events
    public UnityEvent<int, int> OnHealthChanged = new UnityEvent<int, int>(); // <old, new>
    public UnityEvent<CharacterHealth> OnDie = new UnityEvent<CharacterHealth>();

    private void Awake()
    {
        Health = MaxHealth;
    }

    private void OnValidate()
    {
        ModifyHealth(0);
    }

    public void TakeDamage(int damageValue)
    {
        ModifyHealth(-damageValue);
    }

    public void RestoreHealth(int healthValue)
    {
        ModifyHealth(healthValue);
    }

    private void ModifyHealth(int value)
    {
        if (IsDead)
            return;

        int newHealth = Health + value;

        newHealth = Mathf.Clamp(newHealth, 0, MaxHealth);
        OnHealthChanged.Invoke(Health, newHealth);

        Health = newHealth;

        if (Health == 0)
        {
            OnDie.Invoke(this);
            IsDead = true;
        }
    }
}
