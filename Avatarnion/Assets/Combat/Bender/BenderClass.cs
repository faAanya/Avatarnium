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
    public GameObject enemy;
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
        animator.SetTrigger("attack");
        enemy = FindClosestEnemy();
        SpawnEnemy();
        Debug.Log("pew pew");
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
        projectile = Instantiate(projectile, enemy.transform.position, Quaternion.identity);
    }



}
