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

}
