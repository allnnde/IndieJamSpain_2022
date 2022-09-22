using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSword : WeaponObject
{
    [SerializeField] private float range = 3f;
    private RaycastHit2D[] hits;

    public override void SetValues()
    {
        fireRate = 1f;
        bulletSpeed = 0f;
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }


    // Update is called once per frame
    public override void CreateBullets()
    {
        // FIXME: Ni puta idea de como hacer un boxcast julio
        //var layer = LayerMask.GetMask("Enemy");
        hits = Physics2D.CircleCastAll(transform.position, range, Vector2.zero);
        //hits = Physics2D.BoxCastAll(transform.position, range, playerMouse.DegreeTowardsMouse(), playerMouse.Vector2TowardsMouse(), range.x / 2);
        // new Vector3(transform.position.x + (range.x / 2), transform.position.y, transform.position.z), new Vector3(range.x, range.y , 0)
        foreach (var item in hits)
        {
            if (item.collider.gameObject.CompareTag("Enemy"))
            {
                var enemyController = item.collider.gameObject.GetComponent<EnemyController>();
                ObjectPool.Instance.Despawn(enemyController.PoolTag, enemyController.Owner);
            }
        }

    }
}
