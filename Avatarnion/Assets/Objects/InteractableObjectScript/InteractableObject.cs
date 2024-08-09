using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public Moving moving;
    public List<ObjectMaterial> material;

    public ObjectState objectState = ObjectState.General;

    public float burningSec;
    public float dryingSec;
    float tempDruingSpeed;

    public bool emiss = false;
    public GameObject emission;

    void Awake()
    {
        if (emission != null)
        {
            emission.SetActive(emiss);
        }
        // if (moving == Moving.Static)
        // {
        //     gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

        // }
        tempDruingSpeed = dryingSec;
    }

    public virtual void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<InteractableObject>() != null)
        {


            if (other.GetComponent<InteractableObject>().objectState == ObjectState.Burning && gameObject.GetComponent<InteractableObject>().material.Contains(ObjectMaterial.FireFrigile))
            {
                Debug.Log("Fire!");
                gameObject.GetComponent<InteractableObject>().objectState = ObjectState.Burning;

            }
        }

    }

    void Update()
    {
        if (objectState == ObjectState.Burning)
        {
            burningSec -= Time.deltaTime;

            if (burningSec <= 0.0f)
            {
                Destroy(gameObject);
            }
        }
        else if (objectState == ObjectState.Wet)
        {
            dryingSec -= Time.deltaTime;

            if (dryingSec <= 0.0f)
            {
                objectState = ObjectState.General;
                dryingSec = tempDruingSpeed;
            }
        }
    }

    public void AbsorbWater()
    {

    }



}

public enum Moving
{
    Static,
    Dynamic
}
public enum ObjectMaterial
{
    Water,
    FireFrigile,
    Meltable,
    Teleportable,
    Brokable,
    Lightable,
    General
}
public enum ObjectState
{
    Solid,
    Broken,
    Burning,
    Wet,
    General
}