using System.Threading.Tasks;
using UnityEngine;

public class MonsterSummoner : MonoBehaviour
{
    [SerializeField] private int _maxSummonNum = 8;

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
        monster.transform.localPosition = new Vector3(0, 1, -13f);
        _summonNum++;
    }

    private void OnMonsterDestroyed()
    {
        _summonNum--;
        _summonNum = Mathf.Max(_summonNum, 0);
    }
}