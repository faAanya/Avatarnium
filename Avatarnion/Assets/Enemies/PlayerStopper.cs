using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStopper : EnemyClass
{
    public GameObject[] projectiles;
    public int rootsCount;
    public GameObject root;


    public float cooldown, startCooldown, awareTime, startAwareTime, liveTime, startLiveTime;
    public override void Awake()
    {
        base.Awake();
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        startAwareTime = awareTime;
        startCooldown = cooldown;
        startLiveTime = liveTime;
    }

    // Update is called once per frame
    void Update()
    {
        cooldown -= Time.deltaTime;
        if (cooldown <= 0)
        {
            if (cooldown >= -.5f)
            {
                gameObject.transform.position = player.transform.position;
            }
            else
            {
                awareTime -= Time.deltaTime;
                gameObject.GetComponent<MeshRenderer>().enabled = true;
                if (awareTime <= 0)
                {

                    Surround();
                    liveTime -= startLiveTime;
                    if (liveTime <= 0)
                    {
                        Reset();
                    }
                }
            }
        }
    }
    void Reset()
    {

        gameObject.GetComponent<MeshRenderer>().enabled = false;
        cooldown = startCooldown;
        awareTime = startAwareTime;
        liveTime = startLiveTime;
        // for (int i = 0; i < gameObject.transform.childCount; i++)
        // {
        //     Destroy(gameObject.transform.GetChild(i).gameObject);
        // }
    }
    void Surround() //Spawns stones around player in different directions
    {

        Debug.Log("Frow");
        projectiles = new GameObject[rootsCount];
        for (int i = 0; i < projectiles.Length; i++)
        {
            projectiles[i] = root;
        }

        for (int i = 0, j = 0; i < projectiles.Length && j < 360; i++, j += 360 / projectiles.Length)
        {

            Vector3 pos = new Vector3(1f * Mathf.Cos(j * Mathf.PI / 180) + transform.position.x, .5f, 1f * Mathf.Sin(j * Mathf.PI / 180) + transform.position.z);
            GameObject prj = Instantiate(projectiles[i], pos, Quaternion.identity);
            prj.transform.parent = gameObject.transform;

        }


    }
}
