using System.Collections.Generic;
using UnityEngine;

public class PoolManager : SingletonMonoBehaviour<PoolManager>
{
    [System.Serializable]
    public struct Pool
    {
        public string PoolName;
        public int PoolSize;
        public GameObject Prefab;
        public Transform ParentTransform { get; set; }
    }

    [SerializeField] private List<Pool> _pools;

    private Dictionary<string, Queue<GameObject>> _poolDictionary;

    protected override void Awake()
    {
        base.Awake();

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

                PoolObject poolObject = obj.AddComponent<PoolObject>();
                poolObject.PoolKey = poolKey;

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

            PoolObject poolObject = obj.AddComponent<PoolObject>();
            poolObject.PoolKey = pool.PoolName;

            // TODO: 새로 생성한 obj 부모 transform 밑으로 들어가지 않음
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
        string poolKey = obj.GetComponent<PoolObject>().PoolKey;
        
        if (_poolDictionary.ContainsKey(poolKey))
        {
            _poolDictionary[poolKey].Enqueue(obj);
            obj.SetActive(false);
        }
        else
        {
            Debug.LogError(poolKey + " key does not exist");
        }
    }

}
