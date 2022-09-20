using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed = 5f;
    public Rigidbody2D rigidBody;
    private PlayerControls playerControls;
    private Vector2 movement = Vector2.zero;
    public bool canMove = true;


    private void Awake()
    {
        playerControls = new PlayerControls();
        playerControls.Enable();
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
        movement.x = playerControls.Player.Movement.ReadValue<Vector2>().x;
        movement.y = playerControls.Player.Movement.ReadValue<Vector2>().y;
    }

    
    // Calls 50 times a second, better to use with physics
    void FixedUpdate()
    {
        rigidBody.MovePosition(rigidBody.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
