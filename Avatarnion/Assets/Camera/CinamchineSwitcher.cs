
using UnityEngine;
using Cinemachine;
public class CinamchineSwitcher : MonoBehaviour
{



    private bool MainCamera = true;

    [SerializeField]
    private CinemachineFreeLook mainCamera, bossCamera;

    public Collider cameraChangerTrigger;

    void Awake()
    {
        bossCamera.GetComponent<Camera>().enabled = !bossCamera.GetComponent<Camera>().enabled;
    }

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
            bossCamera.GetComponent<Camera>().enabled = true;
            mainCamera.GetComponent<Camera>().enabled = false;
            mainCamera.Priority = 0;
            bossCamera.Priority = 1;

        }
        else
        {
            bossCamera.GetComponent<Camera>().enabled = false;
            mainCamera.GetComponent<Camera>().enabled = true;
            mainCamera.Priority = 1;
            bossCamera.Priority = 0;

        }
        MainCamera = !MainCamera;
    }

    public void SwitchMainCamera(Camera camera)
    {
        camera.enabled = !camera.enabled;
    }

}
