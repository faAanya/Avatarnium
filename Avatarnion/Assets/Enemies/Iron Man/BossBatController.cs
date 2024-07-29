using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class BossBatController : EnemyClass
{
    bool isReady = true, isHitting = false;

    [SerializeField]
    float smooth = 5.0f;
    [SerializeField]
    float attackCoolDown = 1;
    float rndKeeper;

    public override void Awake()
    {
        base.Awake();
        FindPlayerPosition();
    }
    void FindPlayerPosition()
    {
        Vector3 Look = new Vector3(0, gameObject.transform.position.x - player.GetComponent<Transform>().position.x, gameObject.transform.position.z - player.GetComponent<Transform>().position.z);

        float angle = Mathf.Atan2(Look.z, -Look.y) * Mathf.Rad2Deg;
        rndKeeper = angle;
    }



    void Update()
    {

        if (transform.rotation != Quaternion.Euler(0, 90, 60) && attackCoolDown > 0)
        {
            Quaternion target = Quaternion.Euler(0, 90, 60);
            transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);
        }
        attackCoolDown -= Time.deltaTime;

        if (attackCoolDown <= 0 + Time.deltaTime)
        {

            isHitting = true;
        }
        if (isHitting)
        {

            float tiltAroundZ = -35;
            float tiltAroundY = rndKeeper;

            Quaternion target = Quaternion.Euler(0, tiltAroundY, tiltAroundZ);

            // Dampen towards the target rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);
            if (transform.rotation == target)
            {
                FindPlayerPosition();
                isHitting = false;
                attackCoolDown = 1f;
            }
        }

    }

}
