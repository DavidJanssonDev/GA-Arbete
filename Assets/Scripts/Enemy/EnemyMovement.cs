using PlayerStats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private EnemyValuesScript EnemyValuesScript;
    private Transform Player;
    private Vector3 LastKnownPosition;

    private void Awake()
    {

        Player = GameObject.FindGameObjectWithTag("Player").transform;

        EnemyValuesScript = GetComponent<EnemyValuesScript>();
        EnemyValuesScript.PlayerStats = Player.GetComponent<PlayerValueStats>();
        LastKnownPosition = transform.position;
    }

    private void Update()
    {
        if (EnemyValuesScript.PlayerDitected && EnemyValuesScript.PlayerStats.GameOver == false)
        {
            MoveEnemy();
        }
    }

    public void MoveEnemy()
    {
        Debug.Log("Move Enemy");

        // Update the LastKnownPosition only when the player is detected
        LastKnownPosition = Player.transform.position;

        // Move towards the LastKnownPosition gradually
        transform.position = Vector3.MoveTowards(transform.position, LastKnownPosition, EnemyValuesScript.MovmentSpeed * Time.deltaTime);
    }
}
