using System;

public class TurnStack
{
    public int Current { get; private set; }

    public event Action<int, int> OnChanged;

    public TurnStack(int initialValue = 0)
    {
        Current = initialValue;
    }

    public void MoveClockwise(int amount = 1)
    {
        if (amount <= 0) return;

        int prev = Current;
        Current += amount;
        OnChanged?.Invoke(prev, Current);
    }

    public bool TryMoveCounterClockwise(int amount = 1)
    {
        if(Current - amount < 0) return false;

        int prev = Current;
        Current -= amount;
        OnChanged?.Invoke(prev, Current);
        return true;
    }

    public void OtherAction()
    {
        int prev = Current;
        OnChanged?.Invoke(prev, prev + 1);
    }
}