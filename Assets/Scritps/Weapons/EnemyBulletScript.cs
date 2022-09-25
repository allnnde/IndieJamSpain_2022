using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletScript : MonoBehaviour, IPoolable
{
    public float speed = 15;
    public GameObject Owner => gameObject;
    public EnemyController origen;
    public PlayerScript player;
    public string PoolTag { get; set; }
    private Vector3 shootPoint;
    private Vector3 originPoint;

    private void Update()
    {
        var direction = shootPoint - originPoint;
        transform.position += direction.normalized * Time.deltaTime * speed;        
        var angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) - 90;
        transform.rotation = Quaternion.Euler(0,0,angle);

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Aqui ponemos pa dañar al enemigo/jugador
        if (collision.gameObject.CompareTag("Player"))
        {
            var player = collision.gameObject.GetComponent<PlayerScript>();
            origen.DamageTarget(player);
            ObjectPool.Instance.Despawn(PoolTag, Owner);
        }

    }

    void OnBecameInvisible()
    {
        ObjectPool.Instance.Despawn(PoolTag, Owner);
    }

    public void OnInstanciate(Transform parent)
    {
        transform.parent = parent;
        gameObject.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();

    }

    public void OnSpawn(Vector3 position, Quaternion rotation)
    {
        gameObject.SetActive(true);
        transform.position = position;
        transform.rotation = rotation;
        shootPoint = player.transform.position;
        originPoint = transform.position;
    }

    public void OnDespawn()
    {
        gameObject?.SetActive(false);
    }
}
