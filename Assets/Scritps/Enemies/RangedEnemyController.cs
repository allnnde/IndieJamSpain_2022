using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyController : EnemyController
{
    public float MinimusDistance = 10;
    public float AttackRadius= 12;
    public float shootCooldown = 5.5f;
    private float time;
    public override void DamageTarget(PlayerScript player)
    {
        player.TakeDamage(2f);
    }

    public override void Update()
    {
        base.Update();
        time += Time.deltaTime;
        if(time > shootCooldown)
        {
            Shoot();
            time = 0;
        }

    }

    private void Shoot() {
        Debug.Log("disparooooo");
        var bulletObj = ObjectPool.Instance.Spawn(PoolTagsConstants.BULLET_ENEMY_POOL_TAG,transform.position, Quaternion.identity);
        var bullet = bulletObj.GetComponent<EnemyBulletScript>();
        bullet.origen = this;
    }
    public override void Move()
    {
        var distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance > MinimusDistance)
        {
            var playerPosition = player.transform.position;
            var direction = playerPosition - transform.position;
            transform.position += direction.normalized * Time.deltaTime * speed;
        }
        else
        {
            var playerPosition = player.transform.position;
            var direction = playerPosition - transform.position;
            transform.position -= direction.normalized * Time.deltaTime * speed;
        }
 
    }

}
