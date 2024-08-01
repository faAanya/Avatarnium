using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveProjectile : MonoBehaviour
{
    /* public float maxColliderRadius;

    // private SphereCollider sphereCollider;

    // public override void Start()
    // {
    //     base.Start();
    //     sphereCollider = GetComponent<SphereCollider>();

    // }

    // public override void Update()
    // {
    //     base.Update();
    // }

    // void OnCollisionEnter(Collision other)
    // {
    //     if (other.gameObject.GetComponent<InteractableObject>() != null)
    //     {
    //         StartCoroutine(Explosion());
    //     }
    // }


    // IEnumerator Explosion() //increases the collider of the objects
    // {

    //     while (sphereCollider.radius <= maxColliderRadius)
    //     {
    //         Debug.Log(sphereCollider.radius);
    //         sphereCollider.radius += .2f;
    //         yield return new WaitForSeconds(.002f);
    //     }
    //     Destroy(gameObject);
    // }*/


    // Private variables
    [SerializeField] private float delay = 1.0f;
    [SerializeField] private float radius = 5.0f;
    [SerializeField] private float force = 1500.0f;
    [SerializeField] private bool explodeOnCollision = false;
    private float delayTimer;

    [SerializeField] private float speed;


    // Unity methods
    private void Awake()
    {
        gameObject.GetComponent<Rigidbody>().velocity = transform.forward * speed;
        delayTimer = 0.0f;
    }

    private void Update()
    {
        delayTimer += Time.deltaTime;

        if (delayTimer >= delay && !explodeOnCollision)
        {
            DoExplosion();
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (explodeOnCollision && enabled)
        {
            DoExplosion();
            Destroy(gameObject);
        }
    }

    // Helper methods
    private void DoExplosion()
    {
        HandleDestruction();
    }


    private void HandleDestruction()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider collider in colliders)
        {
            Rigidbody rigidbody = collider.GetComponent<Rigidbody>();

            if (rigidbody != null)
            {
                rigidbody.AddExplosionForce(force, transform.position, radius);
            }
        }
    }
}


