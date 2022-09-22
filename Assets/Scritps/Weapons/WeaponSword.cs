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
        //var layer = LayerMask.GetMask("Enemy");
        hits = Physics2D.CircleCastAll(transform.position, range, Vector2.zero);
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
