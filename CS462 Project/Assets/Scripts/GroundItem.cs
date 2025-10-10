using UnityEngine;

public class GroundItem : MonoBehaviour
{
    public ItemObject item;
    public int amount;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //show UI
        }

    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //Hide UI
        }
    }
}
