using UnityEngine;

[CreateAssetMenu(fileName = "New Key Object", menuName = "Inventory System/KeyObject")]
public class KeyObject : ItemObject
{

    public int doorID;
    public void Awake()
    {
        type = itemType.Key;
    }
    override
    public bool Use()
    {
        return true;
    }
}
