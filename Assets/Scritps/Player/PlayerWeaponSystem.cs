using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeaponSystem : MonoBehaviour
{

    private PlayerScript playerScript;
    [HideInInspector] public bool canShoot = true;
    private WeaponObject selectedWeapon;

    private float shootCooldown = 0;
    private float timeToShoot;

    private Dictionary<WeaponType, WeaponObject> weaponObjects = new Dictionary<WeaponType, WeaponObject>();
    private InputAction shootAction;

    private void Awake()
    {
        playerScript = gameObject.GetComponent<PlayerScript>();
        var playerControls = playerScript.getPlayerControls();
        if (playerControls == null)
        {
            playerControls = new PlayerControls();
            playerControls.Enable();
        }
        weaponObjects.Add(WeaponType.Sword, GetComponent<WeaponSword>());
        weaponObjects.Add(WeaponType.Pistol, GetComponent<WeaponPistol>());
        weaponObjects.Add(WeaponType.Shotgun, GetComponent<WeaponShotgun>());

        foreach (var weapon in weaponObjects.Values)
        {
            weapon.enabled = false;
        }
        playerControls.Weapons.SwitchTo1.performed += (InputAction.CallbackContext context) => SwitchWeapon(WeaponType.Sword);
        playerControls.Weapons.SwitchTo2.performed += (InputAction.CallbackContext context) => SwitchWeapon(WeaponType.Pistol);
        playerControls.Weapons.SwitchTo3.performed += (InputAction.CallbackContext context) => SwitchWeapon(WeaponType.Shotgun);

        shootAction = playerControls.Player.Shoot;

    }

    private void SwitchWeapon(WeaponType weaponType)
    {
        if (!weaponObjects.TryGetValue(weaponType, out selectedWeapon))
            return;

        foreach (var weaponScrip in weaponObjects.Values)
        {
            weaponScrip.enabled = false;
        }

        selectedWeapon.enabled = true;
        shootCooldown = selectedWeapon.fireRate / 2;
        
        timeToShoot = 0;
    }
    private void Update()
    {
        var isShooting = System.Convert.ToBoolean(shootAction.ReadValue<float>());

        timeToShoot += (playerScript.inRage) ? Time.deltaTime * playerScript.stats.rageMultiplier : Time.deltaTime;

        if (canShoot && selectedWeapon != null && isShooting && timeToShoot > shootCooldown)
        {
            selectedWeapon.Attack();
            timeToShoot = 0;
            // QOL, cambiar armas hace que esa arma tenga su mitad de cooldown pa poder disparar
            shootCooldown = selectedWeapon.fireRate;
        }
        
    }
}
