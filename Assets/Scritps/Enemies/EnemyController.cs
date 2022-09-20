using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, IPoolable
{
    public GameObject Owner => gameObject;
    public string tagPlayer = "Player";
    private GameObject player;

    public void OnInstanciate(Transform parent)
    {
        transform.parent = parent;

        gameObject.SetActive(false);
    }

    public void OnSpawn(Vector3 position, Quaternion rotation)
    {
        transform.position = position;
        transform.rotation = rotation;
        gameObject.SetActive(true);
    }

    public void OnDespawn()
    {
        gameObject.SetActive(false);
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag(tagPlayer);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        var playerPosition = player.transform.position;
        var direction = playerPosition - transform.position;
        transform.position += direction.normalized * Time.deltaTime;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player golpeado!!!!");
            ObjectPool.Instance.Despawn("basic", Owner);
        }
    }

}
