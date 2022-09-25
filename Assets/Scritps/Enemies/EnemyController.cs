using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class EnemyController : MonoBehaviour, IPoolable
{
    public GameObject Owner => gameObject;
    public float MaxLife = 20;
    public float Damage = 3;
    public float AttackCooldown = 1;
    private float actualLife;
    string animPreviusly = "";

    public string PoolTag { get; set; }

    public string tagPlayer = "Player";
    public float Speed = 2;
    protected GameObject player;

    protected Animator animator;
    protected SpriteRenderer spriteRenderer;

    public Image lifeBar;


    public void OnInstanciate(Transform parent)
    {
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        transform.parent = parent;
        gameObject.SetActive(false);
        actualLife = MaxLife;
    }

    public void OnSpawn(Vector3 position, Quaternion rotation)
    {
        transform.position = position;
        transform.rotation = rotation;
        gameObject.SetActive(true);
    }

    public void SetLevel(int level)
    {
        MaxLife *= 1f + (level / 100f);
        Damage *= 1f + (level / 100f);
        Speed *= 1f + (level / 100f);
        if (AttackCooldown > 0.3)
        {
            AttackCooldown *= 1f - (level / 100f);
        }
    }

    public void OnDespawn()
    {
        gameObject.SetActive(false);
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag(tagPlayer);
    }

    // Update is called once per frame
    public virtual void Update()
    {
        Move();
    }


    public void OnCollisionEnter2D(Collision2D collision)
    {
        var target = collision.gameObject;
        if (target.CompareTag("Player"))
        {
            DamageTarget(target.GetComponent<PlayerScript>());
        }
    }

    public abstract void DamageTarget(PlayerScript player);
    public abstract void Move();

    public void TakeDamage(float damage)
    {
        actualLife -= damage;
        lifeBar.fillAmount = actualLife / MaxLife;
        if(actualLife <= 0)
        {
            if (UnityEngine.Random.Range(0, 100) <= 10)
            {
                ObjectPool.Instance.Spawn(PoolTagsConstants.PICKUPS_TAG, transform.position, Quaternion.identity);
            }
            
            ObjectPool.Instance.Despawn(PoolTag, Owner);
            
        }
    }

    private string GetAnimationName(Vector2 direction)
    {
        if (direction.x == 0 && direction.y > 0)
            animPreviusly = "Anim_Back"; // AnimationLabelConstants.WalkingTopLabel;
        if (direction.x == 0 && direction.y < 0)
            animPreviusly = "Anim_Front";// AnimationLabelConstants.WalkingBottomLabel;
        if (direction.x < 0 && direction.y == 0)
            animPreviusly = "Anim_Left";//AnimationLabelConstants.WalkingLeftLabel;
        if (direction.x > 0 && direction.y == 0)
            animPreviusly = "Anim_Right";// AnimationLabelConstants.WalkingRightLabel;
        if (direction.x == 0 && direction.y == 0)
            animPreviusly = "Anim_Idle";//AnimationLabelConstants.IdleLabel;
        return animPreviusly;
    }

    protected void AnimateMove(Vector2 newPosition)
    {
        var FloorToInt = Vector2Int.FloorToInt(newPosition);
        var CeilToInt = Vector2Int.CeilToInt(newPosition);
        var RoundToInt = Vector2Int.RoundToInt(newPosition);

        //Debug.Log("FloorToInt " + FloorToInt);
        //Debug.Log("CeilToInt " + CeilToInt);
        Debug.Log("RoundToInt " + RoundToInt);

        var animation = GetAnimationName(RoundToInt);
        Debug.Log("animation " + animation);
        animator.Play(animation);
        spriteRenderer.flipX = animation == "Anim_Left";
    }

}
