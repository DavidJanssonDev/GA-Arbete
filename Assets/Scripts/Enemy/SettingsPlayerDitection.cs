using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsPlayerDitection : MonoBehaviour
{
    [Header("Detection SETTINGS ")]
    [SerializeField] private GameObject DetectionObject;
    private EnemyValuesScript EnemyValuesScript;


    private void Awake()
    {

        EnemyValuesScript = GetComponent<EnemyValuesScript>();
        float _range = EnemyValuesScript.DetectionRange * 2;
        EnemyValuesScript.EnemyPlayerDetectionRange.transform.localScale = new Vector3(_range, _range);
       
    }

    // Update is called once per frame
    private void Update()
    {
        if (EnemyValuesScript.PlayerStats.GameOver == false)
        {
            DetectionObject.SetActive(EnemyValuesScript.DetectionEnabled);
        }

    }
 
}
