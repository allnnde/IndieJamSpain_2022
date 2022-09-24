using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSword : WeaponObject
{
    [SerializeField] private float range = 3f;
    private RaycastHit2D[] hits;

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }

    // Update is called once per frame
    public override void PreparateAttackMode()
    {
        //var layer = LayerMask.GetMask("Enemy");
        hits = Physics2D.CircleCastAll(transform.position, range, Vector2.zero);
        foreach (var item in hits)
        {
            if (item.collider.gameObject.CompareTag("Enemy"))
            {
                var enemyController = item.collider.gameObject.GetComponent<EnemyController>();
                enemyController.TakeDamage(damage);
                playerScript.AddRage(5f);
            }
        }

    }
}
