using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponObject : MonoBehaviour
{
    protected PlayerMouseTracking playerMouse;
    protected PlayerScript playerScript;
    public float damage = 5;

    public float fireRate = 1.5f;
    public float bulletSpeed = 20f;


    private void Awake()
    {
        playerMouse = GetComponent<PlayerMouseTracking>();
        playerScript = GetComponent<PlayerScript>();
    }

    public void Attack()
    {
        PreparateAttackMode();
    }
    public abstract void PreparateAttackMode();
}
