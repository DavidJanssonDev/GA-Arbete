using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private EnemyValuesScript enemyValuesScript;
    private Vector3 lastKnowPosition;

    private void Awake()
    {
        enemyValuesScript = GetComponent<EnemyValuesScript>();
        enemyValuesScript.Player = GameObject.FindGameObjectWithTag("Player");
        lastKnowPosition = transform.position;
    }

    private void Update()
    {
        if (enemyValuesScript._PlayerDitected)
        {
            lastKnowPosition = enemyValuesScript.Player.transform.position;
        }

        MoveEnemy();


    }

    private void MoveEnemy()
    {
        transform.position = Vector3.MoveTowards(transform.position, lastKnowPosition, enemyValuesScript._movmentSpeed * Time.deltaTime);
    }
 }
