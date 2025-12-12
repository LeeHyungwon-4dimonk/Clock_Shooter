using System.Collections.Generic;
using UnityEngine;

public class PoolManager : Singleton<PoolManager>
{
    [SerializeField] private List<Pool> _pools = new List<Pool>();
    private Dictionary<string, Pool> _poolsDict = new Dictionary<string, Pool>();

    private void Awake()
    {
        foreach (var pool in _pools)
        {
            pool.Init(this.transform);
            _poolsDict.Add(pool._key, pool);
        }
    }

    public void CreatePool(string key, GameObject prefab, int size)
    {
        if(_poolsDict.ContainsKey(key))
        {
            Debug.LogWarning($"Pool with key '{key}' already exists.");
            return;
        }

        Pool newPool = new Pool()
        {
            _key = key,
            _prefab = prefab,
            _size = size
        };

        newPool.Init(this.transform);
        _poolsDict.Add(key, newPool);
    }

    public GameObject Get(string key)
    {
        if(_poolsDict.ContainsKey(key)) return _poolsDict[key].Get();

        Debug.LogWarning($"Pool with key '{key}' not found!");
        return null;
    }

    public void Return(string key, GameObject obj)
    {
        if(_poolsDict.ContainsKey(key)) _poolsDict[key].Return(obj);
    }
}
