using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    public EnemyController meleeEnemyController;
    public EnemyController rangedEnemyController;
    public PlayerBulletScript playerBullet;
    public EnemyBulletScript enemyBullet;
    public EnemySpawner[] enemiesSpawners;

    private float time = 0;
    private float timeToWave = 10;
    void Start()
    {
        ObjectPool.Instance.Pools = new List<Pool>();
        ObjectPool.Instance.Pools.Add(new Pool { Item = meleeEnemyController, Parent = transform.transform, Size = 20, Tag = PoolTagsConstants.MELEE_ENEMY_POOL_TAG });
        ObjectPool.Instance.Pools.Add(new Pool { Item = rangedEnemyController, Parent = transform.transform, Size = 20, Tag = PoolTagsConstants.RANGED_ENEMY_POOL_TAG });
        ObjectPool.Instance.Pools.Add(new Pool { Item = playerBullet, Parent = transform.transform, Size = 50, Tag = PoolTagsConstants.BULLET_PLAYER_POOL_TAG });
        ObjectPool.Instance.Pools.Add(new Pool { Item = enemyBullet, Parent = transform.transform, Size = 50, Tag = PoolTagsConstants.BULLET_ENEMY_POOL_TAG });
        ObjectPool.Instance.Start();

        enemiesSpawners = FindObjectsOfType<EnemySpawner>(false);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > timeToWave)
        {
            foreach (var item in enemiesSpawners)
            {
                item.CreateWave();
            }
            time = 0;
        }
    }
}
