using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{

    //TODO ver de sacar a una clase/s
    [HideInInspector] public float health;
    public float maxHealth = 10f;
    public float moveSpeed = 5f;
    public float dashSpeed = 400f;
    public float pickupRange = 30f;
    public float minDamage = 5f;
    public float maxDamage = 7f;
    public float dashCooldown = 1f;
    public bool canDash = true;
    public float limitTimeRage = 10f;
    
    public float rageOnDamage = 30f;
    public float rageTime = 5f;
    private float rage = 0f;
    const int maxRage = 100;
    private PlayerControls playerControls;
    private PlayerMovement playerMovement;
    private PlayerMouseTracking playerMouseTracking;

    private bool inRage = false;
    private float timeToDash = 0f;
    private float timeWithoutRage = 0f;

    private void Awake()
    {
        playerControls = new PlayerControls();
        playerControls.Enable();
        playerControls.Player.Dash.performed += CheckDash;
        playerMovement = gameObject.GetComponent<PlayerMovement>();
        playerMouseTracking = gameObject.GetComponent<PlayerMouseTracking>();

        health = maxHealth;
        
    }

    private void CheckDash(InputAction.CallbackContext context) 
    {
        Debug.Log("timeToDash: " + timeToDash + " dashCooldown: " + dashCooldown);
        if (canDash && timeToDash > dashCooldown)
        {
            StartCoroutine(playerMovement.Dash(dashSpeed, playerControls.Player.Movement.ReadValue<Vector2>()));
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
        AddRage(rageOnDamage);

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
            StartCoroutine(EnterRage());
        }
    }

    //TODO ver si esto es necessario como coroutine, sino ver de trasmormar en async o timer simple
    private IEnumerator EnterRage()
    {
        inRage = true;
        Debug.Log("It's morbin time");
        yield return new WaitForSeconds(rageTime);
        inRage = false;
        rage = 0f;
    }

    //TODO no es necesario si ya tenemos la propiedad
    public bool IsInRage()
    {
        return inRage;
    }
    private void Update()
    {

        timeToDash += Time.deltaTime;

        if (!inRage)
        {
            timeWithoutRage += Time.deltaTime;
        }

        if (timeWithoutRage > limitTimeRage)
        {
            DecreaseRage();
        }
        
    }
    private void DecreaseRage()
    {
        //TODO: Parametrizar
        var decreaseRate = 0.05f;
        var rageLeft = rage - decreaseRate;
        rage = Mathf.Max(0, rageLeft);

    }
}
