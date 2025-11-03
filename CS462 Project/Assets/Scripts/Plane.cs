using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class Plane : MonoBehaviour
{
    public List<GameObject> parts = new List<GameObject>();
    public void repairPart(int index)
    {
        parts[index].SetActive(true);
    }
}
