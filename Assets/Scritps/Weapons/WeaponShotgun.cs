using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShotgun : WeaponObject
{
    public float spread = 20f;
    public int bullets = 4;
    public override void PreparateAttackMode()
    {
        var bullet = ObjectPool.Instance.Spawn(PoolTagsConstants.BULLET_PLAYER_POOL_TAG, transform.position, playerMouse.QuaternionTowardsMouse());
        bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.up * bulletSpeed, ForceMode2D.Impulse);

        for (int i = 0; i <= bullets; i++)
        {
            // TODO: Implementar el daño
            bullet = ObjectPool.Instance.Spawn(PoolTagsConstants.BULLET_PLAYER_POOL_TAG, transform.position, playerMouse.QuaternionTowardsMouse());
            bullet.transform.Rotate(0, 0, Random.Range(-spread, spread));
            bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.up * bulletSpeed, ForceMode2D.Impulse);
        }
    }


}
