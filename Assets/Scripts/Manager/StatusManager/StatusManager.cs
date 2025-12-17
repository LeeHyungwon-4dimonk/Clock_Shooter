using System;
using System.Threading.Tasks;
using UnityEngine;

public class StatusManager : Singleton<StatusManager>
{
    public event Action OnMonsterDied;
    public PlayerStats Stats { get; private set; }

    private PlayerStatData _data;
    private bool _isInitialized;

    private async void Start()
    {
        await InitAsync();
        Stats = new PlayerStats(_data);
    }

    private async Task InitAsync()
    {
        if(_isInitialized) return;

        var data = await AssetLoaderProvider.Loader.LoadAsync<ScriptableObject>("PlayerStatData");

        _data = data as PlayerStatData;

        _isInitialized = true;
    }

    public void ApplyPlayerAttack(Monster monster)
    {
        int damage = Stats.AttackDamage;
        monster.Stats.TakeDamage(damage);

        if(monster.Stats.CurrentHP <= 0)
        {
            monster.Die();
            OnMonsterDied?.Invoke();
        }
    }

    public void ApplyMonsterAttack(Monster monster)
    {
        int damage = monster.Stats.CurrentHP;
        Stats.TakeDamage(damage);
        monster.Die();
        OnMonsterDied?.Invoke();
    }
}