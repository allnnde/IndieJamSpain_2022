using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class PickupDetectionScript : MonoBehaviour
{
    public float addingHealth = 30f;
    private PlayerScript playerScript;

    // Start is called before the first frame update
    void Start()
    {
        playerScript = this.gameObject.GetComponentInParent<PlayerScript>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Pickups"))
        {
            ObjectPool.Instance.Despawn(PoolTagsConstants.PICKUPS_TAG, collision.gameObject);
            playerScript.Heal(addingHealth);
        }
    }
}
