using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeaponSystem : MonoBehaviour
{

    public GameObject bulletPrefab;

    private PlayerMouseTracking playerMouse = null;
    private PlayerControls playerControls;

    public float fireRate = 0.2f;
    private bool canShoot = true;
    public float bulletSpeed = 20f;

    float timeUntilShoot;

    private void Awake()
    {
        playerMouse = this.gameObject.GetComponent<PlayerMouseTracking>();
        playerControls = new PlayerControls();
        playerControls.Enable();
        //playerControls.Player.Shoot.performed += Shoot;
    }


    public void Shoot()
    {
        timeUntilShoot = Time.time + fireRate;
        
        GameObject bullet = Instantiate(bulletPrefab, transform.position, playerMouse.QuaternionTowardsMouse());
        Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();
        bulletRB.AddForce(bullet.transform.up * bulletSpeed, ForceMode2D.Impulse);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Calls 50 times a second, better to use with physics
    void FixedUpdate()
    {
        bool isShooting = System.Convert.ToBoolean(playerControls.Player.Shoot.ReadValue<float>());
        if(isShooting && canShoot && (Time.time > timeUntilShoot))
        {
            Shoot();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
 
    }
}
