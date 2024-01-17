using PlayerStats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private EnemyValuesScript EnemyValuesScript;
    private PlayerValueStats PlayerValuesScript;
    private Vector3 LastKnowPosition;

    private void Awake()
    {
        EnemyValuesScript = GetComponent<EnemyValuesScript>();
        EnemyValuesScript.PlayerStats = EnemyValuesScript.Player.GetComponent<PlayerValueStats>();
        LastKnowPosition = transform.position;
    }

    private void Update()
    {
        Debug.Log(EnemyValuesScript.Player);
        Debug.Log(PlayerValuesScript);
        if (EnemyValuesScript.PlayerStats.GameOver == false)
        {
            if (EnemyValuesScript._PlayerDitected)
            {
                LastKnowPosition = EnemyValuesScript.Player.transform.position;
            }
            MoveEnemy();
        }
    }

    private void MoveEnemy()
    {
        transform.position = Vector3.MoveTowards(transform.position, LastKnowPosition, EnemyValuesScript._movmentSpeed * Time.deltaTime);
    }
 }
