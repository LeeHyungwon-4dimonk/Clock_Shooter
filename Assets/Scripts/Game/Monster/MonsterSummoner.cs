using System.Threading.Tasks;
using UnityEngine;

public class MonsterSummoner : MonoBehaviour
{
    [SerializeField] private int _maxSummonNum = 8;
    [SerializeField] private int _directionCount = 8;
    [SerializeField] private float _spawnRadius = 13f;
    [SerializeField] private float _yOffset = 1f;

    private int _summonNum;
    public bool IsInitialized { get; private set; } = false;

    private async void Start()
    {
        await InitAsync();
    }

    public async Task InitAsync()
    {
        if (IsInitialized) return;

        var monsterPrefab = await AssetLoaderProvider.Loader.LoadAsync<GameObject>("Monster");

        Manager.Pool.CreatePool("Monster", monsterPrefab, 8, "Monsters", this.transform);

        IsInitialized = true;
    }

    private void OnEnable()
    {
        Manager.Game.turnStack.OnChanged += TrySummon;
        MonsterController.OnMonsterDestroyed += OnMonsterDestroyed;
    }

    private void OnDisable()
    {
        Manager.Game.turnStack.OnChanged -= TrySummon;
        MonsterController.OnMonsterDestroyed -= OnMonsterDestroyed;
    }

    private void TrySummon(int prev, int cur)
    {
        int turn = cur - prev;

        if (turn < 0) return;

        if (_summonNum >= _maxSummonNum) return;

        GameObject monster = Manager.Pool.Get("Monster");

        int spawnIndex = Random.Range(0, _directionCount);
        monster.transform.localPosition = GetSpawnPosition(spawnIndex);

        var controller = monster.GetComponent<MonsterController>();
        controller.Initialize(spawnIndex);

        _summonNum++;
    }

    private Vector3 GetSpawnPosition(int index)
    {
        float angle = index * (360 / _directionCount);
        float rad = angle * Mathf.Deg2Rad;
        return new Vector3(Mathf.Cos(rad) * _spawnRadius, _yOffset, Mathf.Sin(rad) * _spawnRadius);
    }

    private void OnMonsterDestroyed()
    {
        _summonNum--;
        _summonNum = Mathf.Max(_summonNum, 0);
    }
}