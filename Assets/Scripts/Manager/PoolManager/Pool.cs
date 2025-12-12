using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pool
{
    public string _key;
    public GameObject _prefab;
    public int _size = 10;

    private Queue<GameObject> _objects = new Queue<GameObject>();
    private Transform _parent;

    public void Init(Transform parent)
    {
        parent = _parent;

        for(int i = 0; i < _size; i++)
        {
            GameObject obj = GameObject.Instantiate(_prefab, _parent);
            obj.SetActive(false);
            _objects.Enqueue(obj);
        }
    }

    public GameObject Get()
    {
        if(_objects.Count > 0)
        {
            GameObject obj = _objects.Dequeue();
            obj.SetActive(true);
            return obj;
        }

        GameObject newObj = GameObject.Instantiate(_prefab, _parent);
        return newObj;
    }

    public void Return(GameObject obj)
    {
        obj.SetActive(false);
        _objects.Enqueue(obj);
    }
}
