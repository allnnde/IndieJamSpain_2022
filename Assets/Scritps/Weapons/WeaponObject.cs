using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponObject : MonoBehaviour
{
    [HideInInspector] public PlayerMouseTracking playerMouse;
    [HideInInspector] public PlayerScript playerScript;

    public float fireRate = 0.2f;
    public float bulletSpeed = 20f;
    protected float damage;

    float timeUntilShoot;

    private void Awake()
    {
        playerMouse = this.gameObject.GetComponent<PlayerMouseTracking>();
        playerScript = this.gameObject.GetComponent<PlayerScript>();
    }

    // Start is called before the first frame update
    void Start()
    {
        SetValues();
    }


    public void Shoot()
    {
        timeUntilShoot = Time.time + fireRate;
        damage = playerScript.GetDamage();
        CreateBullets();
    }

    public abstract void SetValues();
    public abstract void CreateBullets();

    public void TryShoot()
    {
        if (Time.time > timeUntilShoot)
        { 
            Shoot();
        }
    }

 
}
