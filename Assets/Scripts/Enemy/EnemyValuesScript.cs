using PlayerStats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyValuesScript : MonoBehaviour
{
        

    [Header("Detection Varaible")]
    public float DetectionRange;
    public float DetectionDelay;
    public bool PlayerDitected = false;
    public bool DetectionEnabled = false;
    public bool EnemyDetectionEnabel = true;
    public PlayerValueStats PlayerStats;
    public GameObject EnemyPlayerDetectionRange;



    [Header("ENEMY STATS")]
    public int Health = 10;
    public float MovmentSpeed;
    public int Damage = 1;
    public bool isHit = false;

    
}
