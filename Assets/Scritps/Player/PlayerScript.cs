using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    [HideInInspector] public float health;
    public float maxHealth = 10f;
    public float moveSpeed = 5f;
    public float pickupRange = 30f;
    public float minDamage = 5f;
    public float maxDamage = 7f;
    
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


    private void OnEnable() {
        playerControls.Enable();
    }

    private void OnDisable() {
        playerControls.Disable();
    }

    public float GetDamage(bool randomized = true)
    {
        return (randomized) ? (Mathf.Round(Random.Range(minDamage, maxDamage) * 10.0f) * 0.1f) : maxDamage;
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
        //gameObject.setActive(false);
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
