using System;
using UnityEngine;

public class HealthEvent : MonoBehaviour
{
    public event Action<HealthEvent, HealthEventArgs> OnHealthChanged;

    public void CallHealthChangedEvent(bool increaseHealth, int amount)
    {
        OnHealthChanged?.Invoke(this, new HealthEventArgs()
        {
            increaseHealth = increaseHealth,
            amount = amount
        });
    }
}

public class HealthEventArgs : EventArgs
{
    public bool increaseHealth;
    public int amount;
}