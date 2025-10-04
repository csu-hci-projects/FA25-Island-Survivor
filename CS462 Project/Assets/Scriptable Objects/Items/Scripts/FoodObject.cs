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
    public void Use()
    {
        Debug.Log("Using Food");
        if (restoreStaminaValue + Hunger.currentHealth > Hunger.maxHealth)
        {
            Hunger.currentHealth = Hunger.maxHealth;
        }
        else
        {
            Hunger.currentHealth += restoreStaminaValue;
        }
    }
}
