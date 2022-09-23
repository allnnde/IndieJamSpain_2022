using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletScript : MonoBehaviour, IPoolable
{
    private PlayerScript player;

    public GameObject Owner => gameObject;
    public string PoolTag { get; set; }
    void OnTriggerEnter2D(Collider2D collision)
    {
        // Aqui ponemos pa dañar al enemigo/jugador
        if (collision.gameObject.CompareTag("Enemy"))
        {
            var enemyController = collision.gameObject.GetComponent<EnemyController>();
            ObjectPool.Instance.Despawn(enemyController.PoolTag, enemyController.Owner);
            player.AddRage(20f);

        }

        ObjectPool.Instance.Despawn(PoolTag, Owner);
      
    }

    void OnBecameInvisible()
    {
        ObjectPool.Instance.Despawn(PoolTag, Owner);
    }

    public void OnInstanciate(Transform parent)
    {
        transform.parent = parent;
        gameObject.SetActive(false);
        player = GetComponent<PlayerScript>();
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
