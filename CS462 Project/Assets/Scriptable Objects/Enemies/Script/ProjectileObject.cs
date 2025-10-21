using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileObject", menuName = "Scriptable Objects/ProjectileObject")]
public class ProjectileObject : ScriptableObject
{
    public int damage;
    public float speed;
    public float time;
}
