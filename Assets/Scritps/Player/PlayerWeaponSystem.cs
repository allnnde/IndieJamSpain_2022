using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeaponSystem : MonoBehaviour
{

    private PlayerScript playerScript;
    public bool canShoot = true;
    private WeaponObject selectedWeapon;

    private float shootCooldown = 0;
    private float timeToShoot;

    private Dictionary<WeaponType, WeaponObject> weaponObjects = new Dictionary<WeaponType, WeaponObject>();
    private InputAction shootAction;

    private void Awake()
    {
        playerScript = gameObject.GetComponent<PlayerScript>();
        var playerControls = playerScript.getPlayerControls();

        weaponObjects.Add(WeaponType.Sword, GetComponent<WeaponSword>());
        weaponObjects.Add(WeaponType.Pistol, GetComponent<WeaponPistol>());

        foreach (var weapon in weaponObjects.Values)
        {
            weapon.enabled = false;
        }
        playerControls.Weapons.SwitchTo1.performed += (InputAction.CallbackContext context) => SwitchWeapon(WeaponType.Sword);
        playerControls.Weapons.SwitchTo2.performed += (InputAction.CallbackContext context) => SwitchWeapon(WeaponType.Pistol);

        shootAction = playerScript.getPlayerControls().Player.Shoot;

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
        shootCooldown = selectedWeapon.fireRate;
        timeToShoot = 0;
    }
    private void Update()
    {
        var isShooting = System.Convert.ToBoolean(shootAction.ReadValue<float>());

        timeToShoot += Time.deltaTime;

        if (canShoot && selectedWeapon != null && isShooting && timeToShoot > shootCooldown)
        {
            selectedWeapon.Attack();
            timeToShoot = 0;
        }
        
    }
}
