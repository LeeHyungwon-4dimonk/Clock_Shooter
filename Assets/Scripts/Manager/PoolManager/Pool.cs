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
        _parent = parent;
        CreateObjects();
    }

    public void Init(Transform root, string childName)
    {
        // root 아래 childName 자식 오브젝트 생성
        GameObject child = new GameObject(childName);
        child.transform.SetParent(root);
        child.transform.localPosition = Vector3.zero;

        _parent = child.transform;
        CreateObjects();
    }

    private void CreateObjects()
    {
        for (int i = 0; i < _size; i++)
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