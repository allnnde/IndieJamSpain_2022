using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RageBulletScript : MonoBehaviour, IPoolable
{
    private PlayerScript player;
    public float Damage { get; set; }
    public float rotationSpeed = 200f;

    public GameObject Owner => gameObject;
    public string PoolTag { get; set; }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            var enemyController = collision.gameObject.GetComponent<EnemyController>();
            enemyController.TakeDamage(Damage);
        }
    }

    public void OnInstanciate(Transform parent)
    {
        transform.parent = GameObject.FindGameObjectWithTag("Player").transform;
        gameObject.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
    }

    public void OnSpawn(Vector3 position, Quaternion rotation)
    {
        gameObject.SetActive(true);
        transform.position = position;
    }

    public void OnDespawn()
    {
        gameObject?.SetActive(false);
    }

    private void Update()
    {
        //transform.RotateAround(player.gameObject.transform.position, new Vector3(0, 0, 1), rotationSpeed * Time.deltaTime);
    }

}
