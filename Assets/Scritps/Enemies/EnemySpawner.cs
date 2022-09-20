using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public int counter;
    public string tagpoll;
    public Transform Route;

    private async void Start()
    {
        for (int i = 0; i < counter; i++)
        {
            ObjectPool.Instance.Spawn(tagpoll, transform.position, transform.rotation);
            await Task.Delay(1000);
        }
    }

}
