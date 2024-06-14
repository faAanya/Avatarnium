using System.Collections;
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

    private GameObject enemy;



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
        attack3.performed += FindClosestEnemies;

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
        attack3.performed -= FindClosestEnemies;
        attack4.performed -= ShooterUp;
        attack5.performed -= Push;



        playerInput.Combinations.Disable();
    }

    GameObject FindClosestEnemy() //looks for closest enemy
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
    public void SpawnProjectile(InputAction.CallbackContext context) //Shooting to closest Enemy
    {
        enemy = FindClosestEnemy();
        GameObject newProjectile = Instantiate(fireBall, gameObject.transform.position, Quaternion.identity);
        newProjectile.GetComponent<Rigidbody>().velocity = (enemy.transform.position + enemy.GetComponent<Collider>().bounds.size / 2 - gameObject.transform.position) * 15f;


        AudioManager.Instance.PlayOneShot(FMODEvents.Instance.fireBallShot, this.transform.position);
    }


    private void SpawnSurrond(InputAction.CallbackContext context)
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

                Vector3 pos = new Vector3(z * Mathf.Cos(j * Mathf.PI / 180) + transform.position.x, .5f, z * Mathf.Sin(j * Mathf.PI / 180) + transform.position.z);
                GameObject prj = Instantiate(projectiles[i], pos, Quaternion.identity);
                prj.transform.parent = gameObject.transform;

            }
            yield return new WaitForSeconds(.05f);

        }

    }

    void FindClosestEnemies(InputAction.CallbackContext context) //Lightening 
    {
        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        allEnemies.ToList();
        System.Random r = new System.Random();

        for (int i = 0; i < lighteningCount; i++)
        {
            int index = r.Next(0, allEnemies.Length);
            GameObject newProjectile = Instantiate(lightening, allEnemies[index].transform.position + new Vector3(0, 20f), Quaternion.identity);
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
        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in allEnemies)
        {
            // enemy.GetComponent<Rigidbody>().AddForce(new Vector3(enemy.transform.position.x - transform.position.x + 10f, .3f, enemy.transform.position.z - transform.position.z + 10f), ForceMode.Impulse);
            enemy.GetComponent<Rigidbody>().velocity = (enemy.transform.position - transform.position).normalized * 10f;
        }
        AudioManager.Instance.PlayOneShot(FMODEvents.Instance.airShot, transform.position);
    }


}
