using PlayerStats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private EnemyValuesScript EnemyValuesScript;

    private void Awake()
    {
        EnemyValuesScript = GetComponent<EnemyValuesScript>();
    }

    private void Update()
    {
        if (EnemyValuesScript != null && EnemyValuesScript.PlayerDitected && !EnemyValuesScript.PlayerGameObject.GetComponent<PlayerValueStats>().GameOver)
        {
            MoveEnemy();
        }
    }

    private void MoveEnemy()
    {
        // Move towards the LastKnownPosition gradually
        transform.position = Vector3.MoveTowards(transform.position, EnemyValuesScript.LastKnownPosition.position, EnemyValuesScript.MovmentSpeed * Time.deltaTime);
    }
}
