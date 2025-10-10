using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class Door : MonoBehaviour
{
    public List<int> doorID = new List<int>();
    public void openDoor(int ID)
    {
        doorID.Remove(ID);
        if (doorID.Count == 0)
        {
            Destroy(this.gameObject);
            //add code to check if this object is the plane. If it is, end the game instead.
        }
    }

    public string IDtoString()
    {
        string finalString = "";
        if (doorID.Count == 1) {
            return "Key " + doorID[0].ToString();
        }
        for(int i = 0; i < doorID.Count;i++)
        {
            if(i != doorID.Count - 1)
            {
                finalString += "Key " + doorID[i].ToString() + ", ";
            }
            else
            {
                finalString += "and Key " + doorID[i].ToString();
            }
        }
        return finalString;
    }
}
