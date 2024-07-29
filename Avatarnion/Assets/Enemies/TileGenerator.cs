using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TileGenerator : MonoBehaviour
{

    [SerializeField]
    GameObject saveTile, killingTile;
    GameObject[] tiles;

    void Awake()
    {

        tiles = new GameObject[4];
        StartCoroutine("GenerateTile");
    }

    IEnumerator GenerateTile()
    {
        System.Random random = new System.Random();
        int randomPos = random.Next(0, 4);
        // for (int i = 0; i < tiles.Length; i++)
        // {
        //     Vector3 pos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + i * 5);
        //     if (i == randomPos)
        //     {
        //         GameObject newTile = Instantiate(saveTile, pos, Quaternion.identity);
        //     }
        //     else
        //     {
        //         GameObject newTile = Instantiate(killingTile, pos, Quaternion.identity);

        //     }
        // }
        Debug.Log(randomPos);
        for (int i = 0; i < 20; i++)
        {
            if (randomPos == 0)
            {
                randomPos++;
            }
            else if (randomPos == 3)
            {
                randomPos--;
            }
            else
            {
                randomPos += random.Next(-1, 2);
            }
            Debug.Log($"New {randomPos}");
            for (int j = 0; j < 4; j++)
            {
                Debug.Log(randomPos);

                Vector3 pos = new Vector3(gameObject.transform.position.x - i * saveTile.transform.localScale.x, gameObject.transform.position.y, gameObject.transform.position.z + j * saveTile.transform.localScale.z);
                if (j == randomPos)
                {
                    GameObject newTile = Instantiate(saveTile, pos, Quaternion.identity);
                    newTile.transform.SetParent(gameObject.transform);
                }
                else
                {
                    GameObject newTile = Instantiate(killingTile, pos, Quaternion.identity);
                    newTile.transform.SetParent(gameObject.transform);
                }
                yield return new WaitForSeconds(.5f);
            }
        }

    }

}
