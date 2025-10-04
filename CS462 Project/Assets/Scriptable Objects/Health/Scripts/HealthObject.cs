using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "New Health Object", menuName = "Inventory System/Health")]
public class HealthObject : ScriptableObject
{
    public int currentHealth;
    public int maxHealth;
    public int GetHealth()
    {
        return currentHealth;
    }
    public void SetHealth(int health)
    {
        if((currentHealth + health) > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else
        {
            currentHealth += health;
        }
    }
    public void ResetHP()
    {
        currentHealth = maxHealth;
    }
}
