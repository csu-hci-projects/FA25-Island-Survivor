using UnityEngine;

public class EnemyActor : MonoBehaviour
{
    public EnemyObject enemyType;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyType.EnemyHealth.ResetHP();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyType.EnemyHealth.currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
