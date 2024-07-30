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

    [SerializeField]
    private int length, width;

    void Awake()
    {

        tiles = new GameObject[4];
        StartCoroutine("GenerateTile");
    }

    IEnumerator GenerateTile()
    {
        System.Random random = new System.Random();
        int randomPos = random.Next(0, width + 1);
        Debug.Log(randomPos);
        for (int i = 0; i < length; i++)
        {
            if (randomPos == 0)
            {
                randomPos++;
            }
            else if (randomPos == width)
            {
                randomPos--;
            }
            else
            {
                randomPos += random.Next(-1, 2);
            }
            Debug.Log($"New {randomPos}");
            for (int j = 0; j <= width; j++)
            {
                Debug.Log(randomPos);

                Vector3 pos = new Vector3(gameObject.transform.position.x + j * saveTile.transform.localScale.x, gameObject.transform.position.y, gameObject.transform.position.z + i * saveTile.transform.localScale.z);
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
                yield return new WaitForSeconds(.05f);
            }
        }

    }

}
