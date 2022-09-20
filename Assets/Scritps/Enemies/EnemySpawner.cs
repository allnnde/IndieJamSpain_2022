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
        await CreateWave();
    }

    public async Task CreateWave()
    {
        for (int i = 0; i < counter; i++)
        {
            var randomPosition = Random.insideUnitCircle * 5;
            Vector2 finalPosition = transform.position + new Vector3(randomPosition.x, randomPosition.y, 0);
            ObjectPool.Instance.Spawn(tagpool, finalPosition, transform.rotation);
            await Task.Delay(100);
        }
    }
}
