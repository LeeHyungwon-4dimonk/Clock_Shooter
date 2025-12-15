using System.Threading.Tasks;
using UnityEngine;

public class MonsterSummoner : MonoBehaviour
{
    [SerializeField] private int _maxSummonNum = 8;
    [SerializeField] private int _directionCount = 8;

    private int _summonNum;

    private async void Start()
    {
        await InitAsync();
    }

    private async Task InitAsync()
    {
        var monsterPrefab =
            await AssetLoaderProvider.Loader.LoadAsync<GameObject>("Monster");

        Manager.Pool.CreatePool("Monster", monsterPrefab, _maxSummonNum, "Monsters", transform);
    }

    private void OnEnable()
    {
        Manager.Game.turnStack.OnChanged += OnTurnChanged;
        MonsterController.OnMonsterDestroyed += OnMonsterDestroyed;
    }

    private void OnDisable()
    {
        Manager.Game.turnStack.OnChanged -= OnTurnChanged;
        MonsterController.OnMonsterDestroyed -= OnMonsterDestroyed;
    }

    private void OnTurnChanged(int prev, int cur)
    {
        int delta = cur - prev;

        // 이동은 중앙에서 단 한 번
        Manager.Game.monsterPositionManager.ResolveTurnMove(delta);

        // 전진일 때만 소환
        if (delta <= 0) return;
        if (_summonNum >= _maxSummonNum) return;

        GameObject monster = Manager.Pool.Get("Monster");
        int dir = Random.Range(0, _directionCount);

        monster.GetComponent<MonsterController>().Initialize(dir);

        _summonNum++;
    }

    private void OnMonsterDestroyed()
    {
        _summonNum = Mathf.Max(0, _summonNum - 1);
    }
}