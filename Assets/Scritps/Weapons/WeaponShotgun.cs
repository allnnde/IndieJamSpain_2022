using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShotgun : WeaponObject
{
    public float spread = 5f;
    public int bullets = 4;
    public override void PreparateAttackMode()
    {
        GameObject[] bulletsList = null;

        for (int i = 0; i <= bullets; i++)
        {
            // TODO: Implementar el da�o
            bulletsList[i] = ObjectPool.Instance.Spawn(PoolTagsConstants.BULLET_PLAYER_POOL_TAG, transform.position, playerMouse.QuaternionTowardsMouse());
            bulletsList[i].transform.Rotate(Random.Range(-spread, spread), Random.Range(-spread, spread), 0);
            bulletsList[i].GetComponent<Rigidbody2D>().AddForce(bulletsList[i].transform.up * bulletSpeed, ForceMode2D.Impulse);
        }
    }
}
