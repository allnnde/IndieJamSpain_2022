using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    [Header("Health Settings")]
    public float startingHealth = -1;
    public float maxHealth = 10f;

    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float dashCooldown = 1f;
    public float dashForce = 400f;
    

    [Header("Damage Settings")]
    
    public float minDamage = 5f;
    public float maxDamage = 7f;

    [Header("Rage Settings")]
    public float limitTimeRage = 10f;
    public float rageOnDamage = 30f;
    public float rageTime = 5f;
    public float decreaseRate = 0.05f;

    [Header("Other Stats")]
    public float pickupRange = 30f;

    [HideInInspector] public bool inRage = false;
    [HideInInspector] public bool canDash = true;

}
