
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Inventory/Weapon")]
public class WeaponData : Item
{
    public int damage;
    public float range;
    public float fireRate;
    public int magazineSize;
    public float reloadTime;
    public bool isAutomatic;
    public bool isSemiAuto;
    public bool isSingleShot;
    public GameObject weaponPrefab;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            if (isAutomatic)
            {
                isAutomatic = false;
                isSemiAuto = true;
                isSingleShot = false;
                Debug.Log("Switched to Semi-Automatic");
            }
            else if (isSemiAuto)
            {
                isAutomatic = false;
                isSemiAuto = false;
                isSingleShot = true;
                Debug.Log("Switched to Single Shot");
            }
            else if (isSingleShot)
            {
                isAutomatic = true;
                isSemiAuto = false;
                isSingleShot = false;
                Debug.Log("Switched to Automatic");
            }
            else if (isSingleShot)
            {

            }
        }
    }
}

