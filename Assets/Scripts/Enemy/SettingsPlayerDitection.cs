using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsPlayerDitection : MonoBehaviour
{
    [Header("DITECTION SETTINGS ")]
    [SerializeField] private GameObject DitectionObject;
    private EnemyValuesScript EnemyValuesScript;


    private void Awake()
    {

        EnemyValuesScript = GetComponent<EnemyValuesScript>();
        float _range = EnemyValuesScript._DitectionRange * 2;
        EnemyValuesScript.transform.localScale = new Vector3(_range, _range);
       
    }

    // Update is called once per frame
    private void Update()
    {   
         DitectionObject.SetActive(EnemyValuesScript._DitectionEnabled);
    }
}
