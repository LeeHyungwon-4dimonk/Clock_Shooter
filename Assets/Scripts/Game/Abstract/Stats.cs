using System;
using UnityEngine;

public abstract class Stats
{
    public int MaxHP {  get; private set; }
    public int CurrentHP { get; private set; }
    public event Action OnStatusChanged;

    protected void InitiailizeHP(int hp)
    {
        MaxHP = hp;
        CurrentHP = hp;
    }

    public virtual void TakeDamage(int damage)
    {
        CurrentHP = Mathf.Max(CurrentHP - damage, 0);
        OnStatusChanged?.Invoke();
    }

    protected void NotifyStatusChanged()
    {
        OnStatusChanged?.Invoke();
    }
}