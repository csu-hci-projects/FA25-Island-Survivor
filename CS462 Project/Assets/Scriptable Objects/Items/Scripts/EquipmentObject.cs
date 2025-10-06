using System.Collections;
using StarterAssets;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment Object", menuName = "Inventory System/Items/Equipment")]
public class EquipmentObject : ItemObject
{
    public int speedIncrease;
    public int speedTime;
    public void Awake()
    {
        type = itemType.Equipment;
    }
    override
    public bool Use()
    {
        return true;
    }
    
}
