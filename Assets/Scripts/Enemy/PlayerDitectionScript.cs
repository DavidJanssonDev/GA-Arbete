using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDitectionScript : MonoBehaviour
{
    EnemyValuesScript _enemyValuesScript;
    private void Awake()
    {
        _enemyValuesScript = transform.parent.GetComponent<EnemyValuesScript>();
    }


    private void OnTriggerEnter2D(Collider2D gameobject)
    {
       

        if (gameobject.CompareTag("Player"))
        {
            _enemyValuesScript._PlayerDitected = true;
        }

    }

    private void OnTriggerExit2D(Collider2D gameobject)
    {
        
        if (gameobject.CompareTag("Player"))
        {
            _enemyValuesScript._PlayerDitected = false;
        }

    }
    
}
