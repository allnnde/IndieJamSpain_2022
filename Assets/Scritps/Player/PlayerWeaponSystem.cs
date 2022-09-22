using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeaponSystem : MonoBehaviour
{


    private PlayerMouseTracking playerMouse;
    private PlayerScript playerScript;

    public float fireRate = 0.2f;
    private bool canShoot = true;
    public float bulletSpeed = 20f;

    float timeUntilShoot;

    private void Awake()
    {
        playerMouse = this.gameObject.GetComponent<PlayerMouseTracking>();
        playerScript = this.gameObject.GetComponent<PlayerScript>();
    }


    public void Shoot()
    {
        timeUntilShoot = Time.time + fireRate;

        GameObject bullet = ObjectPool.Instance.Spawn(PoolTagsConstants.BULLET_PLAYER_POOL_TAG, transform.position, playerMouse.QuaternionTowardsMouse());
        
        Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();
        bulletRB.AddForce(bullet.transform.up * bulletSpeed, ForceMode2D.Impulse);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

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

    // Update is called once per frame
    void Update()
    {
 
    }
}
