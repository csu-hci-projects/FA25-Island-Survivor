using StarterAssets;
using UnityEngine;
using System.Collections;
using static UnityEditor.Progress;
using UnityEditor.ShaderGraph.Internal;

public class SpeedManager : MonoBehaviour
{
    float time;
    bool poisonApplied = false;
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
    public void ApplyPoison(int damage, float time)
    {
        this.time = time;
        if (!poisonApplied)
        {
            StartCoroutine(DamageOverTime(damage));
            poisonApplied = true;
        }
    }
    private IEnumerator DamageOverTime(int damage)
    {
        while (time > 0)
        {
            GetComponent<PlayerHealthManager>().dealDamage(-damage);
            time--;
            yield return new WaitForSeconds(1.0f);
        }
        poisonApplied = false;
    }
}
