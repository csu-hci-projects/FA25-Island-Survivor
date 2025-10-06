using UnityEngine;

[CreateAssetMenu(fileName = "New Healing Object", menuName = "Inventory System/Items/Healing")]
public class HealingObject : ItemObject
{
    public HealthObject PlayerHealth;
    public int healAmount;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Awake()
    {
        type = itemType.Healing;
    }
    override
    public bool Use()
    {
        Debug.Log("Using Healing");
        if(PlayerHealth.currentHealth != 100)
        {
            if (PlayerHealth.currentHealth + healAmount > PlayerHealth.maxHealth)
            {
                PlayerHealth.currentHealth = PlayerHealth.maxHealth;
            }
            else
            {
                PlayerHealth.currentHealth += healAmount;
            }
            return true;
        }
        return false;
        
    }
}
