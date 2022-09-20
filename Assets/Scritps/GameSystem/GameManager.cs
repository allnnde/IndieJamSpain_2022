using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    public EnemyController enemyBasicController;
    public EnemySpawner[] enemiesSpawners;

    private float time = 0;
    private float timeToWave = 10;
    void Start()
    {
        ObjectPool.Instance.Pools = new List<Pool>();
        ObjectPool.Instance.Pools.Add(new Pool { Item = enemyBasicController, Parent = transform.transform, Size = 20, Tag = "basic" });
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
