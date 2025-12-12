using System;
using System.Threading.Tasks;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public int TurnStack { get; private set; } = 0;
    public event Action<int> OnTurnStackChanged;

    public void AddTurn()
    {
        TurnStack++;
        OnTurnStackChanged?.Invoke(1);
        Debug.Log(TurnStack);
    }

    public void SubTurn()
    {
        TurnStack--;
        OnTurnStackChanged?.Invoke(-1);
        Debug.Log(TurnStack);
    }
}