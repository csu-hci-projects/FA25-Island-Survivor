using TMPro;
using UnityEngine;

public class PickupTextManager : MonoBehaviour
{
    public TextMeshProUGUI pickupText;
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Door>() != null)
        {
            updateText(other.GetComponent<Door>());
        }
    }
    public void OnTriggerExit(Collider other)
    {
        pickupText.text = "";
    }
    public void updateText(Door door)
    {
        if (door.doorID.Count != 0)
        {
            pickupText.text = door.IDtoString();
        }
        else
        {
            pickupText.text = "";
        }
    }
}
