using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float bulletTime = -1f;

    void Start()
    {
        if (bulletTime != -1f)
        {
            Destroy(gameObject, bulletTime);
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        // Aqui ponemos pa dañar al enemigo/jugador
        // FIXME: Cuando se destruye, ya no se mueren al tocar el jugador (Seguramente por ser un simple Destroy)
        Destroy(other.gameObject);

        Destroy(gameObject);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
