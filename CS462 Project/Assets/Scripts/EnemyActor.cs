using UnityEngine;

public class EnemyActor : MonoBehaviour
{
    public EnemyObject enemyType;
    private int currentHealth = 1;
    public int[] probabilities = new int[5];
    public GameObject[] Prefabs = new GameObject[5];
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = enemyType.EnemyHealth.maxHealth;

    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            for (int i = 0; i < Prefabs.Length; i++)
            {
                int roll = Random.Range(0, 100);
                if (roll <= probabilities[i])
                {
                    GameObject SpawnedItem = Instantiate(Prefabs[i], transform.position + new Vector3(i+1, 0.2f, 0), Quaternion.identity);
                    SpawnedItem.GetComponent<GroundItem>().amount = Random.Range(1, 5);
                }
            }
            Destroy(gameObject);
        }
    }

    public void dealDamage(int damage)
    {
        currentHealth -= damage;
    }
    public int getHealth()
    {
        return currentHealth;
    }
}
