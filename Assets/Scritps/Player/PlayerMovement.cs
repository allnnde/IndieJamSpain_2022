using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private PlayerScript playerScript;
    [HideInInspector] public bool canMove = true;

    private bool isDashing = false;
    private Animator animator;
    private SpriteRenderer spriteRenderer;


    private void Awake()
    {
        playerScript = GetComponent<PlayerScript>();
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    // Calls 50 times a second, better to use with physics
    void FixedUpdate()
    {
        if (canMove && !isDashing)
            Move();
    }

    private void Move()
    {
        var playerActions = playerScript.getPlayerControls().Player;
        var movement = playerActions.Movement.ReadValue<Vector2>();
        var speed = (playerScript.inRage) ? playerScript.stats.moveSpeed * 2 : playerScript.stats.moveSpeed;
        var newPosition = rigidBody.position + movement * speed * Time.fixedDeltaTime;

        AnimateMove(movement);

        rigidBody.MovePosition(newPosition);
    }

    private void AnimateMove(Vector2 newPosition)
    {
        Debug.Log(newPosition);
        var animation = GetAnimationName(newPosition);
        animator.Play(animation);
        spriteRenderer.flipX = animation == "Hero_Left";

    }

    public IEnumerator Dash(float speed, Vector2 direction)
    {
        isDashing = true;
        rigidBody.velocity = (direction * speed * Time.fixedDeltaTime);
        yield return new WaitForSeconds(0.3f);
        isDashing = false;
    }

    private string GetAnimationName(Vector2 direction)
    {
        if (direction.x == 0 && direction.y > 0)
            return "Hero_Back"; // AnimationLabelConstants.WalkingTopLabel;
        if (direction.x == 0 && direction.y < 0)
            return "Hero_Front";// AnimationLabelConstants.WalkingBottomLabel;
        if (direction.x < 0 && direction.y == 0)
            return "Hero_Left";//AnimationLabelConstants.WalkingLeftLabel;
        if (direction.x > 0 && direction.y == 0)
            return "Hero_Right";// AnimationLabelConstants.WalkingRightLabel;
        if (direction.x == 0 && direction.y == 0)
            return "Hero_Idle";//AnimationLabelConstants.IdleLabel;

        return string.Empty;
    }

}
