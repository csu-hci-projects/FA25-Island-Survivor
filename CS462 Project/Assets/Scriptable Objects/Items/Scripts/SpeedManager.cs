using StarterAssets;
using UnityEngine;
using System.Collections;
using static UnityEditor.Progress;

public class SpeedManager : MonoBehaviour
{
    public void UseEquipment(EquipmentObject item)
    {
        Debug.Log("Using Equipment increase by " + item.speedIncrease + " for " + item.speedTime + " seconds");
        GetComponent<FirstPersonController>().MoveSpeed *= item.speedIncrease;
        GetComponent<FirstPersonController>().SprintSpeed *= item.speedIncrease;
        StartCoroutine(EffectDuration(item.speedTime, item));

    }
    private IEnumerator EffectDuration(int time, EquipmentObject item)
    {
        yield return new WaitForSeconds(time);
        GetComponent<FirstPersonController>().MoveSpeed /= item.speedIncrease;
        GetComponent<FirstPersonController>().SprintSpeed /= item.speedIncrease;
    }
}
