using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyController : EnemyController
{
    public float time = 0;
    public override void DamageTarget(PlayerScript player)
    {
        player.TakeDamage(Damage);
    }
    public override void Move()
    {
        var playerPosition = player.transform.position;
        var direction = playerPosition - transform.position;
        transform.position += direction.normalized * Time.deltaTime * Speed;
    }
    public void OnCollisionStay2D(Collision2D collision)
    {
        var target = collision.gameObject;
        if (target.CompareTag("Player"))
        {
            time += Time.deltaTime;
            if (time > AttackCooldown)
            {
                DamageTarget(target.GetComponent<PlayerScript>());
                time = 0;
            }
        }
    }

}
