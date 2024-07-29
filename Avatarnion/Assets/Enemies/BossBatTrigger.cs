using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBatTrigger : EnemyClass
{



    public override void Awake()
    {
        base.Awake();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            PlayerHealthController.OnHealthChange.Invoke(-damage);
        }
    }
}
