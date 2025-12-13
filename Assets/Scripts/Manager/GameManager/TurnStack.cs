using System;
using UnityEngine;

public class TurnStack
{
    public int Current { get; private set; }

    public event Action<int, int> OnChanged;

    public TurnStack(int initialValue = 0)
    {
        Current = initialValue;
    }

    public void Add(int amount = 1)
    {
        if (amount <= 0) return;

        int prev = Current;
        Current += amount;
        OnChanged?.Invoke(prev, Current);
    }

    public bool TrySub(int amount = 1)
    {
        if(Current < amount) return false;

        int prev = Current;
        Current -= amount;
        OnChanged?.Invoke(prev, Current);
        return true;
    }
}