using UnityEngine;

[CreateAssetMenu(fileName = "New Food Object", menuName ="Inventory System/Items/Food")]
public class FoodObject : ItemObject
{
    public int restoreStaminaValue;
    public HealthObject Hunger;
    public void Awake()
    {
        type = itemType.Food;
    }
    override
    public bool Use()
    {
        Debug.Log("Using Food");
        if(Hunger.currentHealth < 300)
        {
            if (restoreStaminaValue + Hunger.currentHealth > Hunger.maxHealth)
            {
                Hunger.currentHealth = Hunger.maxHealth;
            }
            else
            {
                Hunger.currentHealth += restoreStaminaValue;
            }
            return true;
        }
        return false;
        
    }
}
