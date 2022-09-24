using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupScript : MonoBehaviour, IPoolable
{
    public GameObject Owner => gameObject;
    public string PoolTag { get; set; }

    void OnBecameInvisible()
    {
        ObjectPool.Instance.Despawn(PoolTag, Owner);
    }

    public void OnInstanciate(Transform parent)
    {
        transform.parent = parent;
        gameObject.SetActive(false);
    }

    public void OnSpawn(Vector3 position, Quaternion rotation)
    {
        gameObject.SetActive(true);
        transform.position = position;
        transform.rotation = rotation;
    }

    public void OnDespawn()
    {
        gameObject?.SetActive(false);
    }
}
