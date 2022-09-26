using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
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
    public GameObject playerRenderer;

    public Image rageBar;

    private void Awake()
    {
        playerControls = new PlayerControls();
        playerControls.Enable();
        playerControls.Player.Dash.performed += CheckDash;
        playerMovement = gameObject.GetComponent<PlayerMovement>();
        rageBar = GameObject.Find("rageBar").GetComponent<Image>();
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

        if (health <= 0)
        {
            Die();
        }
    }


    private void Die()
    {
        gameObject.SetActive(false);
        stats.deathParticle?.GetComponent<ParticleSystem>().Emit(1);
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
            _ = EnterRage();
        }
        rageBar.fillAmount = rage / maxRage;
    }

    public void Heal(float quantity)
    {
        var healthLeft = health + quantity;
        health = Mathf.Min(stats.maxHealth, healthLeft);
        lifeBar.fillAmount = health / stats.maxHealth;

    }

    private async Task EnterRage()
    {
        inRage = true;
        stats.startRageParticle?.GetComponent<ParticleSystem>().Play(true);
        stats.onRageParticle?.GetComponent<ParticleSystem>().Play();
        stats.startRageParticle?.GetComponent<ParticleSystem>().Emit(1);
        stats.onRageParticle?.GetComponent<ParticleSystem>().Emit(1);

        Debug.Log(stats.onRageParticle.GetComponent<ParticleSystem>().isPlaying);
        playerRenderer.GetComponent<SpriteRenderer>().color = stats.rageColor;
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
        stats.onRageParticle?.GetComponent<ParticleSystem>().Stop();
        stats.finishRageParticle?.GetComponent<ParticleSystem>().Play();
        stats.finishRageParticle?.GetComponent<ParticleSystem>().Emit(1);

        var rageBullet = GetComponentsInChildren<RageBulletScript>(true);
        foreach (var item in rageBullet)
        {
            item.gameObject.SetActive(false);
        }

        playerRenderer.GetComponent<SpriteRenderer>().color = Color.white;
        rage = 0f;
        rageBar.fillAmount = 0f;
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
