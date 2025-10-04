using UnityEngine;

[CreateAssetMenu(fileName = "EnemyObject", menuName = "Scriptable Objects/EnemyObject")]
public class EnemyObject : ScriptableObject
{
    public HealthObject EnemyHealth;
    public int MoveSpeed;
    public int damage;
    public float attackSpeed;

}
