using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine.Utility;
using UnityEngine;
using UnityEngine.InputSystem;

public class BenderClass : MonoBehaviour
{

    public PlayerControls playerInput;
    /* public float damage;
    // public float speed;
    // public float range;
    // public float coolDown;*/

    private Animator animator;

    [Header("Attack1 - FireBall")]
    public InputAction attack1;
    public GameObject fireBall;


    [Header("Attack2 - Stone Attack")]
    public int stonesCount;
    public int stonesRowCount;

    public InputAction attack2;
    public GameObject stone;

    [Header("Attack3 - Lightening")]
    public InputAction attack3;
    public GameObject lightening;
    public int lighteningCount;
    [Header("Attack4 - More FireBalls")]
    public InputAction attack4;
    public int fireBallsCount;
    [Header("Attack5 - Air Attack")]
    public InputAction attack5;


    public GameObject[] projectiles;

    private InteractableObject interactableObject;



    void Awake()
    {

        playerInput = new PlayerControls();

        animator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
    }

    public virtual void OnEnable()
    {
        attack1 = playerInput.Combinations.Fire1;
        attack1.performed += SpawnProjectile;

        attack2 = playerInput.Combinations.Fire2;
        attack2.performed += SpawnSurrond;

        attack3 = playerInput.Combinations.Fire3;
        attack3.performed += FindClosestInteractableObjects;

        attack4 = playerInput.Combinations.Fire4;
        attack4.performed += ShooterUp;

        attack5 = playerInput.Combinations.Fire5;
        attack5.performed += Push;

        playerInput.Combinations.Enable();
    }


    void OnDisable()
    {
        attack1.performed -= SpawnProjectile;
        attack2.performed -= SpawnSurrond;
        attack3.performed -= FindClosestInteractableObjects;
        attack4.performed -= ShooterUp;
        attack5.performed -= Push;



        playerInput.Combinations.Disable();
    }

    [System.Obsolete]
    InteractableObject FindClosestInteractableObject() //looks for closest interactableObject
    {
        float distanceToClosestinteractableObject = Mathf.Infinity;
        InteractableObject closestinteractableObject = null;
        IEnumerable<InteractableObject> allEnemies = FindObjectsOfType<MonoBehaviour>().OfType<InteractableObject>();

        foreach (InteractableObject currentinteractableObject in allEnemies)
        {
            float distanceTointeractableObject = (currentinteractableObject.transform.position - this.transform.position).sqrMagnitude;
            if (distanceTointeractableObject < distanceToClosestinteractableObject)
            {
                distanceToClosestinteractableObject = distanceTointeractableObject;
                closestinteractableObject = currentinteractableObject;
            }
        }
        return closestinteractableObject;

    }
    public void SpawnProjectile(InputAction.CallbackContext context) //Shooting to closest interactableObject
    {
        interactableObject = FindClosestInteractableObject();
        GameObject newProjectile = Instantiate(fireBall, gameObject.transform.position, Quaternion.identity);
        newProjectile.GetComponent<Rigidbody>().velocity = (interactableObject.transform.position + interactableObject.GetComponent<Collider>().bounds.size / 2 - gameObject.transform.position) * 15f;


        AudioManager.Instance.PlayOneShot(FMODEvents.Instance.fireBallShot, this.transform.position);
    }


    public void SpawnSurrond(InputAction.CallbackContext context)
    {
        StartCoroutine(Surround());
        AudioManager.Instance.PlayOneShot(FMODEvents.Instance.stoneShot, transform.position);
    }
    IEnumerator Surround() //Spawns stones around player in different directions
    {
        projectiles = new GameObject[stonesCount];
        for (int i = 0; i < projectiles.Length; i++)
        {
            projectiles[i] = stone;
        }
        for (int z = 0; z < stonesRowCount; z++)
        {
            for (int i = 0, j = 0; i < projectiles.Length && j < 360; i++, j += 360 / projectiles.Length)
            {

                Vector3 pos = new Vector3(z * .5f * Mathf.Cos(j * Mathf.PI / 180) + transform.position.x, .5f, .5f * z * Mathf.Sin(j * Mathf.PI / 180) + transform.position.z);
                GameObject prj = Instantiate(projectiles[i], pos, Quaternion.identity);
                prj.transform.parent = gameObject.transform;

            }
            yield return new WaitForSeconds(.05f);

        }

    }

    void FindClosestInteractableObjects(InputAction.CallbackContext context) //Lightening 
    {
        IEnumerable<InteractableObject> allEnemies = FindObjectsOfType<MonoBehaviour>().OfType<InteractableObject>();
        allEnemies.ToList();
        System.Random r = new System.Random();

        for (int i = 0; i < lighteningCount; i++)
        {
            int index = r.Next(0, allEnemies.ToList().Count);
            GameObject newProjectile = Instantiate(lightening, allEnemies.ToList()[index].transform.position + new Vector3(0, 20f), Quaternion.identity);
            allEnemies.ToList().RemoveAt(index);
        }
        AudioManager.Instance.PlayOneShot(FMODEvents.Instance.lighteningShot, transform.position);
    }


    private void ShooterUp(InputAction.CallbackContext context) //Barley attack up
    {
        System.Random random = new System.Random();
        for (int i = 0; i < fireBallsCount; i++)
        {
            GameObject bullet = Instantiate(fireBall, gameObject.transform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody>().AddForce(new Vector3(random.Next(-5, 5), 10f, random.Next(-5, 5)), ForceMode.Impulse);
            AudioManager.Instance.PlayOneShot(FMODEvents.Instance.fireBallShot, bullet.transform.position);
        }

    }

    private void Push(InputAction.CallbackContext context)//Pushes enemies back
    {

        IEnumerable<InteractableObject> allEnemies = FindObjectsOfType<MonoBehaviour>().OfType<InteractableObject>();
        foreach (InteractableObject interactableObject in allEnemies)
        {

            if (interactableObject.moving == Moving.Dynamic)
            {
                interactableObject.GetComponent<Rigidbody>().velocity = new Vector3((interactableObject.transform.position.x - transform.position.x), 2f, (interactableObject.transform.position.z - transform.position.z)); ;
            }
        }
        AudioManager.Instance.PlayOneShot(FMODEvents.Instance.airShot, transform.position);
    }

    private void Pull(InputAction.CallbackContext context)//Pushes enemies back
    {
        IEnumerable<InteractableObject> allEnemies = FindObjectsOfType<MonoBehaviour>().OfType<InteractableObject>();
        foreach (InteractableObject interactableObject in allEnemies)
        {
            if (interactableObject.moving == Moving.Dynamic)
            {
                interactableObject.GetComponent<Rigidbody>().velocity = new Vector3((transform.position.x - interactableObject.transform.position.x), 2f, (transform.position.z - interactableObject.transform.position.z)); ;
            }

        }
        AudioManager.Instance.PlayOneShot(FMODEvents.Instance.airShot, transform.position);
    }



}


