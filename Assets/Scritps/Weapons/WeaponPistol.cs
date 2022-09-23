using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPistol : WeaponObject
{
    public override void PreparateAttackMode()
    {

        GameObject bullet = ObjectPool.Instance.Spawn(PoolTagsConstants.BULLET_PLAYER_POOL_TAG, transform.position, playerMouse.QuaternionTowardsMouse());
        bullet.GetComponent<PlayerBulletScript>().Damage = damage;
        Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();
        bulletRB.AddForce(bullet.transform.up * bulletSpeed, ForceMode2D.Impulse);
    }
}
