using System.Collections;

using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenuAttribute(fileName = "Bender", menuName = "Assets/Benders")]
public class BenderClass : MonoBehaviour
{

    public PlayerControls playerInput;
    public float damage;
    public float speed;
    public float range;
    public float coolDown;

    private Animator animator;
    public InputAction attack;

    public GameObject projectile;
    public GameObject[] projectiles;


    private GameObject enemy;


    public GameObject OriginalSurrounderObject;
    public int SurrounderObjectCount;

    public Transform SurrounderParentTransform;


    void Awake()
    {

        playerInput = new PlayerControls();

        animator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
    }

    public virtual void OnEnable()
    {
        attack = playerInput.Combinations.Fire1;
        attack.performed += Attack;
        playerInput.Combinations.Enable();
    }


    void OnDisable()
    {
        attack.performed -= Attack;
        playerInput.Combinations.Disable();
    }

    private void Attack(InputAction.CallbackContext context)
    {
        // animator.SetTrigger("attack");
        // enemy = FindClosestEnemy();
        // Invoke("SpawnEnemy", .6f);
        StartCoroutine(Surround());

    }

    IEnumerator Surround()
    {
        // float AngleStep = 360 / SurrounderObjectCount;


        // for (int i = 1; i < SurrounderObjectCount; i++)
        // {
        //     for (int j = 0; j < 3; j++)
        //     {
        //         GameObject newSurrounderObject = Instantiate(OriginalSurrounderObject);

        //         newSurrounderObject.transform.RotateAround(transform.position, Vector3.up, AngleStep * i);
        //         newSurrounderObject.transform.SetParent(transform);
        //     }

        // }
        projectiles = new GameObject[SurrounderObjectCount];
        for (int i = 0; i < projectiles.Length; i++)
        {
            projectiles[i] = projectile;
        }
        for (int z = 0; z < 10; z++)
        {
            for (int i = 0, j = 0; i < projectiles.Length && j < 360; i++, j += 360 / projectiles.Length)
            {

                Vector3 pos = new Vector3(z * Mathf.Cos(j * Mathf.PI / 180) + transform.position.x, .5f, z * Mathf.Sin(j * Mathf.PI / 180) + transform.position.z);
                GameObject prj = Instantiate(projectiles[i], pos, Quaternion.identity);
                prj.transform.parent = gameObject.transform;

            }
            yield return new WaitForSeconds(.05f);

        }

    }
    GameObject FindClosestEnemy()
    {
        float distanceToClosestEnemy = Mathf.Infinity;
        GameObject closestEnemy = null;
        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject currentEnemy in allEnemies)
        {
            float distanceToEnemy = (currentEnemy.transform.position - this.transform.position).sqrMagnitude;
            if (distanceToEnemy < distanceToClosestEnemy)
            {
                distanceToClosestEnemy = distanceToEnemy;
                closestEnemy = currentEnemy;
            }
        }
        return closestEnemy;

    }
    public void SpawnEnemy()
    {
        GameObject newProjectile = Instantiate(projectile, gameObject.transform.position, Quaternion.identity);
        newProjectile.GetComponent<Rigidbody>().velocity = (enemy.transform.position + enemy.GetComponent<Collider>().bounds.size / 2 - gameObject.transform.position) * 15f;
    }



}
