using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsPlayerDitection : MonoBehaviour
{
    [Header("DITECTION SETTINGS ")]
    [SerializeField] GameObject _DitectionObject;
     EnemyValuesScript _enemyValuesScript;


    private void Awake()
    {
        
        _enemyValuesScript = GetComponent<EnemyValuesScript>();
        float _range = _enemyValuesScript._DitectionRange * 2;
        _DitectionObject.transform.localScale = new Vector3(_range, _range);
       
    }

    // Update is called once per frame
    void Update()
    {
        _DitectionObject.SetActive(_enemyValuesScript._DitectionEnabled);
      
    }
}
