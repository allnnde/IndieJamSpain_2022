using UnityEngine;

[System.Serializable]
public class PlayerStats
{

    [Header("Health Settings")]
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
    public int rageTime = 5;
    public float decreaseRate = 0.05f;
    public float rageMultiplier = 2f;
    public Color rageColor = new Color(255, 134, 134);

    [Header("Other Stats")]
    public float pickupRange = 30f;

    [Header("Particles")]
    public ParticleSystem onRageParticle;
    public ParticleSystem startRageParticle;
    public ParticleSystem finishRageParticle;
    public ParticleSystem healParticle;
    public ParticleSystem deathParticle;

    [HideInInspector] public bool inRage = false;
    [HideInInspector] public bool canDash = true;

}
