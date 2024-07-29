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

    private Animator animator;
    public float healthBuffer;
    void Awake()
    {
        benderClass = GetComponent<BenderClass>();
        thirdPersonController = GetComponent<ThirdPersonController>();
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        animator = GetComponent<Animator>();
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

            animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 1f, Time.deltaTime * 10f));

            Vector3 worldAimTarget = mouseWorldPosition;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;

            transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);
        }
        else
        {
            aimVirtualCamera.gameObject.SetActive(false);
            thirdPersonController.SetRotateOnMove(true);
            animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 0f, Time.deltaTime * 10f));


        }

        if (starterAssetsInputs.fire1)
        {
            Vector3 aimDir = (mouseWorldPosition - spawnPosition.transform.position).normalized;
            Instantiate(fireBall, spawnPosition.transform.position, Quaternion.LookRotation(aimDir, Vector3.up));
            // newProjectile.GetComponent<Rigidbody>().velocity = (interactableObject.transform.position + interactableObject.GetComponent<Collider>().bounds.size / 2 - gameObject.transform.position) * 15f;

            starterAssetsInputs.fire1 = false;
        }
        if (starterAssetsInputs.fire2)
        {
            IEnumerable<InteractableObject> allEnemies = FindObjectsOfType<InteractableObject>();
            foreach (InteractableObject interactableObject in allEnemies)
            {

                if (interactableObject.moving == Moving.Dynamic)
                {
                    interactableObject.GetComponent<Rigidbody>().velocity = new Vector3((interactableObject.transform.position.x - transform.position.x), 2f, (interactableObject.transform.position.z - transform.position.z)); ;
                }
            }
            AudioManager.Instance.PlayOneShot(FMODEvents.Instance.airShot, transform.position);
            starterAssetsInputs.fire2 = false;
        }
        if (starterAssetsInputs.fire3)
        {
            IEnumerable<InteractableObject> allEnemies = FindObjectsOfType<InteractableObject>();
            foreach (InteractableObject interactableObject in allEnemies)
            {
                if (interactableObject.moving == Moving.Dynamic)
                {
                    interactableObject.GetComponent<Rigidbody>().velocity = new Vector3((transform.position.x - interactableObject.transform.position.x), 2f, (transform.position.z - interactableObject.transform.position.z)); ;
                }

            }
            AudioManager.Instance.PlayOneShot(FMODEvents.Instance.airShot, transform.position);
            starterAssetsInputs.fire3 = false;
        }
        if (starterAssetsInputs.fire4)
        {
            PlayerHealthController.OnHealthChange.Invoke(healthBuffer);
            starterAssetsInputs.fire4 = false;
        }

    }
}
