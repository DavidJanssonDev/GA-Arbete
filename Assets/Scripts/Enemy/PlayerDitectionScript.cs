using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GenerallStuff;
using PlayerStats;

namespace ENEMYDECTECTION
{
    public class PlayerDitectionScript : MonoBehaviour
    {
        private EnemyValuesScript EnemyValuesScript;
        private EnemyMovement EnemyMovementScript;


        private void Awake()
        {
            EnemyMovementScript = GetComponent<EnemyMovement>();
            EnemyValuesScript = GetComponent<EnemyValuesScript>();

            Debug.Log(EnemyValuesScript);
            Debug.Log(EnemyMovementScript);
        }


        private void Update()
        {
            if (EnemyMovementScript != null && EnemyMovementScript != null && !EnemyValuesScript.PlayerGameObject.GetComponent<PlayerValueStats>().GameOver )

                if (Vector2.Distance(EnemyValuesScript.PlayerGameObject.transform.position, transform.position) <= EnemyValuesScript.DetectionRangeFromPlayer)
                {
                    // STUFF IF THE PLAYER ARE DETECTED
                    EnemyValuesScript.PlayerDitected = true;
                    EnemyValuesScript.LastKnownPosition = EnemyValuesScript.PlayerGameObject.transform;

                }
                else
                {
                    // STUFF IF THE PLAYER ARE NOT DETECTED
                    EnemyValuesScript.PlayerDitected = false;

                }
        }
    }
}