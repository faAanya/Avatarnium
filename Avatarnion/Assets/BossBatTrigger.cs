using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBatTrigger : MonoBehaviour
{

    [SerializeField]
    private float damage;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerHealthController.OnHealthChange.Invoke(-damage);
        }
    }
}
