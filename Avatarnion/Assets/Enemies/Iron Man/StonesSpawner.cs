using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StonesSpawner : MonoBehaviour
{
    public float attackCoolDown, numKeeper;
    public GameObject stone;
    public int stoneCount;
    private void StoneShooter() //Barley attack up
    {
        System.Random random = new System.Random();
        for (int i = 0; i < stoneCount; i++)
        {
            GameObject bullet = Instantiate(stone, gameObject.transform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody>().AddForce(new Vector3(random.Next(-20, 20), 10f, random.Next(-40, 1)), ForceMode.Impulse);
        }

    }

    void Awake()
    {
        numKeeper = attackCoolDown;
    }
    // Update is called once per frame
    void Update()
    {
        attackCoolDown -= Time.deltaTime;

        if (attackCoolDown <= 0 + Time.deltaTime)
        {

            StoneShooter();
            attackCoolDown = numKeeper;
        }
    }
}
