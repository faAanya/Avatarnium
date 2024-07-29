using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillingTile : EnemyClass
{
    [SerializeField]
    private float saveTime, startTime;

    public override void Awake()
    {
        base.Awake();
        startTime = saveTime;
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

