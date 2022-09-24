using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyController : MonoBehaviour, IPoolable
{
    public GameObject Owner => gameObject;
    public float MaxLife = 20;
    public float Damage = 3;
    public float AttackCooldown = 1;
    private float actualLife;    

    public string PoolTag { get; set; }

    public string tagPlayer = "Player";
    public float Speed = 2;
    protected GameObject player;

    public void OnInstanciate(Transform parent)
    {
        transform.parent = parent;
        gameObject.SetActive(false);
        actualLife = MaxLife;
    }

    public void OnSpawn(Vector3 position, Quaternion rotation)
    {
        transform.position = position;
        transform.rotation = rotation;
        gameObject.SetActive(true);
    }

    public void SetLevel(int level)
    {
        MaxLife *= 1f + (level / 100f);
        Damage *= 1f + (level / 100f);
        Speed *= 1f + (level / 100f);
        if (AttackCooldown > 0.3)
        {
            AttackCooldown *= 1f - (level / 100f);
        }
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
    public virtual void Update()
    {
        Move();
    }


    public void OnCollisionEnter2D(Collision2D collision)
    {
        var target = collision.gameObject;
        if (target.CompareTag("Player"))
        {
            DamageTarget(target.GetComponent<PlayerScript>());
        }
    }

    public abstract void DamageTarget(PlayerScript player);
    public abstract void Move();

    public void TakeDamage(float damage)
    {
        actualLife -= damage;
        if(actualLife <= 0)
        {
            ObjectPool.Instance.Despawn(PoolTag, Owner);
        }
    }
}
