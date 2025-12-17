using UnityEngine;

public class Monster : MonoBehaviour
{
    public MonsterStats Stats { get; private set; }
    
    [SerializeField] private MonsterStatData statData;
    
    private MonsterController _controller;
    private bool _subscribed;

    private void Awake()
    {
        _controller = GetComponent<MonsterController>();
    }

    public void Initialize()
    {
        if(!_subscribed)
        {
            Stats = new MonsterStats(statData);
            _controller.OnHit += HandleHit;
            _subscribed = true;
        }
    }

    public void ResetState()
    {
        if(_subscribed)
        {
            _controller.OnHit -= HandleHit;
            _subscribed = false;
        }
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
        ResetState();
        Manager.Game.monsterPositionManager.Unregister(_controller);
        Manager.Pool.Return("Monster", gameObject);
    }
}