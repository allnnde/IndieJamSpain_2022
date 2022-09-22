using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    public float health;
    public float maxHealth = 10f;
    public float moveSpeed = 5f;
    public float pickupRange = 30f;
    
    public float rageOnDamage = 30f;
    public float rageOnHit = 5f;
    public float rageTime = 5f;
    private float rage = 0f;
    const int maxRage = 100;
    private PlayerControls playerControls;


    private void Awake()
    {
        playerControls = new PlayerControls();
        playerControls.Enable();
        health = maxHealth;
    }


    public PlayerControls getPlayerControls()
    {
        return playerControls;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float dmg)
    {
        var healthLeft = health - dmg;
        health = Mathf.Max(0, healthLeft);

        if (health == 0)
        {
            Die();
        }
    }


    private void Die()
    {
        Debug.Log("He muerto nooooOOoOoOOoOooOOO");
        Destroy(this.gameObject);
    }

    public void AddRage(float quantity)
    {
        var rageLeft = rage + quantity;
        rage = Mathf.Min(maxRage, rageLeft);

        if (rage >= maxRage)
        {
            EnterRage();
        }
    }

    private void EnterRage()
    {

    }
}
