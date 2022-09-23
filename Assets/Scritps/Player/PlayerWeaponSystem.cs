using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeaponSystem : MonoBehaviour
{
    public WeaponObject[] weapons;

    private PlayerScript playerScript;
    public bool canShoot = true;
    private int selectedWeapon = 0;


    private void Awake()
    {
        playerScript = this.gameObject.GetComponent<PlayerScript>();
        var playerControls = playerScript.getPlayerControls();
        playerControls.Weapons.SwitchTo1.performed += (InputAction.CallbackContext context) => SwitchWeapon(0);
        playerControls.Weapons.SwitchTo2.performed += (InputAction.CallbackContext context) => SwitchWeapon(1);

    }

    private void Start()
    {
        
    }

    private void SwitchWeapon(int weapon)
    {
        // FIXME: Si weapons[weapon] es null dice que está fuera de la boundary, tirando error
        Debug.Log("Switching to weapon: " + weapon);
        if (weapons.Length >= weapon && weapons[weapon] != null)
        {
            selectedWeapon = weapon;
            Debug.Log(weapons[selectedWeapon]);
        }
    }


    private void FixedUpdate()
    {
        var ShootAction = playerScript.getPlayerControls().Player.Shoot;
        var isShooting = System.Convert.ToBoolean(ShootAction.ReadValue<float>());

        if (canShoot && weapons[selectedWeapon] != null && isShooting)
        {
            weapons[selectedWeapon].TryShoot();
        }
        
    }


}
