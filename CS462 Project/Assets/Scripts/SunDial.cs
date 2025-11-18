using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class TimeOfDay : MonoBehaviour
{
    public float degree;
    public GameObject sun;
    float time = 0f;
    float duration = 10f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Vector3 current = transform.eulerAngles;
            if(current.x < degree)
            {
                    StartCoroutine(RotateSun(current));
               
            }
            
        }
    }

    private IEnumerator RotateSun(Vector3 current)
    {
        Vector3 target = new Vector3(degree, -30f, 0f);
        while (time < 1f)
        {
            time += Time.deltaTime / duration;
            sun.transform.rotation = Quaternion.Lerp(Quaternion.Euler(current), Quaternion.Euler(target), time);
            yield return null;
        }
    }
}
