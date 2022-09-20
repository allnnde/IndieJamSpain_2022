using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed = 5f;
    public Rigidbody2D rigidBody;
    private PlayerControls playerControls;
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
    }
    
    // Calls 50 times a second, better to use with physics
    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        var moviment = playerControls.Player.Movement.ReadValue<Vector2>();
        var newPosition = rigidBody.position + moviment * moveSpeed * Time.fixedDeltaTime;
        rigidBody.MovePosition(newPosition);
    }
}
