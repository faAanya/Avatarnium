using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using Cinemachine.Editor;
public class CinamchineSwitcher : MonoBehaviour
{



    private bool MainCamera = true;

    [SerializeField]
    private CinemachineVirtualCamera mainCamera, bossCamera;

    public Collider cameraChangerTrigger;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "BossTrigger")
        {
            SwitchState();
        }
    }

    private void SwitchState()
    {
        if (MainCamera)
        {
            mainCamera.Priority = 0;
            bossCamera.Priority = 1;
            SwitchMainCamera();
        }
        else
        {
            SwitchMainCamera();
            mainCamera.Priority = 1;
            bossCamera.Priority = 0;

        }
        MainCamera = !MainCamera;
    }

    public void SwitchMainCamera()
    {
        mainCamera.GetComponent<Camera>().enabled = !mainCamera.GetComponent<Camera>().enabled;
    }

}
