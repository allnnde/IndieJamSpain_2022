using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeaponSystem : MonoBehaviour
{
    public WeaponObject[] weapons;

    private PlayerScript playerScript;
    public bool canMove = true;


    private void Awake()
    {
        playerScript = this.gameObject.GetComponent<PlayerScript>();
    }

    
    // Calls 1 time per frame
    void Update()
    {
    }

    
}
