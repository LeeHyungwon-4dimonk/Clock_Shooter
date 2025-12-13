using System;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public TurnStack turnStack { get; private set; }

    private void Awake()
    {
        turnStack = new TurnStack();
        turnStack.OnChanged += HandleTurnChanged;
    }

    private void HandleTurnChanged(int prev, int current)
    {
        Debug.Log($"TurnStack: {prev} -> {current}");
    }
}