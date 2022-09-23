using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponObject : MonoBehaviour
{
    protected PlayerMouseTracking playerMouse;
    protected PlayerScript playerScript;
    protected float damage;

    public float fireRate = 1.5f;
    public float bulletSpeed = 20f;


    private void Awake()
    {
        playerMouse = GetComponent<PlayerMouseTracking>();
        playerScript = GetComponent<PlayerScript>();
        damage = playerScript.GetDamage();
    }

    public void Attack()
    {
        PreparateAttackMode();
    }
    public abstract void PreparateAttackMode();
}
