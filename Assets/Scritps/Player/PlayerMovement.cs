using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private PlayerControls playerControls;

    public Rigidbody2D rigidBody;
    private PlayerScript playerScript;
    public bool canMove = true;

    private bool isDashing = false;


    private void Awake()
    {
        playerScript = this.gameObject.GetComponent<PlayerScript>();
    }


    // Calls 1 time per frame
    void Update()
    {
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
        var speed = (playerScript.IsInRage()) ? playerScript.moveSpeed * 2 : playerScript.moveSpeed;
        var newPosition = rigidBody.position + movement * speed * Time.fixedDeltaTime;
        rigidBody.MovePosition(newPosition);
    }

    public IEnumerator Dash(float speed, Vector2 direction)
    {
        isDashing = true;
        rigidBody.velocity = (direction * speed * Time.fixedDeltaTime);
        yield return new WaitForSeconds(0.3f);
        isDashing = false;
    }
}
