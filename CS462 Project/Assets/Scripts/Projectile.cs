using UnityEngine;

public class Projectile : MonoBehaviour
{
    public ProjectileObject projectile; 
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<SpeedManager>().ApplyPoison(projectile.damage, projectile.time);
        }
        if (!other.gameObject.CompareTag("Enemy") && !other.gameObject.CompareTag("Desert"))
        {
            Destroy(gameObject);
        }
    }
}
