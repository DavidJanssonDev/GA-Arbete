using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GenerallStuff;

public class PlayerDitectionScript : MonoBehaviour
{
    EnemyValuesScript EnemyValuesScript;
    EnemyMovement EnemyMovementScript;

    
    
    
    
    private float lastDetectionTime;
    private void Awake()
    {
        EnemyValuesScript = GetComponent<EnemyValuesScript>();
        EnemyMovementScript = GetComponent<EnemyMovement>();
    }

    private void OnDrawGizmosSelected()
    {
        if (!Application.isPlaying) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, EnemyValuesScript.DetectionRange);
    }

    private void OnTriggerEnter2D(Collider2D gameobject)
    {
        if (CanDetectPlayer(gameobject))
        {
            EnemyValuesScript.PlayerDitected = true;
            StartCoroutine(DelayedMoveEnemy());
        }
    }

    private void OnTriggerExit2D(Collider2D gameobject)
    {
        if (gameobject.gameObject.layer == (int)LayerStuff.LayerEnum.PLAYER && EnemyValuesScript.EnemyDetectionEnabel)
        {
            EnemyValuesScript.PlayerDitected = false;
        }
    }

    private bool CanDetectPlayer(Collider2D gameobject)
    {
        if (gameobject.gameObject.layer == (int)LayerStuff.LayerEnum.PLAYER &&
            EnemyValuesScript.EnemyDetectionEnabel &&
            Time.time - lastDetectionTime > EnemyValuesScript.DetectionDelay &&
            Vector2.Distance(transform.position, gameobject.transform.position) <= EnemyValuesScript.DetectionRange)
        {
            lastDetectionTime = Time.time;
            Debug.Log("Player detected!");
            return true;
        }

        return false;
    }


    private IEnumerator DelayedMoveEnemy()
    {
        yield return new WaitForSeconds(EnemyValuesScript.DetectionDelay);
        if (EnemyValuesScript.PlayerDitected)
        {
            EnemyMovementScript.MoveEnemy();
        }
    }

}
