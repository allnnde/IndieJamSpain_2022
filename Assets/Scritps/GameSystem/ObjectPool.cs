using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool
{
    [SerializeField]
    public List<Pool> Pools;

    private Dictionary<string, List<GameObject>> _activedObject;
    private Dictionary<string, List<GameObject>> _inactivedObject;

    private static ObjectPool _instance;

    public static ObjectPool Instance
    {
        get
        {
            if (_instance == null)
                _instance = new ObjectPool();
            return _instance;
        }
    }

    private ObjectPool()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    public void Start()
    {
        _activedObject = new Dictionary<string, List<GameObject>>();
        _inactivedObject = new Dictionary<string, List<GameObject>>();

        foreach (var _pool in Pools)
        {
            _activedObject[_pool.Tag] = new List<GameObject>();
            _inactivedObject[_pool.Tag] = new List<GameObject>();
            for (int i = 0; i < _pool.Size; i++)
            {
                var obj = InstanciatePoolObject(_pool);
                _inactivedObject[_pool.Tag].Add(obj);
            }
        }
    }

    private GameObject InstanciatePoolObject(Pool pool)
    {
        var obj = GameObject.Instantiate(pool.Item.Owner, Vector3.zero, Quaternion.identity);
        var totalObject = _inactivedObject[pool.Tag].Count + _activedObject[pool.Tag].Count;
        obj.name += $" - {pool.Tag}_{totalObject.ToString().PadLeft(5, '0')}";
        var poolable = obj.GetComponent<IPoolable>();
        poolable.PoolTag = pool.Tag;
        poolable.OnInstanciate(pool.Parent);
        return obj;
    }

    public GameObject Spawn(string tag, Vector3 position, Quaternion rotation)
    {
        var obj = _inactivedObject[tag].FirstOrDefault();
        IPoolable poolable;
        if (obj == null)
        {
            var pool = Pools.FirstOrDefault(p => p.Tag.Equals(tag));
            obj = InstanciatePoolObject(pool);
        }
        else
        {
            _inactivedObject[tag].Remove(obj);
        }

        poolable = obj.GetComponent<IPoolable>();
        poolable.OnSpawn(position, rotation);

        _activedObject[tag].Add(obj);
        return obj;
    }

    public void Despawn(string tag, GameObject gameObject)
    {
        var obj = _activedObject[tag].Find(p => p.name == gameObject.name);
        _activedObject[tag].Remove(obj);
        if (obj == null)
        {
            return;
        }

        var pool = obj.GetComponent<IPoolable>();
        pool.OnDespawn();

        _inactivedObject[tag].Add(obj);
    }
}

[Serializable]
public class Pool
{
    public IPoolable Item;
    public int Size;
    public Transform Parent;
    public string Tag;
}