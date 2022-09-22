using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletScript : MonoBehaviour, IPoolable
{
    public float bulletTime = -1f;
    public GameObject Owner => gameObject;

  
    void OnTriggerEnter2D(Collider2D collision)
    {
        // Aqui ponemos pa dañar al enemigo/jugador
        if (collision.gameObject.CompareTag("Enemy"))
        {
            var enemyController = collision.gameObject.GetComponent<EnemyController>();
            switch (enemyController)
            {
                case MeleeEnemyController melee:
                    ObjectPool.Instance.Despawn(PoolTagsConstants.MELEE_ENEMY_POOL_TAG, melee.gameObject);
                    break;
                case RangedEnemyController ranged:
                    ObjectPool.Instance.Despawn(PoolTagsConstants.RANGED_ENEMY_POOL_TAG, ranged.gameObject);
                    break;
                default:
                    break;
            }
        }

        ObjectPool.Instance.Despawn(PoolTagsConstants.BULLET_PLAYER_POOL_TAG, Owner);

        if (bulletTime >= 0)
        {
            // Despawnear despues de "bulletTime" tiempo
        }
    }

    void OnBecameInvisible()
    {
        ObjectPool.Instance.Despawn(PoolTagsConstants.BULLET_PLAYER_POOL_TAG, Owner);
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
