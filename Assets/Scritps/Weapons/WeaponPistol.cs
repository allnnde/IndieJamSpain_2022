using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPistol : WeaponObject
{
    public override void SetValues()
    {
        fireRate = 0.5f;
        bulletSpeed = 50f;
    }

    public override void CreateBullets()
    {
        GameObject bullet = ObjectPool.Instance.Spawn(PoolTagsConstants.BULLET_PLAYER_POOL_TAG, transform.position, playerMouse.QuaternionTowardsMouse());
        // TODO: Implementar el daño
        Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();
        bulletRB.AddForce(bullet.transform.up * bulletSpeed, ForceMode2D.Impulse);
    }
}
