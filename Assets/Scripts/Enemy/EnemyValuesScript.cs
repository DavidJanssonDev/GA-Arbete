using PlayerStats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyValuesScript : MonoBehaviour
{
        

    [Header("Detection Varaible")]
    // SETTINGS ON DETECTION FOR THE ENEMY 
    public float DetectionRangeFromPlayer;
    public float DetectionDelay;
    // DETCTION VARIABLES
    public bool PlayerDitected = false;
    public bool DetectionEnabled = false;
    public bool EnemyDetectionEnabel = true;
    public Transform LastKnownPosition;
    
    public PlayerValueStats PlayerStats;
    public Transform PlayerGameObject;



    [Header("ENEMY STATS")]
    public int Health = 10;
    public float MovmentSpeed;
    public int Damage = 1;
    public bool isHit = false;
    

}
