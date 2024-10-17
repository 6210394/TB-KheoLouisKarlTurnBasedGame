using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraZoneScript : MonoBehaviour
{
    public CinemachineVirtualCamera attachedCamera;

    void Awake()
    {
        attachedCamera = GetComponentInChildren<CinemachineVirtualCamera>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            attachedCamera.Priority = 10;
            GameManager.instance.ChangeCameraTarget(other.transform, this.attachedCamera);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            attachedCamera.Priority = 0;
        }
    }
}
