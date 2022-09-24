using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RageBulletScript : MonoBehaviour
{
    private PlayerScript player;
    public float Damage = 10;
    public float rotationSpeed = 200f;

    public GameObject Owner => gameObject;
    public string PoolTag { get; set; }

    private void Awake()
    {
        player = GetComponentInParent<PlayerScript>();
        gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            var enemyController = collision.gameObject.GetComponent<EnemyController>();
            enemyController.TakeDamage(Damage);
        }
    }
    private void Update()
    {
        //transform.position = (transform.position - player.transform.position);
        transform.RotateAround(player.transform.position, Vector3.forward, rotationSpeed * Time.deltaTime);
    }

}
