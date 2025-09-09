using System;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public WeaponData currentWeapon;
    private float fireRateTimer;
    void Start()
    {
        fireRateTimer = currentWeapon.fireRate;
    }

    private void Update()
    {
        if (shouldFire())
        {
            Shoot();
        }
    }

    bool shouldFire()
    {
        fireRateTimer += Time.deltaTime;
        if(fireRateTimer < currentWeapon.fireRate)
        {
            return false;
        }
        if(currentWeapon.isAutomatic && Input.GetButton("Fire1"))
        {
            return true;
        }

        if (currentWeapon.isSemiAuto && Input.GetButtonDown("Fire1"))
        {
            return true;
        }
        if (currentWeapon.isSingleShot && Input.GetButtonDown("Fire1"))
        {
            return true;
        }
        return false;
    }
    
    void Shoot()
    {
        fireRateTimer = 0f;
        Debug.Log("Shooting");
    }
}
