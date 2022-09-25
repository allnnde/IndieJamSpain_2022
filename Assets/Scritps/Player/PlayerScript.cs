using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    public PlayerStats stats = new PlayerStats();
    private float health;
    private PlayerControls playerControls;
    private PlayerMovement playerMovement;
    private float rage = 0f;
    const int maxRage = 100;
    [HideInInspector] public bool inRage = false;
    [HideInInspector] public bool canDash = true;
    private float timeToDash = 0f;
    private float timeWithoutRage = 0f;

    public Image lifeBar;

    private void Awake()
    {
        playerControls = new PlayerControls();
        playerControls.Enable();
        playerControls.Player.Dash.performed += CheckDash;
        playerMovement = gameObject.GetComponent<PlayerMovement>();
        health = stats.maxHealth;

    }

    private void CheckDash(InputAction.CallbackContext context)
    {
        var dashCooldown = stats.dashCooldown;

        if (canDash && timeToDash > dashCooldown)
        {
            StartCoroutine(playerMovement.Dash(stats.dashForce, playerControls.Player.Movement.ReadValue<Vector2>()));
            timeToDash = 0f;
        }
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
    public void TakeDamage(float dmg)
    {
        health -= dmg;
        AddRage(stats.rageOnDamage);
        
        lifeBar.fillAmount = health / stats.maxHealth;

        if (health == 0)
        {
            Die();
        }
    }


    private void Die()
    {
        Debug.Log("He muerto nooooOOoOoOOoOooOOO");
        gameObject.SetActive(false);
    }

    public void AddRage(float quantity)
    {
        if (inRage)
            return;
        timeWithoutRage = 0f;
        var rageLeft = rage + quantity;
        rage = Mathf.Min(maxRage, rageLeft);

        if (rage >= maxRage)
        {
            EnterRage();
        }
    }

    public void Heal(float quantity)
    {
        var healthLeft = health + quantity;
        health = Mathf.Min(stats.maxHealth, healthLeft);

    }

    private async Task EnterRage()
    {
        inRage = true;
        CreateRageBullets();
        await Task.Delay(stats.rageTime * 1000);
        ApplyRageFinal();
    }

    private void CreateRageBullets() 
    {
        var rageBullet = GetComponentsInChildren<RageBulletScript>(true);
        foreach (var item in rageBullet)
        {
            item.gameObject.SetActive(true);
        }

    }

    private void ApplyRageFinal()
    {
        inRage = false;

        var rageBullet = GetComponentsInChildren<RageBulletScript>(true);
        foreach (var item in rageBullet)
        {
            item.gameObject.SetActive(false);
        }
        rage = 0f;
    }

    private void Update()
    {

        timeToDash += Time.deltaTime;

        if (!inRage)
        {
            timeWithoutRage += Time.deltaTime;
        }

        if (timeWithoutRage > stats.limitTimeRage)
        {
            DecreaseRage();
        }
        
    }
    private void DecreaseRage()
    {
        var rageLeft = rage - stats.decreaseRate;
        rage = Mathf.Max(0, rageLeft);
        if(rage == 0)
        {
            ApplyRageFinal();
        }

    }
}
