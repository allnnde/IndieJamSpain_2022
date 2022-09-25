using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyController : EnemyController
{
    public float MinimusDistance = 10;
    public float AttackRadius= 12;
    private float time;
    public override void DamageTarget(PlayerScript player)
    {
        player.TakeDamage(Damage);
    }

    public override void Update()
    {
        base.Update();
        time += Time.deltaTime;
        if(time > AttackCooldown)
        {
            Shoot();
            time = 0;
        }

    }

    private void Shoot() {
        var bulletObj = ObjectPool.Instance.Spawn(PoolTagsConstants.BULLET_ENEMY_POOL_TAG,transform.position, Quaternion.identity);
        var bullet = bulletObj.GetComponent<EnemyBulletScript>();
        bullet.origen = this;
    }
    public override void Move()
    {
        var distance = Vector3.Distance(transform.position, player.transform.position);

        var playerPosition = player.transform.position;
        var direction = playerPosition - transform.position;

        if (distance > MinimusDistance)        
            transform.position += direction.normalized * Time.deltaTime * Speed;        
        else        
            transform.position -= direction.normalized * Time.deltaTime * Speed;

        AnimateMove(direction.normalized);
    }

}
