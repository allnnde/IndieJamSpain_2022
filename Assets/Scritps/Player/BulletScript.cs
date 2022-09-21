using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour, IPoolable
{
    public float bulletTime = -1f;
    public GameObject Owner => gameObject;    
  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Aqui ponemos pa dañar al enemigo/jugador
        // FIXME: Cuando se destruye, ya no se mueren al tocar el jugador (Seguramente por ser un simple Destroy)
        if (collision.gameObject.CompareTag("Enemy"))
        {
            ObjectPool.Instance.Despawn("basic", collision.gameObject);
        }

        ObjectPool.Instance.Despawn("bullet", Owner);
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
