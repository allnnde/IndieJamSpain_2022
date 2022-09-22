using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyController : EnemyController
{
    public override void DamageTarget(PlayerScript player)
    {
        player.TakeDamage(2f);
    }
    public override void Move()
    {
        var playerPosition = player.transform.position;
        var direction = playerPosition - transform.position;
        transform.position += direction.normalized * Time.deltaTime * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var player = collision.gameObject.GetComponent<PlayerScript>();
            DamageTarget(player);
            ObjectPool.Instance.Despawn(PoolTagsConstants.MELEE_ENEMY_POOL_TAG, Owner);
        }
    }

}
