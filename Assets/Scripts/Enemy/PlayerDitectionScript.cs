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
            EnemyValuesScript.PlayerGameObject = GameObject.FindGameObjectWithTag("Player").transform;
  
        }


        private void Update()
        {
            if (EnemyMovementScript != null && EnemyMovementScript != null && !EnemyValuesScript.PlayerGameObject.GetComponent<PlayerValueStats>().GameOver)
            { 
                Debug.Log(Physics2D.Raycast(transform.position, transform.position - EnemyValuesScript.PlayerGameObject.position, (int)LayerStuff.LayerEnum.PLAYER >> (int)LayerStuff.LayerEnum.Wall).collider.gameObject.layer == (int)LayerStuff.LayerEnum.ENEMY);
                Debug.Log(Physics2D.Raycast(transform.position, transform.position - EnemyValuesScript.PlayerGameObject.position ));
                Debug.Log(Physics2D.Raycast(transform.position, transform.position - EnemyValuesScript.PlayerGameObject.position ).collider);
                Debug.Log(Physics2D.Raycast(transform.position, transform.position - EnemyValuesScript.PlayerGameObject.position ).collider.name);
                if (
                    Vector2.Distance(EnemyValuesScript.PlayerGameObject.transform.position, transform.position) <= EnemyValuesScript.DetectionRangeFromPlayer && 
                    Physics2D.Raycast(transform.position, EnemyValuesScript.PlayerGameObject.position - transform.position,EnemyValuesScript.DetectionRangeFromPlayer).collider.gameObject.layer == (int)LayerStuff.LayerEnum.ENEMY)
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
}