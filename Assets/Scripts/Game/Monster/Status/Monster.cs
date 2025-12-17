using System;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField] private MonsterStatData statData;
    private MonsterController _controller;

    public MonsterStats Stats { get; private set; }

    private void Awake()
    {
        _controller = GetComponent<MonsterController>();
        Stats = new MonsterStats(statData);
    }

    private void OnEnable()
    {
        _controller.OnHit += HandleHit;
    }

    private void OnDisable()
    {
        _controller.OnHit -= HandleHit;
    }

    private void HandleHit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Manager.Status.ApplyPlayerAttack(this);
        }
        else if(collision.gameObject.CompareTag("Player"))
        {
            Manager.Status.ApplyMonsterAttack(this);
        }
    }

    public void Die()
    {
        Manager.Game.monsterPositionManager.Unregister(_controller);
        Manager.Pool.Return("Monster", gameObject);
    }
}