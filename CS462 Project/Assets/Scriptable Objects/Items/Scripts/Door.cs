using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class Door : MonoBehaviour
{
    public List<KeyObject> doorID = new List<KeyObject>();
    public string message;
    public GameWin winScreen;
    public void openDoor(KeyObject key)
    {
        GetComponent<Plane>().repairPart(doorID.IndexOf(key));
        doorID.Remove(key);
        if (doorID.Count == 0)
        {
            gameObject.SetActive(false);
            if(key.doorID < 6)
            {
                winScreen.gameObject.SetActive(true);
                winScreen.WinGame();
            }
        }
    }

    public string IDtoString()
    {
        string finalString = message + " (Needs ";
        if (doorID.Count == 1) {
            return finalString + doorID[0].KeyToString() + ")";
        }
        for(int i = 0; i < doorID.Count;i++)
        {
            if(i != doorID.Count - 1)
            {
                finalString += doorID[i].KeyToString() + ", ";
            }
            else
            {
                finalString += "and " + doorID[i].KeyToString() + ")";
            }
        }
        return finalString;
    }
}
