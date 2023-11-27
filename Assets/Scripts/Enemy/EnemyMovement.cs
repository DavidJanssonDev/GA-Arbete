using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    EnemyValuesScript _enemyValuesScript;
    [SerializeField] Vector3 _lastKnowPosition;

    private void Awake()
    {
        _enemyValuesScript = GetComponent<EnemyValuesScript>();
        _lastKnowPosition = transform.position;
    }

    private void Update()
    {
        if (_enemyValuesScript._PlayerDitected)
        {
            _lastKnowPosition = _enemyValuesScript.Player.transform.position;
        }

        transform.position = Vector3.MoveTowards(transform.position, _lastKnowPosition, _enemyValuesScript._movmentSpeed * Time.deltaTime);

    }
}
