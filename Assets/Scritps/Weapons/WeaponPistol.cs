using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Pistol", menuName = "Weapon/Pistol")]
public class WeaponPistol : WeaponObject
{

    public override void CreateBullets()
    {
        GameObject bullet = ObjectPool.Instance.Spawn(PoolTagsConstants.BULLET_PLAYER_POOL_TAG, player.transform.position, playerMouse.QuaternionTowardsMouse());
        // TODO: Implementar el daño
        Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();
        bulletRB.AddForce(bullet.transform.up * bulletSpeed, ForceMode2D.Impulse);
    }
}
