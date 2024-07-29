using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{

    public List<Effect> effects;
    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<InteractableObject>() != null)
        {
            if (effects.Contains(Effect.Fire) && other.GetComponent<InteractableObject>().material.Contains(ObjectMaterial.FireFrigile))
            {
                other.GetComponent<InteractableObject>().objectState = ObjectState.Burning;
                other.GetComponent<InteractableObject>().emiss = true;
                other.GetComponent<InteractableObject>().emission.SetActive(other.GetComponent<InteractableObject>().emiss);
                Destroy(gameObject);
            }
            if (effects.Contains(Effect.Water))
            {
                other.GetComponent<InteractableObject>().objectState = ObjectState.Wet;
            }
            Destroy(gameObject);
        }

    }

    void Start()
    {
        float speed = 100f;
        gameObject.GetComponent<Rigidbody>().velocity = transform.forward * speed;
    }
    void Update()
    {
        if (transform.position.y < GameObject.FindGameObjectWithTag("Ground").transform.position.y - 2f)
        {
            Destroy(gameObject);
        }
    }
}
public enum Effect
{
    Break,
    Fire,
    Water,
    Freez,
    Push,
    Pull
}