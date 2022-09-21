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


    private void Awake()
    {
        playerScript = this.gameObject.GetComponent<PlayerScript>();
    }

    /*
    private void OnEnable() {
        playerControls.Enable();
    }

    private void OnDisable() {
        playerControls.Disable();
    }
    */
    
    // Calls 1 time per frame
    void Update()
    {
    }
    
    // Calls 50 times a second, better to use with physics
    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        var playerActions = playerScript.getPlayerControls().Player;
        var movement = playerActions.Movement.ReadValue<Vector2>();
        var newPosition = rigidBody.position + movement * playerScript.moveSpeed; * Time.fixedDeltaTime;
        rigidBody.MovePosition(newPosition);
    }
}
