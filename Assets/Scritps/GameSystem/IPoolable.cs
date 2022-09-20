using UnityEngine;

public interface IPoolable
{
    GameObject Owner { get; }

    void OnInstanciate(Transform parent);

    void OnSpawn(Vector3 position, Quaternion rotation);

    void OnDespawn();
}