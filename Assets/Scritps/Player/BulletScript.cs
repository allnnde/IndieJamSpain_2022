using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour, IPoolable
{
    public float bulletTime = -1f;
    public GameObject Owner => gameObject;

    
    void OnCollisionEnter2D(Collision2D other)
    {
        // Aqui ponemos pa dañar al enemigo/jugador
        if (other.gameObject.CompareTag("Enemy"))
        {
            ObjectPool.Instance.Despawn("basic", other.gameObject);
        }

        ObjectPool.Instance.Despawn("bullet", Owner);

        if (bulletTime >= 0)
        {
            // Despawnear despues de "bulletTime" tiempo
        }
    }

    void OnBecameInvisible()
    {
        ObjectPool.Instance.Despawn("bullet", Owner);
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
