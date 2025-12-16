using System.Threading.Tasks;
using UnityEngine;

public class MonsterSummoner : MonoBehaviour
{
    [SerializeField] private int _maxSummonNum = 8;
    [SerializeField] private int _directionCount = 8;
    [SerializeField] private int _summonTurnOffset = 2;

    private int _summonTurn = 2;
    private int _summonNum;

    private async void Start()
    {
        await InitAsync();
    }

    private async Task InitAsync()
    {
        var monsterPrefab = await AssetLoaderProvider.Loader.LoadAsync<GameObject>("Monster");

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
        _summonTurn++;

        Manager.Game.monsterPositionManager.ResolveTurnMove(delta);

        if (delta <= 0) return;
        if (_summonNum >= _maxSummonNum) return;
        if(_summonTurn < _summonTurnOffset) return;

        GameObject monster = Manager.Pool.Get("Monster");
        int dir = Random.Range(0, _directionCount);

        monster.GetComponent<MonsterController>().Initialize(dir);

        _summonNum++;
        if(_summonTurn >= _summonTurnOffset) _summonTurn = 0;
    }

    private void OnMonsterDestroyed()
    {
        _summonNum = Mathf.Max(0, _summonNum - 1);
    }
}