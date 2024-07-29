using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using StarterAssets;
using UnityEngine;

public class ThirdPersonShooterController : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera aimVirtualCamera;

    private StarterAssetsInputs starterAssetsInputs;

    [SerializeField] private LayerMask aimColliderLayerMask = new LayerMask();
    [SerializeField] private Transform debugTransform;

    [SerializeField]
    private BenderClass benderClass;

    private ThirdPersonController thirdPersonController;

    public GameObject fireBall;

    [SerializeField]
    private Transform spawnPosition;
    void Awake()
    {
        benderClass = GetComponent<BenderClass>();
        thirdPersonController = GetComponent<ThirdPersonController>();
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
    }

    void Update()
    {
        Vector3 mouseWorldPosition = Vector3.zero;
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2, Screen.height / 2);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 999f, aimColliderLayerMask))
        {
            // debugTransform.position = hitInfo.point;
            mouseWorldPosition = hitInfo.point;
        }
        if (starterAssetsInputs.aim)
        {
            aimVirtualCamera.gameObject.SetActive(true);
            thirdPersonController.SetRotateOnMove(false);

            Vector3 worldAimTarget = mouseWorldPosition;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;

            transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);
        }
        else
        {
            aimVirtualCamera.gameObject.SetActive(false);
            thirdPersonController.SetRotateOnMove(true);
        }

        if (starterAssetsInputs.fire1)
        {
            Vector3 aimDir = (mouseWorldPosition - spawnPosition.transform.position).normalized;
            Instantiate(fireBall, spawnPosition.transform.position, Quaternion.LookRotation(aimDir, Vector3.up));
            // newProjectile.GetComponent<Rigidbody>().velocity = (interactableObject.transform.position + interactableObject.GetComponent<Collider>().bounds.size / 2 - gameObject.transform.position) * 15f;

            starterAssetsInputs.fire1 = false;
        }
    }
}
