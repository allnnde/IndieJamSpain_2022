using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]
public abstract class WeaponObject : ScriptableObject
{
    [HideInInspector] public PlayerMouseTracking playerMouse;
    [HideInInspector] public PlayerScript playerScript;

    public float fireRate = 0.2f;
    public float bulletSpeed = 20f;
    [HideInInspector] public float damage;
    [HideInInspector] public GameObject player;

    [HideInInspector] public float timeUntilShoot = 0f;

    void Awake()
    {
        timeUntilShoot = 0f;
    }

    // Start is called before the first frame update

    public void GetPlayerVariables()
    {
        // Hacer que solo se pongan los valores si son null
        player = GameObject.Find("Player");
        playerMouse = player.GetComponent<PlayerMouseTracking>();
        playerScript = player.GetComponent<PlayerScript>();
        Debug.Log("player: " + player + " playerMouse: " + playerMouse + " playerScript: " + playerScript);
    }
    public void Shoot()
    {
        GetPlayerVariables();
        timeUntilShoot = Time.time + fireRate;
        damage = playerScript.GetDamage();
        CreateBullets();
    }

    public abstract void CreateBullets();

    public void TryShoot()
    {
        //FIXIT: timeUntilShoot tiene un valor exageradamente alto sin contexto
        Debug.Log("Time: " + Time.time + " Disparo: " + timeUntilShoot + " Comprobacion: " + (Time.time >= timeUntilShoot));
        if (Time.time >= timeUntilShoot)
        {
            Debug.Log("Disparo2");
            Shoot();
        }
    }

 
}
