using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<Animator>().SetTrigger("pushed");
            Debug.Log("pew pew");
            Destroy(gameObject);
        }
    }


    void Update()
    {
        if (transform.position.y < GameObject.FindGameObjectWithTag("Ground").transform.position.y - 2f)
        {
            Destroy(gameObject);
        }
    }
}
