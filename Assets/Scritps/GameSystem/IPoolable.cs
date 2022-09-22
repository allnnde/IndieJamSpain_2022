using UnityEngine;

public interface IPoolable
{
    GameObject Owner { get; }
    string PoolTag { get; set; }

    void OnInstanciate(Transform parent);

    void OnSpawn(Vector3 position, Quaternion rotation);

    void OnDespawn();
}