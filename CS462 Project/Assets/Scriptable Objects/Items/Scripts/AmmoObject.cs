using UnityEngine;

[CreateAssetMenu(fileName = "Ammo", menuName = "Inventory System/Ammo")]
public class AmmoObject : ScriptableObject
{
    public int ammoCount;
    public WeaponObject weapon;

}
