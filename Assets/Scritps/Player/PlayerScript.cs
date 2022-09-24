using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    [HideInInspector] public PlayerStats stats;

    [HideInInspector] public float health;
    private PlayerControls playerControls;
    private PlayerMovement playerMovement;
    private float rage = 0f;
    const int maxRage = 100;
    [HideInInspector] public bool inRage = false;
    [HideInInspector] public bool canDash = true;
    private float timeToDash = 0f;
    private float timeWithoutRage = 0f;

    private List<GameObject> bulletList = new List<GameObject>();
    private void Awake()
    {
        stats = GetComponent<PlayerStats>();
        playerControls = new PlayerControls();
        playerControls.Enable();
        playerControls.Player.Dash.performed += CheckDash;
        playerMovement = gameObject.GetComponent<PlayerMovement>();

        health = (stats.startingHealth > 0) ? stats.startingHealth : stats.maxHealth;

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
        var healthLeft = health - dmg;
        health = Mathf.Max(0, healthLeft);
        AddRage(stats.rageOnDamage);

        if (health == 0)
        {
            Die();
        }
    }


    private void Die()
    {
        Debug.Log("He muerto nooooOOoOoOOoOooOOO");
        //gameObject.SetActive(false);
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
            //TODO ver si esto es necessario como coroutine, sino ver de trasmormar en async o timer simple
            EnterRage();
        }
    }

    //TODO ver si esto es necessario como coroutine, sino ver de trasmormar en async o timer simple
    private async Task EnterRage()
    {
        inRage = true;
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        CreateRageBullets();
        await Task.Delay(stats.rageTime * 1000);
        ApplyRageFinal();
    }

    private void CreateRageBullets() 
    {
        var bullets = 1;
        var diffAngle = 360 / bullets;
        for (int i = 1; i <= bullets; i++)
        {
            //TODO elegir bien posicion de las balas
            var bullet = ObjectPool.Instance.Spawn(PoolTagsConstants.BULLET_RAGE_POOL_TAG, transform.position + new Vector3(0, 2, 0), Quaternion.identity);
            //bullet.transform.parent = transform;
            bullet.GetComponent<RageBulletScript>().Damage = stats.maxDamage * 2;
            bullet.transform.RotateAround(gameObject.transform.position, new Vector3(0, 0, 1), (diffAngle * i) * Time.deltaTime);
            bulletList.Add(bullet);
        }

    }

    private void ApplyRageFinal()
    {
        inRage = false;
        
        foreach(var bullet in bulletList)
        {
            ObjectPool.Instance.Despawn(PoolTagsConstants.BULLET_RAGE_POOL_TAG, bullet);
        }

        bulletList.Clear();
        
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
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
