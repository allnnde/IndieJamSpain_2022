using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public int counter;
    public string tagpool;

    private async void Start()
    {
        await CreateWave(0);
    }

    public async Task CreateWave(int level)
    {
        for (int i = 0; i < counter + level; i++)
        {
            var randomPosition = Random.insideUnitCircle * 5;
            Vector2 finalPosition = transform.position + new Vector3(randomPosition.x, randomPosition.y, 0);
            var enemy = ObjectPool.Instance.Spawn(tagpool, finalPosition, transform.rotation);
            enemy.GetComponent<EnemyController>().SetLevel(level);


            await Task.Delay(100);
        }
    }
}
