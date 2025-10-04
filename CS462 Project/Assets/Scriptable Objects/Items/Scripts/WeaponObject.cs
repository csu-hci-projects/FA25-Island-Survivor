using UnityEngine;

public enum weaponType
{
    Melee,
    Ranged
}

[CreateAssetMenu(fileName = "New Weapon Object", menuName = "Inventory System/Items/Weapons" +"")]

public class WeaponObject : ItemObject
{
    public int damage;
    public int magSize;
    public int bulletCount;
    public int ammo;
    public float fireCooldown;
    public bool isAutomatic;
    public float range;
    public float reloadCooldown;
    public weaponType weaponType;
    public LayerMask attackLayer;
    public void Awake()
    {
        type = itemType.Weapon;
    }
    override
    public void Use()
    {
        if (weaponType == weaponType.Melee)
        {
            Debug.Log("Using Melee Weapon");
            //deal damage in player box collider for swinging weapon
            //for all objects with tag enemy in the box at this time, access their health value and decrement it
        }
        else
        {
            Debug.Log("Using Ranged Weapon");
            bulletCount--;
        }
        
    }
    public void Reload()
    {
        if (ammo > 0 && bulletCount < magSize)
        {
            Debug.Log("Reloading Weapon");
            if (ammo - (magSize - bulletCount) > 0)
            {
                ammo -= magSize - bulletCount;
                bulletCount = magSize;
            }
            else
            {
                bulletCount += ammo;
                ammo = 0;
            }
        }
    }
}
