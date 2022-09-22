using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponObject : MonoBehaviour
{
    [HideInInspector] public PlayerMouseTracking playerMouse;
    [HideInInspector] public PlayerScript playerScript;

    public float fireRate = 0.2f;
    [HideInInspector] public bool canShoot = true;
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

    private bool WillShoot()
    {
        var ShootAction = playerScript.getPlayerControls().Player.Shoot;
        var isShooting = System.Convert.ToBoolean(ShootAction.ReadValue<float>());
        
        return (isShooting && canShoot && (Time.time > timeUntilShoot));
    }

    // Calls 50 times a second, better to use with physics
    void FixedUpdate()
    {
        if(WillShoot())
        {
            Shoot();
        }
    }

    
}
