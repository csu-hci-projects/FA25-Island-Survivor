using UnityEngine;

[CreateAssetMenu(fileName = "New Healing Object", menuName = "Inventory System/Items/Healing")]
public class HealingObject : ItemObject
{
    public int healAmount;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Awake()
    {
        type = itemType.Healing;
    }
    override
    public bool Use()
    {
        return true;
        
    }
}
