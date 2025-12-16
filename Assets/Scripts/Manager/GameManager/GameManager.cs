using System;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public const int Direction = 8;
    public TurnStack turnStack { get; private set; }
    public MonsterPositionManager monsterPositionManager { get; private set; }

    private void Awake()
    {
        turnStack = new TurnStack();
        monsterPositionManager = new MonsterPositionManager();
    }
}