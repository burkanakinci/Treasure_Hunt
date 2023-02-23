using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : CustomBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string PrefabTag;
        public GameObject PooledPrefab;
        public int SpawnedSize = 1;
        public Transform PrefabParent;
    }
    public List<Pool> pools = new List<Pool>();
    public Dictionary<string, Queue<IPooledObject>> poolDictionary;

    private IPooledObject m_SpawnOnPool;
    private IPooledObject m_TempSpawned;

    public override void Initialize()
    {
        poolDictionary = new Dictionary<string, Queue<IPooledObject>>();

        for (int i = 0; i < pools.Count; i++)
        {
            Queue<IPooledObject> activeOnPool = new Queue<IPooledObject>();
            Queue<IPooledObject> objectsOnPool = new Queue<IPooledObject>();

            for (int j = 0; j < pools[i].SpawnedSize; j++)
            {
                m_TempSpawned = Instantiate(pools[i].PooledPrefab, pools[i].PrefabParent).GetComponent<IPooledObject>();
                m_TempSpawned.GetGameObject().gameObject.SetActive(false);
                m_TempSpawned.GetGameObject().Initialize();
                m_TempSpawned.SetPooledTag(pools[i].PrefabTag);
                objectsOnPool.Enqueue(m_TempSpawned);
            }
            poolDictionary.Add(pools[i].PrefabTag, objectsOnPool);

        }
    }
    public IPooledObject SpawnFromPool(string _prefabTag,
                                    Vector3 _position = new Vector3(),
                                    Quaternion _rotation = new Quaternion(),
                                    Transform _parent = null)
    {
        if (!poolDictionary.ContainsKey(_prefabTag))
        {
            return null;
        }

        if (poolDictionary[_prefabTag].Count > 0)
        {
            m_SpawnOnPool = poolDictionary[_prefabTag].Dequeue();
        }
        else
        {
            for (int i = pools.Count - 1; i >= 0; i--)
            {
                if (pools[i].PrefabTag == _prefabTag)
                {
                    m_SpawnOnPool = Instantiate(pools[i].PooledPrefab, pools[i].PrefabParent).GetComponent<IPooledObject>();
                    m_SpawnOnPool.GetGameObject().Initialize();
                    m_SpawnOnPool.SetPooledTag(pools[i].PrefabTag);
                    break;
                }
            }
        }



        if (_position != null)
        {
            m_SpawnOnPool.GetGameObject().transform.position = _position;
        }
        if (_rotation != null)
        {
            m_SpawnOnPool.GetGameObject().transform.rotation = _rotation;
        }

        m_SpawnOnPool.GetGameObject().transform.SetParent(_parent);

        m_SpawnOnPool.GetGameObject().gameObject.SetActive(true);
        m_SpawnOnPool.OnObjectSpawn();

        return m_SpawnOnPool;
    }

    public void AddObjectPool(string _prefabTag, IPooledObject _pooledObject)
    {
        if (!poolDictionary[_prefabTag].Contains(_pooledObject))
            poolDictionary[_prefabTag].Enqueue(_pooledObject);
    }
}