using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyController : MonoBehaviour, IPoolable
{
    public GameObject Owner => gameObject;
    public string tagPlayer = "Player";
    public float speed = 2;
    protected GameObject player;

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


    private void OnCollisionEnter2D(Collision2D collision)
    {
        var target = collision.gameObject;
        if (target.CompareTag("Player"))
        {
            DamageTarget(target.GetComponent<PlayerScript>());
            ObjectPool.Instance.Despawn("basic", Owner);
        }
    }

    public abstract void DamageTarget(PlayerScript player);
    public abstract void Move();
}
