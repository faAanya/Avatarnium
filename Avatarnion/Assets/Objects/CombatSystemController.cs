// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class CombatSystemController : MonoBehaviour
// {
//     public void Update()
//     {
//         FindClosestEnemy();
//     }
//     void FindClosestEnemy()
//     {
//         float distanceToClosestEnemy = Mathf.Infinity;
//         GameObject closestEnemy = null;
//         GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");

//         {

//             foreach (GameObject currentEnemy in allEnemies)
//             {
//                 float distanceToEnemy = (currentEnemy.transform.position - this.transform.position).sqrMagnitude;
//                 if (distanceToEnemy < distanceToClosestEnemy)
//                 {
//                     distanceToClosestEnemy = distanceToEnemy;
//                     closestEnemy = currentEnemy;
//                 }
//             }
//         }
//     }
// }