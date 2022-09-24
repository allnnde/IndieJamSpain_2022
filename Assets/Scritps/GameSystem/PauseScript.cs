using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseScript : MonoBehaviour
{
    private PlayerControls playerControls;
    private bool isOpen = false;

    private void Awake()
    {
        playerControls = new PlayerControls();
        playerControls.Enable();
        playerControls.UI.Pause.performed += ToggleMenu;
    }

    private void ToggleMenu(InputAction.CallbackContext context)
    { 
        isOpen = !isOpen;
        gameObject.SetActive(isOpen);
    }

    private void OnEnable()
    {
        Time.timeScale = 0f;
    }

    private void OnDisable()
    {
        Time.timeScale = 1f;
    }
}
