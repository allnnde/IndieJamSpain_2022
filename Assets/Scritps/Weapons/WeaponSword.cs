using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Sword", menuName = "Weapon/Sword")]
public class WeaponSword : WeaponObject
{
    [SerializeField] private float range = 3f;
    private RaycastHit2D[] hits;
    


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(player.transform.position, range);
    }


    // Update is called once per frame
    public override void CreateBullets()
    {
        //var layer = LayerMask.GetMask("Enemy");
        hits = Physics2D.CircleCastAll(player.transform.position, range, Vector2.zero);
        foreach (var item in hits)
        {
            if (item.collider.gameObject.CompareTag("Enemy"))
            {
                var enemyController = item.collider.gameObject.GetComponent<EnemyController>();
                ObjectPool.Instance.Despawn(enemyController.PoolTag, enemyController.Owner);
                GameObject.Find("Player").GetComponent<PlayerScript>().AddRage(5f);
            }
        }

    }
}
