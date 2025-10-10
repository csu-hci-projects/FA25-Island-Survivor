using TMPro;
using UnityEngine;

public class PickupTextManager : MonoBehaviour
{
    public TextMeshProUGUI pickupText;
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<GroundItem>() != null)
        {
            pickupText.text = "Press E to pick up " + other.gameObject.GetComponent<GroundItem>().item.description;
        }
        else if (other.gameObject.GetComponent<Door>() != null)
        {
            pickupText.text = "Press Left Click to Open Door (Needs " + other.gameObject.GetComponent<Door>().IDtoString() + ")";
        }
    }
    public void OnTriggerExit(Collider other)
    {
        pickupText.text = "";
    }
}
