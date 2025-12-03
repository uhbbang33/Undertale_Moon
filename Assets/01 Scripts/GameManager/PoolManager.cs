using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [System.Serializable]
    public struct Pool
    {
        public string PoolName;
        public int PoolSize;
        public GameObject Prefab;
        public Transform ParentTransform;
    }

    [SerializeField] private List<Pool> _pools;

    private Dictionary<string, Queue<GameObject>> _poolDictionary;

    private void Awake()
    {
        _poolDictionary = new();

        for (int i = 0; i < _pools.Count; ++i)
        {
            CreatePool(_pools[i]);
        }
    }

    private void CreatePool(Pool pool)
    {
        string poolKey = pool.PoolName;

        GameObject parentObject = new GameObject(pool.PoolName + "Pool");
        parentObject.transform.SetParent(transform);
        pool.ParentTransform = parentObject.transform;

        if (!_poolDictionary.ContainsKey(poolKey))
        {
            _poolDictionary.Add(poolKey, new Queue<GameObject>());

            for (int i = 0; i < pool.PoolSize; ++i)
            {
                GameObject obj = Instantiate(pool.Prefab, parentObject.transform);

                obj.SetActive(false);

                _poolDictionary[poolKey].Enqueue(obj);
            }
        }
        else
        {
            Debug.LogError(poolKey + " Duplicate Pool");
        }
    }

    public GameObject GetObject(string objName)
    {
        if (!_poolDictionary.ContainsKey(objName))
            return null;

        GameObject obj;

        if (_poolDictionary[objName].Count == 0)
        {
            Pool pool = _pools.Find(p => p.PoolName == objName);
            obj = Instantiate(pool.Prefab, pool.ParentTransform);
        }
        else
        {
            obj = _poolDictionary[objName].Dequeue();
        }
        
        obj.SetActive(true);

        return obj;
    }

    public void ReturnObject(GameObject obj)
    {
        if (_poolDictionary.ContainsKey(obj.name))
        {
            _poolDictionary[obj.name].Enqueue(obj);
            obj.SetActive(false);
        }
        else
        {
            Debug.LogError(obj.name + " key does not exist");
        }
    }

}
