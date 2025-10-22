using Cinemachine;
using UnityEngine;
using System.Collections;

public class CameraZoom : MonoBehaviour
{
    int zoom = 60;
    int normal = 90;
    float smoothing = 5f;
    bool isZoomed = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            isZoomed = !isZoomed;
        }
        if (isZoomed)
        {
            GetComponent<CinemachineVirtualCamera>().m_Lens.FieldOfView = Mathf.Lerp(GetComponent<CinemachineVirtualCamera>().m_Lens.FieldOfView, zoom, Time.deltaTime * smoothing);
        }
        else
        {
            GetComponent<CinemachineVirtualCamera>().m_Lens.FieldOfView = Mathf.Lerp(GetComponent<CinemachineVirtualCamera>().m_Lens.FieldOfView, normal, Time.deltaTime * smoothing);
        }
    }
}
