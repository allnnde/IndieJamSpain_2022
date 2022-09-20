using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    public EnemyController enemyBasicController;
    void Start()
    {
        ObjectPool.Instance.Pools = new List<Pool>();
        ObjectPool.Instance.Pools.Add(new Pool { Item= enemyBasicController, Parent=transform.transform, Size=20, Tag="basic" });
        ObjectPool.Instance.Start();


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
