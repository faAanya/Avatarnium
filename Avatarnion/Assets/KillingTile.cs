using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillingTile : MonoBehaviour
{
    [SerializeField]
    private float saveTime, startTime;

    [SerializeField]
    private float damage;

    GameObject player;

    void Awake()
    {
        startTime = saveTime;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject == player)
        {
            Debug.Log("Meow");
            saveTime -= Time.deltaTime;
            if (saveTime < 0)
            {
                PlayerHealthController.OnHealthChange(-damage);
            }
        }
    }


    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            saveTime = startTime;
        }
    }
}

