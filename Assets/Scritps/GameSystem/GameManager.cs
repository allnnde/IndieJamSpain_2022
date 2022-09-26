using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    public EnemyController meleeEnemyController;
    public EnemyController rangedEnemyController;
    public PlayerBulletScript playerBullet;
    public EnemyBulletScript enemyBullet;
    public PickupScript healthPickup;

    public float minuteForWave = 3;

    private EnemySpawner[] enemiesSpawners;
    private float time = 0;
    private double timeToWave;
    private int level=0;
    public TMP_Text hordasText;

    void Start()
    {
        ObjectPool.Instance.Pools = new List<Pool>();
        ObjectPool.Instance.Pools.Add(new Pool { Item = meleeEnemyController, Parent = transform.transform, Size = 20, Tag = PoolTagsConstants.MELEE_ENEMY_POOL_TAG });
        ObjectPool.Instance.Pools.Add(new Pool { Item = rangedEnemyController, Parent = transform.transform, Size = 20, Tag = PoolTagsConstants.RANGED_ENEMY_POOL_TAG });
        ObjectPool.Instance.Pools.Add(new Pool { Item = playerBullet, Parent = transform.transform, Size = 50, Tag = PoolTagsConstants.BULLET_PLAYER_POOL_TAG });
        ObjectPool.Instance.Pools.Add(new Pool { Item = enemyBullet, Parent = transform.transform, Size = 50, Tag = PoolTagsConstants.BULLET_ENEMY_POOL_TAG });
        ObjectPool.Instance.Pools.Add(new Pool { Item = healthPickup, Parent = transform.transform, Size = 50, Tag = PoolTagsConstants.PICKUPS_TAG });
        ObjectPool.Instance.Start();

        hordasText = GameObject.Find("NroHordasTXT").GetComponent<TMP_Text>();
        hordasText.text = $"Rounde: {level +1}";
        enemiesSpawners = FindObjectsOfType<EnemySpawner>(false);
        timeToWave = TimeSpan.FromMinutes(minuteForWave).TotalSeconds;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > timeToWave)
        {
            level++;
            foreach (var item in enemiesSpawners)
            {
                item.CreateWave(level);
            }
            time = 0;
            hordasText.text = $"Rounde: {level + 1}";
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
